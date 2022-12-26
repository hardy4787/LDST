using LDST.Api.Abstractions;
using LDST.Application.Features.Playground.Commands.CreatePlayground;
using LDST.Application.Features.Playground.Commands.CreateTimeSlots;
using LDST.Application.Features.Playground.Commands.UploadTitleImage;
using LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

[Route("[controller]")]
public class PlaygroundsController : ApiController
{
    private readonly ISender _mediator;
    public PlaygroundsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("hosts/{hostId}")]
    public async Task<IActionResult> CreatePlayground([FromQuery] CreatePlaygroundCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("title-photo")]
    public async Task<IActionResult> UploadTitleImage([FromForm] UploadTitleImageCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpGet("cities/{cityId}/sports/{sportId}")]
    public async Task<IActionResult> GetPlaygroundsByCity([FromQuery] GetPlaygroundsByCityQuery request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("{playgroundId}/timeslots")]
    public async Task<IActionResult> CreateTimeSlots([FromQuery] CreateTimeSlotsCommand request) =>
        GetHandledResult(await _mediator.Send(request));
}