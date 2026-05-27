
using System.Net;

namespace Producer.Presentation.Middlewares
{
    public sealed class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex) 
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex, $"[{errorId}] Unhandled exception: {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    Message = "Something went wrong. Please contact support."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
