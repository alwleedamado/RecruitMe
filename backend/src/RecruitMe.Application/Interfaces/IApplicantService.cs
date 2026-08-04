using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Interfaces
{
    public interface IApplicantService
    {
        Task CreateApplicantAsync(CreateApplicant request, CancellationToken cancellationToken);
        Task<List<ApplicantDto>> GetAllApplicantsAsync();
        Task<ApplicantDto?> GetApplicantByIdAsync(int id);
        Task AddExperienceAsync(int id, WorkExperienceDto workExperienceDto);
    }
}
