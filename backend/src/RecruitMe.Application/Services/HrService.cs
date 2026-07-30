using AutoMapper;
using RecruitMe.Application.Authentication.Interfaces;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Services;


public class HrService(IUnitOfWork unitOfWork,
 IIdentityService identityService,
 IMapper mapper,
  ICurrentUserService currentUserService) : IHrService
{

    public async Task<HrDto> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken)
    {
        var hr =  await unitOfWork.HrRepository.GetByIdentityIdAsync(identityId, cancellationToken);
        var mapped = mapper.Map<HrDto>(hr);
        return mapped;
    }

    public async Task<HrDto> CreateHrAsync(RegisterHrRequest request, CancellationToken cancellationToken)
    {
        var id = await identityService.RegisterHr(request, cancellationToken);
        var hr = new Hr
        {
            IdentityId = id,
            HiredDate = request.HireDate,
            Salary = request.Salary,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = currentUserService.UserId!
        };
        await unitOfWork.HrRepository.AddAsync(hr);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        var mapped = mapper.Map<HrDto>(hr);
        return mapped;
    }

    public async Task<HrDto> GetByIdAsync(int id)
    {
        var entity = await unitOfWork.HrRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        var identityUser = await identityService.GetUserByIdAsync(entity.IdentityId);
        var mapped =  mapper.Map<HrDto>(entity);
        mapped.FullName = identityUser.FullName;
        mapped.Email = identityUser.Email;
        return mapped;
    }

    public async Task<List<HrDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await unitOfWork.HrRepository.GetAllAsync();
        return mapper.Map<List<Hr>, List<HrDto>>(entities);
    }
}
