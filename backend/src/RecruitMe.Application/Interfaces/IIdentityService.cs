using RecruitMe.Application.DTOs;

namespace RecruitMe.Application.Interfaces;

public interface IIdentityService
{
    Task<string> RegisterApplicantAsync(CreateApplicant request, CancellationToken cancellationToken);
    Task<User> GetUserByIdAsync(string id);
    Task<string> RegisterHr(RegisterHrRequest request, CancellationToken cancellationToken);
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
