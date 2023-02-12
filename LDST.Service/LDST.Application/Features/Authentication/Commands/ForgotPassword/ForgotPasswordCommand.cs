using LDST.Domain.EFModels;
using LDST.Application.Abstractions;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using MediatR;
using LDST.Domain.Errors;
using Microsoft.AspNetCore.WebUtilities;
using LDST.Application.Interfaces.Services;
using LDST.Domain.Mail;

namespace LDST.Application.Features.Authentication.Commands.ForgotPassword;

public sealed class ForgotPasswordCommand : ICommand<Unit>
{
    public string Email { get; set; } = null!;

    public string ClientURI { get; set; } = null!;

    internal class Handler : ICommandHandler<ForgotPasswordCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailSender _emailSender;

        public Handler(UserManager<UserEntity> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<ErrorOr<Unit>> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            if((await _userManager.FindByEmailAsync(command.Email)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", command.Email }
            };
            var callback = QueryHelpers.AddQueryString(command.ClientURI, param);
            var message = new Message
            {
                To = new string[] { command.Email },
                Subject = "Reset password token",
                Content = callback
            };
            await _emailSender.SendEmailAsync(message);

            return Unit.Value;
        }
    }
}