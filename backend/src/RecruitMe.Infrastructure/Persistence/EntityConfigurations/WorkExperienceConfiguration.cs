using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.EntityConfigurations
{
    internal class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> builder)
        {
            builder.HasIndex(w => new { w.Id, w.ApplicaId })
                .IsUnique();
            builder.Property(w => w.CompanyName)
                .IsRequired();
            builder.Property(w => w.Description)
                .IsRequired();
        }
    }
}
