using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using RecruitMe.Infrastructure.Persistence;
using RecruitMe.Infrastructure.Persistence.Repositories;

namespace RecruitMe.Infrastructure.Persistence.Repositories;

public class HrRepository(
    ApplicationDbContext context)
    : RepositoryBase<Hr>(context),
      IHrRepository
{
    public Task<Hr> GetByIdentityIdAsync(string identityId)
    {
        throw new NotImplementedException();
    }
}