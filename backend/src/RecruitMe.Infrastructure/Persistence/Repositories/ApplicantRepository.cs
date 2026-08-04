using Microsoft.EntityFrameworkCore;
using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.Repositories
{
    internal class ApplicantRepository(ApplicationDbContext context) : RepositoryBase<Applicant>(context), IApplicantRepository
    {
        public async Task AddExperienceAsync(int id, WorkExperience entity)
        {
            var applicant = await context.Applicants.SingleAsync(a => a.Id == id);
            applicant.WorkExperiences.Add(entity);
        }
    }
}
