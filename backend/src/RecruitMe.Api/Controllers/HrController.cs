using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/hr")]
public class HrController(IHrService hrService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var ret = await hrService.GetAllAsync(cancellationToken);
        return Ok(ret);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost()]
    public async Task<ActionResult> Create(
        RegisterHrRequest request,
        CancellationToken cancellationToken)
    {
        var dto = await hrService.CreateHrAsync(request, cancellationToken);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ret = await hrService.GetByIdAsync(id);
        return Ok(ret);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHr hr)
    {
        return Ok();
    }
}
