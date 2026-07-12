namespace RecruitMe.Infrastructure.Authentication;

public class JwtToken
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }
}
