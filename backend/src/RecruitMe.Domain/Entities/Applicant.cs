namespace RecruitMe.Domain.Entities;

public class Applicant : AuditEntity
{
    public required string IdentityId { get; set; }
    public List<Skill> Skills { get; set; } = [];
    public List<WorkExperience> WorkExperiences { get; set; } = [];
}
