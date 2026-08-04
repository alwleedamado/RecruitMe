using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Interfaces.Repositories
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        Task AddExperienceAsync(int id, WorkExperience entity);
    }
}
