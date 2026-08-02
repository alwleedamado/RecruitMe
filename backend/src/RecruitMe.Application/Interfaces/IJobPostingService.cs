using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Interfaces;

public interface IJobPostingService
{
    Task<JobPostingDto?> GetJobPostingAsync(int jobPostingId);
    Task<JobPostingDto> CreateJobPostingAsync(CreateJobPosting request, CancellationToken cancellationToken);
    Task DeleteJobAsync(int id);
    Task<List<JobPostingDto>> GetAll();
    Task UpdateJobPosting(UpdateJobPosting request);
}
