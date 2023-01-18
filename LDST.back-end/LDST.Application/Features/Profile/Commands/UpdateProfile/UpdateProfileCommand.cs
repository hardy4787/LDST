using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LDST.Application.Features.Profile.Commands.UpdateProfile;

public sealed class UpdateProfileCommand : ICommand<Unit>
{
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public sealed class Handler : ICommandHandler<UpdateProfileCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileManager _fileManager;

        public Handler(IFileManager fileManager, UserManager<UserEntity> userManager)
        {
            _fileManager = fileManager;
            _userManager = userManager;
        }
        public async Task<ErrorOr<Unit>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByNameAsync(request.UserName)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            user.UserName = $"{user.FirstName}-{user.LastName}".ToLower();

            if ((await _userManager.FindByNameAsync(user.UserName)) is not null)
            {
                user.UserName += DateTime.Now.GetUniqueId();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return Unit.Value;
        }
    }
}
