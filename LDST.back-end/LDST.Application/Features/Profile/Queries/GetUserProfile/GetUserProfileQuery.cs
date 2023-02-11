using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Features.Playground.Queries.GetPlaygroundsByHostId;
using LDST.Application.Features.Profile.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Application.Features.Profile.Queries.GetUserProfile;

public sealed class GetUserProfileQuery : IQuery<UserProfileDto>
{
    [FromRoute(Name = "userName")]
    public string UserName { get; set; } = null!;

    internal class Handler : IQueryHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly ISender _mediator;
        private readonly IAppDbContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public Handler(IAppDbContext context, UserManager<UserEntity> userManager, ISender mediator)
        {
            _context = context;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<ErrorOr<UserProfileDto>> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(query.UserName) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var playgrounds = await _mediator.Send(new GetPlaygroundsByHostIdQuery() { HostId = user.Id }, cancellationToken);

            if(playgrounds.IsError)
            {
                return playgrounds.Errors;
            }

            return new UserProfileDto(
                user.TitlePhotoPath,
                user.FirstName, 
                user.LastName, 
                new UserSettingsDto(user.TwoFactorEnabled), 
                playgrounds: playgrounds.Value);
        }
    }
}
