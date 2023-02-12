using LDST.Domain.EFModels;
using LDST.Application.Abstractions;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using MediatR;
using LDST.Domain.Errors;

namespace LDST.Application.Features.Authentication.Commands.Register;

public sealed class ResetPasswordCommand : ICommand<Unit>
{
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;

    internal class Handler : ICommandHandler<ResetPasswordCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;

        public Handler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Unit>> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByEmailAsync(command.Email)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var result = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return Unit.Value;
        }
    }
}