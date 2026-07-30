using RecruitMe.Application.Interfaces.Repositories;

namespace RecruitMe.Application.Interfaces;

public interface IUnitOfWork
{
    public IHrRepository HrRepository { get; }
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
