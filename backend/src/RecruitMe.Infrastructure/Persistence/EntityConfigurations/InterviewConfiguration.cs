using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Infrastructure.Persistence.EntityConfigurations;

internal class InterviewConfiguration : IEntityTypeConfiguration<InterviewSchedule>
{
    public void Configure(EntityTypeBuilder<InterviewSchedule> builder)
    {
        builder.HasIndex(x => x.ShceduledDate);
    }
}
