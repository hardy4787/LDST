﻿using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LDST.Application.Features.Authentication.Commands.DeleteUser;

public sealed class DeleteUserCommand : ICommand<Unit>
{
    public string UserName { get; set; } = null!;

    internal class Handler : ICommandHandler<DeleteUserCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;

        public Handler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByNameAsync(command.UserName)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return Unit.Value;
        }
    }
}