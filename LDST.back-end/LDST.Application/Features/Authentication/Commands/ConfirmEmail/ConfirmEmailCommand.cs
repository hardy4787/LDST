using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LDST.Application.Features.Authentication.Commands.ConfirmEmail;

public sealed class ConfirmEmailCommand : ICommand<Unit>
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;

    internal class Handler : ICommandHandler<ConfirmEmailCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;

        public Handler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Unit>> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByEmailAsync(command.Email)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var result = await _userManager.ConfirmEmailAsync(user, command.Token);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return Unit.Value;
        }
    }
}