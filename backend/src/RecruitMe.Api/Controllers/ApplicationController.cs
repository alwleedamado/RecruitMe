using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;
using RecruitMe.Infrastructure.Identity;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/applications")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var applications = await applicationService.GetApplicationsAsync(ct);
        return Ok(applications);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetApplication(int id, CancellationToken ct)
    {
        var application = await applicationService.GetApplicationByIdAsync(id, ct);
        return Ok(application);
    }

    [HttpPost]
    [Authorize(Roles.Applicant)]
    public async Task<IActionResult> CreateApplication(CreateApplication req, CancellationToken ct)
    {
        var application = await applicationService.CreateApplicationAsync(req, ct);
        return Ok(application);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles.Applicant)]
    public async Task<IActionResult> DeleteApplication(int id, CancellationToken ct)
    {
        await applicationService.DeleteApplicationAsync(id, ct);
        return Ok();
    }

    [HttpPost("{id:int}/reject")]
    [Authorize(Roles.HR)]
    public async Task<IActionResult> RejectApplication(int id, CancellationToken ct)
    {
        await applicationService.RejectApplicationAsync(id, ct);
        return Ok();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles.Applicant)]
    public async Task<IActionResult> UpdateApplication(int id, [FromBody] UpdateApplicationRequest request, CancellationToken ct)
    {
        if (id != request.Id)
            return BadRequest();
        await applicationService.UpdateApplicationAsync(request, ct);
        return NoContent();
    }

    [HttpPost("{id:int}/submit")]
    [Authorize(Roles.Applicant)]
    public async Task<IActionResult> SubmitApplication(int id, CancellationToken ct)
    {
        await applicationService.SubmitApplicationAsync(id, ct);
        return NoContent();
    }
}
