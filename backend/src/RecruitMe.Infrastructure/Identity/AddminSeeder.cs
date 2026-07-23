using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace RecruitMe.Infrastructure.Identity;

public static class AdminSeeder
{
    private const string AdminEmail = "admin@recruitme.com";
    private const string AdminPassword = "Admin123!";

    public static async Task SeedAsync(
        IServiceProvider serviceProvider)
    {
        var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Ensure Admin role exists
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
        {
            var roleResult = await roleManager.CreateAsync(
                new IdentityRole(Roles.Admin));

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create {Roles.Admin} role: " +
                    string.Join(
                        ", ",
                        roleResult.Errors.Select(x => x.Description)));
            }
        }

        // Check whether admin already exists
        var admin = await userManager.FindByEmailAsync(AdminEmail);

        if (admin is not null)
        {
            // Make sure the existing user has the Admin role
            if (!await userManager.IsInRoleAsync(
                    admin,
                    Roles.Admin))
            {
                await userManager.AddToRoleAsync(
                    admin,
                    Roles.Admin);
            }

            return;
        }

        // Create Admin user
        admin = new ApplicationUser
        {
            UserName = AdminEmail,
            Email = AdminEmail,
            FullName = "System Administrator",
            EmailConfirmed = true
        };

        var userResult = await userManager.CreateAsync(
            admin,
            AdminPassword);

        if (!userResult.Succeeded)
        {
            throw new InvalidOperationException(
                "Failed to create admin user: " +
                string.Join(
                    ", ",
                    userResult.Errors.Select(x => x.Description)));
        }

        // Assign Admin role
        var roleAssignmentResult =
            await userManager.AddToRoleAsync(
                admin,
                Roles.Admin);

        if (!roleAssignmentResult.Succeeded)
        {
            throw new InvalidOperationException(
                "Failed to assign Admin role: " +
                string.Join(
                    ", ",
                    roleAssignmentResult.Errors.Select(
                        x => x.Description)));
        }
    }
}