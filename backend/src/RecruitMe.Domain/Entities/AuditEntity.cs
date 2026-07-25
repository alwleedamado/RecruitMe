namespace RecruitMe.Domain.Entities;

public abstract class AuditEntity : EntityBase
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime DaleteAt { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
}