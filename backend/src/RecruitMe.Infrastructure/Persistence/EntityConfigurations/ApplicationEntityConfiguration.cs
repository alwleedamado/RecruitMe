using Microsoft.EntityFrameworkCore;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.EntityConfigurations
{
    internal class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.HasIndex(a => new { a.ApplicantId, a.JobId }).IsUnique();
        }
    }
}
