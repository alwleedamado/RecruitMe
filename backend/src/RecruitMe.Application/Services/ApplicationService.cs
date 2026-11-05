using AutoMapper;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Services
{
    public class ApplicationService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper) : IApplicationService
    {
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private async Task<Applicant> GetApplicantAsync(CancellationToken ct)
        {
            var identityId = _currentUserService.UserId!;
            var applicant = await _unitOfWork.ApplicantRepository.GetApplicantByIdentity(identityId, ct);
            return applicant;
        }

        public async Task<ApplicationDto> CreateApplicationAsync(CreateApplication request, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var mapped = mapper.Map<ApplicationEntity>(request);
            mapped.CreatedBy = _currentUserService.UserId!;
            applicant.Applications.Add(mapped);
            await _unitOfWork.SaveChangesAsync(ct);
            var ret = mapper.Map<ApplicationDto>(mapped);
            return ret;
        }

        public async Task DeleteApplicationAsync(int id, CancellationToken ct)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id)
                ?? throw new ArgumentNullException($"Application with id({id}) not found.");
            _unitOfWork.ApplicationRepository.Delete(application);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<ApplicationDto> GetApplicationByIdAsync(int id, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var application = await _unitOfWork.ApplicationRepository.GetByApplicantId(applicant.Id, id);
            var mapped = mapper.Map<ApplicationDto>(application);
            return mapped;
        }

        public async Task<List<ApplicationDto>> GetApplicationsAsync(CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var applicatinos = applicant.Applications;
            var mapped = mapper.Map<List<ApplicationDto>>(applicant);
            return mapped;
        }

        public async Task RejectApplicationAsync(int id, CancellationToken ct)
        {
            var application = await _unitOfWork.ApplicationRepository.GetByIdAsync(id)
                ?? throw new ArgumentNullException($"Application with id({id}) not found.");
            application.Status = ApplicationStatus.Rejected;
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task UpdateApplicationAsync(UpdateApplicationRequest request, CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var application = await _unitOfWork.ApplicationRepository.GetByApplicantId(applicant.Id, request.Id);
            if (application == null || application.Status != ApplicationStatus.Draft)
                throw new InvalidOperationException("Application should be in the draft state to edited.");
            mapper.Map(request, application);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task SubmitApplicationAsync(int id,  CancellationToken ct)
        {
            var applicant = await GetApplicantAsync(ct);
            var application = await _unitOfWork.ApplicationRepository.GetByApplicantId(applicant.Id, id);
            if (application == null || application.Status != ApplicationStatus.Draft)
                throw new InvalidOperationException("Application should be in the draft state to submited.");
            application.Status = ApplicationStatus.Submited;
            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}
