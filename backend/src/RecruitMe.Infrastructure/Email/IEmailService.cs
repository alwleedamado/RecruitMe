namespace RecruitMe.Infrastructure.Email;

public interface IEmailService
{
    Task SendAsync(
        string to,
        string subject,
        string body,
        bool isHtml = true);
}
