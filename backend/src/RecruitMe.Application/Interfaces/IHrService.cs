using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Interfaces;

public interface IHrService
{
    Task<HrDto> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken);
    Task<HrDto> CreateHrAsync(RegisterHrRequest hr, CancellationToken cancellationToken);
    Task<HrDto> GetByIdAsync(int id);
    Task<List<HrDto>> GetAllAsync(CancellationToken cancellationToken);
}
