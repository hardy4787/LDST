using LDST.Domain.Mail;

namespace LDST.Application.Interfaces.Services;

public interface IEmailSender
{
    void SendEmail(Message message);
    Task SendEmailAsync(Message message);
}
