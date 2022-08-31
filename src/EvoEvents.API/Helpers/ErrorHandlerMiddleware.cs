using Infrastructure.Utilities.CustomException;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EvoEvents.API.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(CustomException exception)
            {
                await HandleExceptionAsync(context, exception, (int)HttpStatusCode.Conflict);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception, (int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = statusCode;

            await response.WriteAsync(exception.Message);
        }
    }
}
