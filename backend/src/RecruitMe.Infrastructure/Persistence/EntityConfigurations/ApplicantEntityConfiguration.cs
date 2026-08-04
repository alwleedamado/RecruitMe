using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.EntityConfigurations
{
    internal class ApplicantEntityConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasMany(a => a.Skills)
                .WithOne()
                .HasForeignKey(s => s.ApplicantId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(a => a.IdentityId)
                .IsUnique();
            builder.HasMany(a => a.WorkExperiences)
                .WithOne()
                .HasForeignKey(w => w.ApplicaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
