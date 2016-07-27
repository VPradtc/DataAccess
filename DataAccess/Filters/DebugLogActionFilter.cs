using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using DataAccess.ApplicationLogger.Abstraction.Interfaces;

namespace DataAccess.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly IApplicationLogger _logger;

        public LogActionFilter(IApplicationLogger logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = actionContext.ActionDescriptor.ActionName;
            string arguments = CreateArgumentsString(actionContext);

            _logger.LogDebug($"Request, Method={actionContext.Request.Method.Method}, Url={actionContext.Request.RequestUri}, Controller={controller}, Action={action}, Arguments: {arguments}");
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string controller = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string action = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            if (actionExecutedContext.Response.IsSuccessStatusCode)
            {

                _logger.LogDebug($"Request, Method={actionExecutedContext.ActionContext.Request.Method.Method}, Url={actionExecutedContext.ActionContext.Request.RequestUri}, Controller={controller}, Action={action} successfull");
            }
            else
            {
                _logger.LogDebug($"Request, Method={actionExecutedContext.ActionContext.Request.Method.Method}, Url={actionExecutedContext.ActionContext.Request.RequestUri}, Controller={controller}, Action={action} failed");
                var exception = actionExecutedContext.Response.Content.ReadAsAsync<HttpError>().Result;
                _logger.LogError(exception);
                if (exception != null)
                {
                    StringBuilder error = new StringBuilder();
                    error.AppendLine(
                        $"Request, Method={actionExecutedContext.ActionContext.Request.Method.Method}, Url={actionExecutedContext.ActionContext.Request.RequestUri}, Controller={controller}, Action={action} failed");
                    error.AppendLine(CreateExceptionString(exception));
                    _logger.LogError(error.ToString());
                }
            }
        }

        private string CreateArgumentsString(HttpActionContext filterContext)
        {
            var result = new StringBuilder();
            foreach (var p in filterContext.ActionArguments)
            {
                result.AppendLine($"Name: {p.Key}, Value: {JsonConvert.SerializeObject(p.Value)}");
            }
            return result.ToString();
        }

        private string CreateExceptionString(HttpError e)
        {
            var sb = new StringBuilder();
            CreateExceptionString(sb, e, string.Empty);

            return sb.ToString();
        }

        private void CreateExceptionString(StringBuilder sb, HttpError e, string indent)
        {
            if (indent == null)
            {
                indent = string.Empty;
            }
            else if (indent.Length > 0)
            {
                sb.AppendFormat("{0}Inner ", indent);
            }

            sb.AppendFormat("Exception Found:\n{0}Type: {1}", indent, e.ExceptionType);
            sb.AppendFormat("\n{0}Message: {1}", indent, e.Message);
            sb.AppendFormat("\n{0}Message Details: {1}", indent, e.MessageDetail);
            sb.AppendFormat("\n{0}Stacktrace: {1}", indent, e.StackTrace);

            if (e.InnerException != null)
            {
                sb.Append("\n");
                CreateExceptionString(sb, e.InnerException, indent + "  ");
            }
        }
    }
}
