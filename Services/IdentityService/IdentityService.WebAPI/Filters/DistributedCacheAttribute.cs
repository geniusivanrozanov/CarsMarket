using System.Text;
using System.Text.Json;
using IdentityService.WebAPI.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace IdentityService.WebAPI.Filters;

public class DistributedCacheAttribute : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(
        ResourceExecutingContext context,
        ResourceExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method != "GET")
        {
            await next();

            return;
        }

        var httpContext = context.HttpContext;
        var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
        var cacheOptions = httpContext.RequestServices.GetRequiredService<IOptions<DistributedCacheOptions>>().Value;
        var cacheKey = GenerateCacheKey(httpContext.Request);

        var cachedResponse = await cache.GetAsync(cacheKey);
        if (cachedResponse is not null)
        {
            context.Result = JsonSerializer.Deserialize<OkObjectResult>(cachedResponse);

            return;
        }

        var executedContext = await next();
        if (executedContext.Result is not OkObjectResult result) return;
        var resultString = JsonSerializer.SerializeToUtf8Bytes(result);
        await cache.SetAsync(cacheKey, resultString, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheOptions.ExpirationMinutes)
        });
    }

    private static string GenerateCacheKey(HttpRequest request)
    {
        var cacheKeyBuilder = new StringBuilder(request.Path.ToString().ToLower());
        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            cacheKeyBuilder.Append($"&{key.ToLower()}={value.ToString().ToLower()}");

        return cacheKeyBuilder.ToString();
    }
}
