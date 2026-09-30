using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RecruitMe.Application.DTOs;
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

    public async Task<List<HrDto>> GetEmployees(CancellationToken cancellationToken)
    {
        var entities = from hr in Context.Hrs
                       join user in Context.Users on hr.IdentityId equals user.Id
                       select new HrDto
                       {
                           Id = hr.Id,
                           FullName = user.FullName,
                           Email = user.Email!,
                           HiredDate = hr.HiredDate,
                           Salary = hr.Salary
                       };
        return await entities.ToListAsync(cancellationToken);
    }
}
