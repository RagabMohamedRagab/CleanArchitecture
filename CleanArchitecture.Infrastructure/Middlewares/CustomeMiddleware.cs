using System.ComponentModel.DataAnnotations;
using System.Net;
using CleanArchitecture.Data.Exceptions;
using CleanArchitecture.Service.Responseobject;
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
           
            try {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = exception switch {
                NotFoundException notFoundEx => CreateResponse(
                    HttpStatusCode.NotFound,
                    notFoundEx.Message
                ),

                ValidationException validationEx => CreateResponse(
                    HttpStatusCode.BadRequest,
                    validationEx.Message
                ),

               
                BusineesException businessEx => CreateResponse(
                    HttpStatusCode.InternalServerError,
                    businessEx.Message
                ),
                Exception expection=>CreateResponse(HttpStatusCode.NotFound,exception.Message
                    )

               
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.Status;

            await context.Response.WriteAsJsonAsync(new
            {
                isSuccessed = response.IsSuccessed,
                status = (int)response.Status,
                message = response.Message,
                error = response.Error
            });
        }

        private static ResponseResult<bool> CreateResponse(HttpStatusCode status, string message)
        {
            return new ResponseResult<bool> {
                IsSuccessed = false,
                Status = status,
                Message = message,
               
            };
        }


    }
}
