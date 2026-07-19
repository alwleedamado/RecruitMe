using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RecruitMe.Infrastructure.Options;

namespace RecruitMe.Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly EmailOptions _options = options.Value;

    public async Task SendAsync(
        string to,
        string subject,
        string body,
        bool isHtml = true)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            _options.FromName,
            _options.FromEmail));

        email.To.Add(MailboxAddress.Parse(to));

        email.Subject = subject;

        email.Body = new BodyBuilder
        {
            HtmlBody = isHtml ? body : null,
            TextBody = isHtml ? null : body
        }.ToMessageBody();

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _options.Host,
            _options.Port,
            _options.EnableSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.None);

        await smtp.AuthenticateAsync(
            _options.Username,
            _options.Password);

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}
