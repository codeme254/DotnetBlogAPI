using System.Text.Json;

namespace BlogAPI.Middlewares;

public class GlobalExceptionHandler(
    RequestDelegate next,
    IHostEnvironment hostEnvironment,
    ILogger<GlobalExceptionHandler> logger
)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var error = "Internal server error";
        var message = "An unexpected error occurred. Please try again";

        // SHOW ERROR IN DETAILS IN DEVELOPMENT
        if (_hostEnvironment.IsDevelopment())
        {
            error = exception.InnerException?.Message ?? exception.Message;
            _logger.LogError("There was an error while processing {method} {path}\n.{error}",
            httpContext.Request.Method, httpContext.Request.Path, error);
        }

        var errorResponse = new
        {
            Status = statusCode,
            Message = message,
            Errors = new[] { error }
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, jsonSerializerOptions);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(jsonResponse);
    }
}