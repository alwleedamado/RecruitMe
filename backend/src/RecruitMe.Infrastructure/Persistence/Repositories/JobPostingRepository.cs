using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Infrastructure.Persistence.Repositories;

public class JobPostingRepository(ApplicationDbContext context) : RepositoryBase<JobPosting>(context), IJobPostingRepository
{

}
