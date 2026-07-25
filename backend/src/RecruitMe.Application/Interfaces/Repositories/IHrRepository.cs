using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Interfaces.Repositories;
public interface IHrRepository : IRepository<Hr>
{
    Task<Hr> GetByIdentityIdAsync(string identityId);
}