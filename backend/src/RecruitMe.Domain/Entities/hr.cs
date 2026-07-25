namespace RecruitMe.Domain.Entities;

public class Hr : AuditEntity
{
    public required string IdentityId { get; set; }

    public DateTime HiredDate { get; set; }

    public decimal Salary { get; set; }
}