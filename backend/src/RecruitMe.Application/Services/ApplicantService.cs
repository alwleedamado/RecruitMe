using AutoMapper;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Services
{
    public class ApplicantService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityService identityService) : IApplicantService
    {
        public async Task AddExperienceAsync(int id, WorkExperienceDto workExperienceDto)
        {
            var applicant = await unitOfWork.ApplicantRepository.GetByIdAsync(id) ?? throw new InvalidCastException("Applicant not found");
            var entity = mapper.Map<WorkExperience>(workExperienceDto);
            await unitOfWork.ApplicantRepository.AddExperienceAsync(id, entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CreateApplicantAsync(CreateApplicant request, CancellationToken cancellationToken)
        {
            var id = await identityService.RegisterApplicantAsync(request, cancellationToken);
            var applicant = new Applicant
            {
                IdentityId = id,
                CreatedAt = DateTime.Now,
            };
            await unitOfWork.ApplicantRepository.AddAsync(applicant);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ApplicantDto>> GetAllApplicantsAsync()
        {
            var entities = await unitOfWork.ApplicantRepository.GetAllAsync();
            var mapped = mapper.Map<List<ApplicantDto>>(entities);
            return mapped;
        }

        public async Task<ApplicantDto?> GetApplicantByIdAsync(int id)
        {
            var entity = await unitOfWork.ApplicantRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            return mapper.Map<ApplicantDto>(entity);
        }
    }
}
