using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Middlewares
{
    public class CustomeMiddleware(RequestDelegate requestDelegate,ILogger<CustomeMiddleware> logger)
    {
        private readonly RequestDelegate _requestDelegate=requestDelegate;
        private readonly ILogger<CustomeMiddleware> _logger=logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation($"Log info For Request {httpContext.Request} - {httpContext.Request.Method}");
            await _requestDelegate(httpContext);
            _logger.LogInformation($"Log info For Response {httpContext.Response} - {httpContext.Response.StatusCode}");
        }

    }
}
