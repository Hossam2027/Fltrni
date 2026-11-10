using Fltrni.core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Fltrni.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger<GlobalExceptionMiddleware> Logger { get; }
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            Next = next;
            Logger = logger;
        }




        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (BadRequestException ex)
            {
                Logger.LogWarning(ex, ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = ex.Message
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);

                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Something went wrong."
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(response));
            }
        }

    }
}
