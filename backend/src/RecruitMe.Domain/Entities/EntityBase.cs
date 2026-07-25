namespace RecruitMe.Domain.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public Boolean IsDeleted { get; set; }
}