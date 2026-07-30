using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace RecruitMe.Infrastructure.Persistence.Repositories;

public class HrRepository(
    ApplicationDbContext context)
        : RepositoryBase<Hr>(context),
      IHrRepository
{
    public async Task<Hr?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken)
    {
        return await Context.Hrs.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
}