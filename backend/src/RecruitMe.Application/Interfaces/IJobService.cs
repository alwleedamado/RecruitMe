using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Interfaces;

public interface IJobService
{
    Task<JobPostingDto?> GetJobPostingAsync(int jobPostingId);
    Task<JobPostingDto> CreateJobPostingAsync(CreateJobPosting request, CancellationToken cancellationToken);
    Task DeleteJobAsync(int id);
}
