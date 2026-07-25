using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Infrastructure.Presistence.Repositories;

public class HrEntityConfiguration : IEntityTypeConfiguration<Hr>
{
    public void Configure(EntityTypeBuilder<Hr> builder)
    {
        builder.ToTable("Hrs");

        builder.HasKey(x => x.Id);
        
        builder.HasIndex("IdentityId")
            .IsUnique();

        builder.Property(x => x.IdentityId)
            .IsRequired();

        builder.Property(x => x.HiredDate)
            .IsRequired();

        builder.Property(x => x.Salary)
            .IsRequired();
    }
}