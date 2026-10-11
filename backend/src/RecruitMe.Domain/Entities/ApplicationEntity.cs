namespace RecruitMe.Domain.Entities;

public  class ApplicationEntity : AuditEntity
{
    public int ApplicantId { get; set; }
    public int JobId { get; set; }
    public JobPosting? JobPosting { get; set; }
    public ApplicationStatus Status { get; set; }
}

public enum ApplicationStatus
{
    Draft,
    Submited,
    Accepted,
    Rejected
}
