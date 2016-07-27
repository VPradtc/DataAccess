using System.Net.Http;
using System.Threading.Tasks;
using DataAccess.ApplicationLogger.Abstraction.Interfaces;

namespace DataAccess.Filters
{
    public class DelegatingLogFilter : DelegatingHandler
    {
        private readonly IApplicationLogger _logger;

        public DelegatingLogFilter(IApplicationLogger logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var correlationId = request.GetCorrelationId().ToString();

            var requestInfo = await LogRequest(request, correlationId);

            var response = await base.SendAsync(request, cancellationToken);

            await LogResponse(response, correlationId, requestInfo);

            return response;
        }

        private async Task<string> LogRequest(HttpRequestMessage request, string correlationId)
        {
            var requestInfo = $"{request.Method} {request.RequestUri}";

            var requestMessage = "";

            await LogMessageAsync("Request", correlationId, requestInfo, requestMessage);

            return requestInfo;
        }

        private async Task LogResponse(HttpResponseMessage response, string correlationId, string requestInfo)
        {
            var responseMessage = string.Empty;

            if (response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                    responseMessage = await response.Content.ReadAsStringAsync();
            }
            else
                responseMessage = response.ReasonPhrase;

            await LogMessageAsync("Response", correlationId, requestInfo, responseMessage);
        }

        private async Task LogMessageAsync(string type, string correlationId, string requestInfo, string body)
        {
            await Task.Run(() => _logger.LogInfo($"{correlationId} - {requestInfo} - API - {type}: \r\n{body}"));
        }
    }
}
