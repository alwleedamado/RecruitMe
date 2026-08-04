using RecruitMe.Application.Interfaces.Repositories;

namespace RecruitMe.Application.Interfaces;

public interface IUnitOfWork
{
    public IHrRepository HrRepository { get; }
    public IJobPostingRepository JobPostingRepository { get; }
    public IApplicantRepository ApplicantRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
