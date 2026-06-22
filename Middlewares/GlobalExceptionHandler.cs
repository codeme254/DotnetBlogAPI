using System.Text.Json;
using BlogAPI.Exceptions;

namespace BlogAPI.Middlewares;

public class GlobalExceptionHandler(
    // RequestDelegate next,
    IHostEnvironment hostEnvironment,
    ILogger<GlobalExceptionHandler> logger
) : IMiddleware
{
    // private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;
    private readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = "An unexpected error occurred. Please try again.";
        var error = "Internal server error";

        if (exception is AppException appException)
        {
            statusCode = appException.StatusCode;
            error = appException.Message;
            message = "There was an error processing your request";
        }
        else
        {
            // If it's not an app exception, maybe it's something sensitive
            // So show it only in dev mode
            if (_hostEnvironment.IsDevelopment())
            {
                error = exception.InnerException?.Message ?? exception.Message;
            }
        }
        _logger.LogError("There was an error processing {Method} {Path}\n{Error}",
        httpContext.Request.Method, httpContext.Request.Path, error);

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