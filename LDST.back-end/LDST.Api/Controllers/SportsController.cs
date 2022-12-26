using LDST.Api.Abstractions;
using LDST.Application.Features.Location.AddCountry;
using LDST.Application.Features.Location.Commands.AddCity;
using LDST.Application.Features.Sports.Commands.AddSport;
using LDST.Application.Features.Sports.Commands.AddSportToCity;
using LDST.Application.Features.Sports.Queries.GetCitySports;
using LDST.Application.Features.Sports.Queries.GetSupportedSports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

[Route("[controller]")]
public class SportsController : ApiController
{
    private readonly ISender _mediator;
    public SportsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("cities")]
    public async Task<IActionResult> AddCity(AddCityCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("countries")]
    public async Task<IActionResult> AddCountry(AddCountryCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> AddSport(AddSportCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("cities/{cityId}/sports/{sportId}")]
    public async Task<IActionResult> AddSport([FromQuery] AddSportToCityCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpGet("citysports/{countryId}")]
    public async Task<IActionResult> Get([FromQuery] GetCitySportsQuery request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetSupportedSportsQuery request) =>
        GetHandledResult(await _mediator.Send(request));
}