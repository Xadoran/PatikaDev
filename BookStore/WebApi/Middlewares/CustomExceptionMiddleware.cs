using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate  _next;

        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                
                string message;
                await _next(context);
                watch.Stop();
                message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path + " - ResponseCode: " +  context.Response.StatusCode + " in " + watch.ElapsedMilliseconds + " ms";
                _loggerService.Write(message); 
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
            
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = "[Error] HTTP " + context.Request.Method + " - " +context.Response.StatusCode + " ErrorMessage : " + ex.Message + " in " + watch.ElapsedMilliseconds + " ms"; 
            _loggerService.Write(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}