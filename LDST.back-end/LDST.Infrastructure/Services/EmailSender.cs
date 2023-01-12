using LDST.Application.Interfaces.Services;
using LDST.Domain.Mail;
using LDST.Infrastructure.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace LDST.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfig;

    public EmailSender(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public void SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    public async Task SendEmailAsync(Message message)
    {
        var mailMessage = CreateEmailMessage(message);
        await SendAsync(mailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
        emailMessage.To.AddRange(message.To.Select(x => new MailboxAddress("email", x)));
        emailMessage.Subject = message.Subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };
        if (message.Attachments != null && message.Attachments.Any())
        {
            byte[] fileBytes;
            foreach (var attachment in message.Attachments)
            {
                using (var ms = new MemoryStream())
                {
                    attachment.Content.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
                bodyBuilder.Attachments.Add(attachment.Name, fileBytes, ContentType.Parse(attachment.ContentType));
            }
        }
        emailMessage.Body = bodyBuilder.ToMessageBody();
        return emailMessage;
    }

    private void Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
        client.Send(mailMessage);
        client.Disconnect(true);
        client.Dispose();
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
        await client.SendAsync(mailMessage);
        await client.DisconnectAsync(true);
        client.Dispose();
    }
}