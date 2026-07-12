using Microsoft.AspNetCore.Identity;
using RecruitMe.Application.Authentication.Interfaces;
using RecruitMe.Application.Authentication.Models;
using RecruitMe.Persistence.Identity;

namespace RecruitMe.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var validPassword = await _userManager.CheckPasswordAsync(
            user,
            request.Password);

        if (!validPassword)
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = await _jwtService.GenerateTokenAsync(user);

        var roles = await _userManager.GetRolesAsync(user);

        return new LoginResponse
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
            Email = user.Email!,
            FullName = user.FullName,
            Roles = roles
        };
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) != null)
            throw new ArgumentException("Email already exists.");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
            throw new ArgumentException(
                string.Join(Environment.NewLine,
                    result.Errors.Select(e => e.Description)));
    }
}
