using Application.Abstraction;
using Newtonsoft.Json;
using System.Net;

namespace Hazar.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IGlobalLoggingService _loggingService;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IGlobalLoggingService loggingService)
        {
            _next = next;
            _logger = logger;
            _loggingService = loggingService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            _logger.LogError(exception, exception.Message);
            await _loggingService.LogAsync(exception.Message, "Global Exception", logLevel: LogLevel.Error);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new { message = exception.Message, details = exception.StackTrace };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
