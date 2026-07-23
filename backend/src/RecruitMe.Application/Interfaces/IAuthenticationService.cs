using RecruitMe.Application.Authentication.DTOs;

namespace RecruitMe.Application.Authentication.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task RegisterAsync(RegisterRequest request);
}
