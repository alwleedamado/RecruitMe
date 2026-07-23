using RecruitMe.Application.Authentication.DTOs;

namespace RecruitMe.Application.Authentication.Interfaces;

public interface IIdentityService
{
    Task RegisterApplicantAsync(RegisterRequest request);

    Task<LoginResponse> LoginAsync(LoginRequest request);
}