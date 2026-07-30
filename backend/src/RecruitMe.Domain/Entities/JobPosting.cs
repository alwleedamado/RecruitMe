namespace RecruitMe.Domain.Entities;

public class JobPosting : AuditEntity
{
    public required string Title { get; set; }
    public required  string Description { get; set; }
    public required string Requirements { get; set; }
    public required JobType JobType { get; set; }
    public string? Location { get; set; }
    public DateTime EndsOn { get; set; }
}

public enum JobType
{
    OnSite,
    Hybrid,
    Remote
}
