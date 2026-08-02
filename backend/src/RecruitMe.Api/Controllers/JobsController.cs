using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController(IJobPostingService jobPostingService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GEtAll()
    {
        var ret = await jobPostingService.GetAll();
        return Ok(ret);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var ret = await jobPostingService.GetJobPostingAsync(id);
        if (ret == null) return NotFound("Job posting not found");
        return Ok(ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateJobPosting request,  CancellationToken cancellationToken)
    {
        var ret = await jobPostingService.CreateJobPostingAsync(request, cancellationToken);
        return Ok(ret);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateJobPosting request)
    {
        if (id != request.Id) return BadRequest();
        await jobPostingService.UpdateJobPosting(request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await jobPostingService.DeleteJobAsync(id);
        } catch(InvalidOperationException ex)
        {
            return NotFound("Job posting not found");
        }
        return NoContent();
    }
}
