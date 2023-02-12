using LDST.Api.Abstractions;
using LDST.Application.Features.Location.Queries.GetSupportedCities;
using LDST.Application.Features.Profile.Commands.UpdateProfile;
using LDST.Application.Features.Profile.Commands.UpdateProfileImage;
using LDST.Application.Features.Profile.Queries.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

[Route("[controller]")]
public class ProfileController : ApiController
{
    private readonly ISender _mediator;
    public ProfileController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetUserProfile([FromQuery] GetUserProfileQuery request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(UpdateProfileCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPut("title-image")]
    public async Task<IActionResult> UpdateProfileImage([FromForm] UpdateProfileImageCommand request) =>
        GetHandledResult(await _mediator.Send(request));
}
