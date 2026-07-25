namespace RecruitMe.Application.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }

    string? Email { get; }

    string? FullName { get; }

    bool IsAuthenticated { get; }

    bool IsInRole(string role);
}