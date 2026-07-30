using AutoMapper;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Services;

public class JobPostingService(IUnitOfWork unitOfWork,IMapper mapper, ICurrentUserService currentUserService) : IJobService
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
}
