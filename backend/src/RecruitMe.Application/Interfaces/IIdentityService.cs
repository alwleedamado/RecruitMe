using RecruitMe.Application.Authentication.DTOs;
using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Authentication.Interfaces;

public interface IIdentityService
{
    Task RegisterApplicantAsync(RegisterRequest request);
    Task<User> GetUserByIdAsync(string id);
    Task<string> RegisterHr(RegisterHrRequest request, CancellationToken cancellationToken);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
