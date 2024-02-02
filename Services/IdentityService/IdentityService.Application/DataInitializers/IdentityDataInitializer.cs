using IdentityService.Domain.Constants;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.DataInitializers;

public static class IdentityDataInitializer
{
    public static async Task SeedDataAsync(UserManager<UserEntity> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        await SeedRolesAsync(roleManager);

        await SeedUsersAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        var roles = Roles.GetAllRoles();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
    
    private static async Task SeedUsersAsync(UserManager<UserEntity> userManager)
    {
        var users = new (UserEntity entity, string password, string role)[]
        {
            (new()
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@temp.com",
                UserName = "admin@temp.com"
            }, "Qwerty12#", Roles.Admin),
            (new()
            {
                FirstName = "User",
                LastName = "User",
                Email = "user@temp.com",
                UserName = "user@temp.com"
            }, "Qwerty12#", Roles.User),
            (new()
            {
                FirstName = "Moderator",
                LastName = "Moderator",
                Email = "moderator@temp.com",
                UserName = "moderator@temp.com"
            }, "Qwerty12#", Roles.Moderator)
        };

        foreach (var user in users)
        {
            var existingUser = await userManager.FindByEmailAsync(user.entity.Email!);
            
            if (existingUser is null)
            {
                var result = await userManager.CreateAsync(user.entity, user.password);
                
                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.FailedToCreateUser, string.Join(", ", result.Errors.Select(e => e.Description))));
                }
            }

            if (!await userManager.IsInRoleAsync(existingUser ?? user.entity, user.role))
            {
                var result = await userManager.AddToRoleAsync(existingUser ?? user.entity, user.role);
                
                if (!result.Succeeded)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.FailedToAddUserToRole, string.Join(", ", result.Errors.Select(e => e.Description))));
                }
            }
        }
    }
}
