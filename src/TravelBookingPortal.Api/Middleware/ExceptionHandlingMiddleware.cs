using Microsoft.AspNetCore.Http;
using System.Net;
using TravelBookingPortal.Api.Exceptions;

namespace TravelBookingPortal.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            if (exception is AppException appException){
                statusCode = appException.StatusCode;
                message = appException.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = new
            {
                error = message,
                stackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }
}