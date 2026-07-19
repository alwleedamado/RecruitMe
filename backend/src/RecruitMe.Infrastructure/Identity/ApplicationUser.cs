using Microsoft.AspNetCore.Identity;

namespace RecruitMe.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
