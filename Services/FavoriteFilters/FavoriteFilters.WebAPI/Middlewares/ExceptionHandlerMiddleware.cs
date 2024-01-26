using System.Net;
using FavoriteFilters.Application.Exceptions;

namespace FavoriteFilters.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly Dictionary<Type, HttpStatusCode> _statusCodes = new()
    {
        { typeof(NotExistsException), HttpStatusCode.NotFound }
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (_statusCodes.TryGetValue(exception.GetType(), out var statusCode))
            {
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    exception.Message
                });
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Internal server error"
                });

                _logger.LogError(exception, "Message: {Message}", exception.Message);
            }
        }
    }
}
