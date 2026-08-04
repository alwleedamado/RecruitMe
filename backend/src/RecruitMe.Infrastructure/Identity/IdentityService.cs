using Microsoft.AspNetCore.Identity;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;
using RecruitMe.Infrastructure.Authentication;

namespace RecruitMe.Infrastructure.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    JwtService jwtService)
    : IIdentityService
{
    public async Task<string> RegisterApplicantAsync(CreateApplicant request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(
            request.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
        };

        var result = await userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }

        if (!await roleManager.RoleExistsAsync(Roles.Applicant))
        {
            await roleManager.CreateAsync(
                new IdentityRole(Roles.Applicant));
        }

        var roleResult = await userManager.AddToRoleAsync(
            user,
            Roles.Applicant);

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(
                ", ",
                roleResult.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }
        return user.Id;
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id)
                    ?? throw new InvalidOperationException();
        return new User(user.FullName, user.Email!);
    }

    public async Task<string> RegisterHr(RegisterHrRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.FindByEmailAsync(
            request.Email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }

        if (!await roleManager.RoleExistsAsync(Roles.HR))
        {
            await roleManager.CreateAsync(
                new IdentityRole(Roles.HR));
        }

        var roleResult = await userManager.AddToRoleAsync(
            user,
            Roles.HR);

        if (!roleResult.Succeeded)
        {
            var errors = string.Join(
                ", ",
                roleResult.Errors.Select(x => x.Description));

            throw new InvalidOperationException(errors);
        }

        return user.Id;
    }

    public async Task<LoginResponse> LoginAsync(
        LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(
            request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var validPassword = await userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!validPassword)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var roles = await userManager.GetRolesAsync(user);

        var (token, expiresAt) =
            jwtService.GenerateToken(user, roles);

        return new LoginResponse
        {
            AccessToken = token,

            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles

        };
    }

}
