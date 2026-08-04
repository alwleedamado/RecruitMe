namespace RecruitMe.Domain.Entities
{
    public  class Skill : EntityBase
    {
        public int ApplicantId { get; set; }
        public required string SkillName { get; set; }
    }
}
