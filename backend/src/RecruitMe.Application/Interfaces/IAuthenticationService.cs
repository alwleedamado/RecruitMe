using RecruitMe.Application.DTOs;

namespace RecruitMe.Application.Interfaces;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task RegisterAsync(RegisterRequest request);
}
