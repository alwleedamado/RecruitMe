using Microsoft.EntityFrameworkCore;
using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Infrastructure.Persistence.Repositories
{
    public class ApplicationRepository : RepositoryBase<ApplicationEntity>, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ApplicationEntity?> GetByApplicantId(int applicantId, int applicaitonId)
        {
            var application = await Context.Applications.FirstOrDefaultAsync(x => x.ApplicantId == applicantId && x.Id == applicaitonId);
            return application;
        }
    }
}
