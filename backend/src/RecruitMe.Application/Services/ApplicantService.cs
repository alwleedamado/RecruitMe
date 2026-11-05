using AutoMapper;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Services
{
    public class ApplicantService(IMapper mapper, IUnitOfWork unitOfWork, IIdentityService identityService, ICurrentUserService currentUserService) : IApplicantService
    {

        private async Task<Applicant> GetApplicantAsync(CancellationToken ct)
        {
            var identityId = currentUserService.UserId!;
            var applicant = await unitOfWork.ApplicantRepository.GetApplicantByIdentity(identityId, ct);
            return applicant;
        }

        public async Task AddExperienceAsync(WorkExperienceDto workExperienceDto, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var entity = mapper.Map<WorkExperience>(workExperienceDto);
            applicant.WorkExperiences.Add(entity);
            await unitOfWork.SaveChangesAsync(ct);
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

        public async Task DeleteExperience(int id, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var work = applicant.WorkExperiences.Single(x => x.Id == id);
            applicant.WorkExperiences.Remove(work);
            await unitOfWork.SaveChangesAsync(ct);
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

        public async Task UpdateExperienceAsync(UpdateExperienceRequest workExperienceDto, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var work = applicant.WorkExperiences.Single(x => x.Id == workExperienceDto.Id);
            mapper.Map(workExperienceDto, work);
            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}
