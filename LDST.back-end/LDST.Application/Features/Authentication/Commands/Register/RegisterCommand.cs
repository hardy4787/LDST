using LDST.Domain.EFModels;
using LDST.Application.Abstractions;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using MediatR;
using LDST.Application.Interfaces.Services;
using LDST.Domain.Mail;
using Microsoft.AspNetCore.WebUtilities;
using LDST.Domain.Errors;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LDST.Application.Extensions;

namespace LDST.Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommand : ICommand<Unit>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string ClientURI { get; set; } = null!;

    internal class Handler : ICommandHandler<RegisterCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailSender _emailSender;

        public Handler(UserManager<UserEntity> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<ErrorOr<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var user = new UserEntity { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email };

            user.UserName = $"{user.FirstName}-{user.LastName}".ToLower();

            if ((await _userManager.FindByNameAsync(user.UserName)) is not null)
            {
                user.UserName += DateTime.Now.GetUniqueId();
            }
            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                {"token", token },
                {"email", user.Email }
            };
            var callback = QueryHelpers.AddQueryString(command.ClientURI, param);
            var message = new Message
            {
                To = new string[] { user.Email },
                Subject = "Email Confirmation token",
                Content = callback,
                Attachments = null
            };
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(user, "Guest");

            return Unit.Value;
        }

        private static string GetUniqueId()
        {
            var ticks = new DateTime(2016, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            return ans.ToString("x");
        }
    }
}