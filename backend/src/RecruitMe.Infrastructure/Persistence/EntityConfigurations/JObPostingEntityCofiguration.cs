using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Infrastructure.Persistence.EntityConfigurations;

public class JObPostingEntityCofiguration : IEntityTypeConfiguration<JobPosting>
{
    public void Configure(EntityTypeBuilder<JobPosting> builder)
    {
        builder.HasIndex(x => x.Title)
            .IsUnique();
        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(x => x.Location)
            .HasMaxLength(100);
        builder.Property(x => x.EndsOn)
            .IsRequired();
        builder.Property(x => x.JobType)
            .HasConversion<string>();
    }
}
