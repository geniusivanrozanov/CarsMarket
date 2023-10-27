using System.Net;
using IdentityService.Application.Exceptions;

namespace IdentityService.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
{
    private readonly Dictionary<Type, HttpStatusCode> _statusCodes = new()
    {
        { typeof(NotExistsException), HttpStatusCode.NotFound },
        { typeof(LoginFailedException), HttpStatusCode.BadRequest },
        { typeof(RegistrationFailedException), HttpStatusCode.Conflict }
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
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

                logger.LogError(exception, "Message: {Message}", exception.Message);
            }
        }
    }
}
