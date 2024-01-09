using Hangfire;
using HangfireBasicAuthenticationFilter;

namespace FavoriteFilters.WebAPI.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
    {
        var user = configuration["HangfireConfiguration:User"];
        var password = configuration["HangfireConfiguration:Password"];
        
        ArgumentException.ThrowIfNullOrEmpty(user);
        ArgumentException.ThrowIfNullOrEmpty(password);
        
        app.UseHangfireDashboard(options: new DashboardOptions
        {
            Authorization = new[]
            {
                new HangfireCustomBasicAuthenticationFilter
                {
                    User = user,
                    Pass = password
                }
            }
        });
        
        return app;
    }
}
