using Microsoft.AspNetCore.Identity;

namespace RecruitMe.Persistence.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
