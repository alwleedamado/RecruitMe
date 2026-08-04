using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.DTOs;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController(
    IHrService hrService, IApplicantService applicantService,
    IIdentityService identityService)
    : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(CreateApplicant request, CancellationToken cancellationToken)
    {
        await applicantService.CreateApplicantAsync(request, cancellationToken);

        return Ok(new
        {
            Message = "Applicant registered successfully."
        });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request)
    {
        var response = await identityService.LoginAsync(request);

        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier)
                ?.Value,

            Email = User.FindFirst(
                System.Security.Claims.ClaimTypes.Email)
                ?.Value,

            Name = User.FindFirst(
                System.Security.Claims.ClaimTypes.Name)
                ?.Value,

            Roles = User.Claims
                .Where(x =>
                    x.Type ==
                    System.Security.Claims.ClaimTypes.Role)
                .Select(x => x.Value)
                .ToList()
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register-hr")]
    public async Task<ActionResult> RegisterHr(
        RegisterHrRequest request,
        CancellationToken cancellationToken)
    {
        var dto = await hrService.CreateHrAsync(request, cancellationToken);
        return Ok(dto);
    }
}
