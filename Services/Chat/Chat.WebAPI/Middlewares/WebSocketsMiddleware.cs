namespace Chat.WebAPI.Middlewares;

public class WebSocketsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var request = context.Request;

        if (request.Path.StartsWithSegments("/messages", StringComparison.OrdinalIgnoreCase) &&
            request.Query.TryGetValue("access_token", out var accessToken))
        {
            request.Headers.Append("Authorization", $"Bearer {accessToken}");
        }

        await next(context);
    }
}
