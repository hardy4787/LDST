using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Application.Features.Profile.GetUserProfile;

public sealed class GetUserProfileQuery : IQuery<UserProfileDto>
{
    [FromRoute(Name = "userName")]
    public string UserName { get; set; } = null!;

    internal class Handler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IAppDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public Handler(IAppDbContext context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ErrorOr<UserProfileDto>> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByNameAsync(query.UserName)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            return new UserProfileDto(user.TitlePhotoPath, user.FirstName, user.LastName, new UserSettingsDto(user.TwoFactorEnabled));
        }
    }
}
