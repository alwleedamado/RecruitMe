namespace RecruitMe.Domain.Entities
{
    public class WorkExperience : AuditEntity
    {
        public required string CompanyName { get; set; }
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ApplicaId { get; set; }
    }
}
