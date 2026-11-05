using RecruitMe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitMe.Application.Interfaces;

public interface IApplicationService
{
    Task<List<ApplicationDto>> GetApplicationsAsync(CancellationToken ct);
    Task<ApplicationDto> GetApplicationByIdAsync(int id, CancellationToken ct);
    Task<ApplicationDto> CreateApplicationAsync(CreateApplication request, CancellationToken ct);
    Task DeleteApplicationAsync(int id, CancellationToken ct);
    Task RejectApplicationAsync(int id, CancellationToken ct);
    Task UpdateApplicationAsync(UpdateApplicationRequest request, CancellationToken ct);
    public Task SubmitApplicationAsync(int id, CancellationToken ct);
}
