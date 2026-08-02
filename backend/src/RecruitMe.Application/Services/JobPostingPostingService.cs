using AutoMapper;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Services;

public class JobPostingPostingService(IUnitOfWork unitOfWork,IMapper mapper, ICurrentUserService currentUserService) : IJobPostingService
{
    public async Task<JobPostingDto?> GetJobPostingAsync(int jobPostingId)
    {
        var entity = await unitOfWork.JobPostingRepository.GetByIdAsync(jobPostingId);
        if (entity == null) return null;
        var mapped = mapper.Map<JobPostingDto>(entity);
        return mapped;
    }

    public async Task<JobPostingDto> CreateJobPostingAsync(CreateJobPosting request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<JobPosting>(request);
        entity.CreatedAt = DateTime.Now;
        entity.CreatedBy = currentUserService.UserId!;
        await unitOfWork.JobPostingRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<JobPostingDto>(entity);
    }

    public async Task DeleteJobAsync(int id)
    {
        var entity = await unitOfWork.JobPostingRepository.GetByIdAsync(id) ?? throw new NullReferenceException();
        unitOfWork.JobPostingRepository.Delete(entity);
    }

    public async Task<List<JobPostingDto>> GetAll()
    {
        var entities = await unitOfWork.JobPostingRepository.GetAllAsync();
        return mapper.Map<List<JobPostingDto>>(entities);
    }

    public async Task UpdateJobPosting(UpdateJobPosting request)
    {
        var entity = await unitOfWork.JobPostingRepository.GetByIdAsync(request.Id) ?? throw new NullReferenceException();
        mapper.Map(request, entity);
        unitOfWork.JobPostingRepository.Update(entity);
        await unitOfWork.SaveChangesAsync();
    }
}
