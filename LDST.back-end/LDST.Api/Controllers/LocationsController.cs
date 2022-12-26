using LDST.Api.Abstractions;
using LDST.Application.Features.Location.Queries.GetSupportedCities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

[Route("[controller]")]
public class LocationsController : ApiController
{
    private readonly ISender _mediator;
    public LocationsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{countryId}")]
    public async Task<IActionResult> Get([FromQuery] GetSupportedCitiesQuery request) =>
        GetHandledResult(await _mediator.Send(request));
}