using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Features.Profile.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Profile.Commands.UpdateProfile;

public sealed class UpdateProfileCommand : ICommand<UpdateProfileResponse>
{
    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public UserSettingsDto Settings { get; set; } = null!;

    public sealed class Handler : ICommandHandler<UpdateProfileCommand, UpdateProfileResponse>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IAppDbContext _context;

        public Handler(UserManager<UserEntity> userManager, IAppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<ErrorOr<UpdateProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.TwoFactorEnabled = request.Settings.IsTwoFactorEnabled;

            var newUserName = $"{user.FirstName}-{user.LastName}".ToLower();
            user.UserName = newUserName;

            if ((user.UserName != newUserName && (await _userManager.FindByNameAsync(newUserName)) is not null))
            {
                user.UserName += DateTime.Now.GetUniqueId();
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return new UpdateProfileResponse(user.UserName);
        }
    }
}
