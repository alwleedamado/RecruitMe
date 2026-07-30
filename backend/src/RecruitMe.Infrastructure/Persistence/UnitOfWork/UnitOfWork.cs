using RecruitMe.Application.Interfaces;
using RecruitMe.Application.Interfaces.Repositories;
using RecruitMe.Infrastructure.Persistence.Repositories;

namespace RecruitMe.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public IHrRepository HrRepository
    {
        get => field ?? new HrRepository(_context);
        set;
    }

    public IJobPostingRepository JobPostingRepository
    {
        get => field ?? new JobPostingRepository(_context);
        set;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {

            throw;
        }
    }
}
