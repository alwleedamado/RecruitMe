using Microsoft.EntityFrameworkCore;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.Repositories;

internal class ApplicantRepository(ApplicationDbContext context) : RepositoryBase<Applicant>(context), IApplicantRepository
{
    public async Task AddExperienceAsync(int id, WorkExperience entity)
    {
        var applicant = await Context.Applicants.SingleAsync(a => a.Id == id);
        applicant.WorkExperiences.Add(entity);
    }

    public async Task<Applicant> GetApplicantByIdentity(string identity, CancellationToken cancellationToken)
    {
        var applicant = await Context.Applicants
            .Include(x => x.Applications)
            .Include(x => x.WorkExperiences)
            .SingleAsync(x => x.IdentityId == identity, cancellationToken);
        return applicant;
    }
}
