using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.DTOs;

public class JobPostingDto
{
    public required string Title { get; set; }
    public required  string Description { get; set; }
    public required string Requirements { get; set; }
    public required string JobType { get; set; }
    public string? Location { get; set; }
    public DateTime EndsOn { get; set; }
}

public class CreateJobPosting
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Requirements { get; set; }
    public required JobType JobType { get; set; }
    public string? Location { get; set; }
    public DateTime EndsOn { get; set; }
}
