using IdentityService.Domain.Contracts;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace IdentityService.Infra.Services;

public class EmailSender(string smtpHost, int smtpPort, string smtpUser, string smtpPass) : IEmailSender
{
    public async Task SendEmailAsync(SendEmailRequest sendMailRequest)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("FrameUp", smtpUser));

        message.To.Add(new MailboxAddress("", sendMailRequest.Email));

        message.Subject = sendMailRequest.Subject;

        message.Body = new TextPart("html")
        {
            Text = sendMailRequest.Body
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(smtpUser, smtpPass);

        await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}
