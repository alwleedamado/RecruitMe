using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RecruitMe.Application.Interfaces;

namespace RecruitMe.Infrastructure.Services;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    private readonly ClaimsPrincipal? _user =
        httpContextAccessor.HttpContext?.User;

    public string? UserId =>
        _user?.FindFirstValue(
            ClaimTypes.NameIdentifier);

    public string? Email =>
        _user?.FindFirstValue(
            ClaimTypes.Email);

    public string? FullName =>
        _user?.FindFirstValue("fullName");

    public bool IsAuthenticated =>
        _user?.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string role) =>
        _user?.IsInRole(role) ?? false;
}