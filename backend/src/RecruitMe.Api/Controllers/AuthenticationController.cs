using Microsoft.AspNetCore.Mvc;
using RecruitMe.Application.Authentication.Interfaces;
using RecruitMe.Application.Authentication.Models;

namespace RecruitMe.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _authenticationService.RegisterAsync(request);

        return Ok(new
        {
            Message = "Registration completed successfully."
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request);

        return Ok(response);
    }
}
