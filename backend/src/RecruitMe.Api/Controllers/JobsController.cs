using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController(IJobService jobService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GEtAll() {}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var ret = await jobService.GetJobPostingAsync(id);
        if (ret == null) return NotFound("Job posting not found");
        return Ok(ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateJobPosting request,  CancellationToken cancellationToken)
    {
        var ret = await jobService.CreateJobPostingAsync(request, cancellationToken);
        return Ok(ret);
    }
}
