using RecruitMe.Infrastructure.Identity;

namespace RecruitMe.Infrastructure.Authentication;

public interface IJwtService
{
    Task<JwtToken> GenerateTokenAsync(ApplicationUser user);
}
