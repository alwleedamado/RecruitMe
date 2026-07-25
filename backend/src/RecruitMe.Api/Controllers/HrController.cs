using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/hr")]
public class HrController : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> Create(int id, [FromBody] CreateHr hr)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHr hr)
    {
        return Ok();
    }
}