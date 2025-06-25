using System.Text.Json;

namespace EventManagementSystem.Middleware;
public class ExceptionSanitizingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionSanitizingMiddleware> logger;

    public ExceptionSanitizingMiddleware(RequestDelegate next, ILogger<ExceptionSanitizingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var error = new
            {
                Message = ex.Message,
                Code = "INTERNAL_ERROR"
            };

            var json = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(json);
        }
    }
}

