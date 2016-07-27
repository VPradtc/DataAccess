using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace DataAccess.Filters
{
    public class InternalServerErrorExceptionFilter : ExceptionFilterAttribute
    {
        private const HttpStatusCode _errorCode = HttpStatusCode.InternalServerError;

        private HttpResponseMessage CreateResponse(HttpRequestMessage request, Exception e)
        {
            return request.CreateResponse(_errorCode, e);
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ctx = actionExecutedContext;

            ctx.Response = CreateResponse(ctx.Request, ctx.Exception);
        }
    }
}