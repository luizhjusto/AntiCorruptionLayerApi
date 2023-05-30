using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AntiCorruptionLayer.Domain.Helpers
{
    public class ApiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ApiMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ApiMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    httpContext.Request?.Method,
                    httpContext.Request?.Path.Value,
                    httpContext.Response?.StatusCode);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);

                _logger.LogError(
                    "Request {method} {url} => {statusCode}: " + Environment.NewLine + "{error}",
                    httpContext.Request?.Method,
                    httpContext.Request?.Path.Value,
                httpContext.Response?.StatusCode,
                    ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            if (exception is BusinessException ex)
                context.Response.StatusCode = ex.StatusCode.GetHashCode();
            else
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorObject = new ExceptionResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = string.Format("{0}", exception.Message)
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorObject));
        }
    }
}