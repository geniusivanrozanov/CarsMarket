using IdentityService.Application.DataInitializers;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Application.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task ApplyApplicationLayer(this IServiceProvider services)
    {
        await services.SeedIdentityDataAsync();
    }
    
    private static async Task SeedIdentityDataAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await IdentityDataInitializer.SeedDataAsync(userManager, roleManager);
    }
}
