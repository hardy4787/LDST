using LDST.Api.Abstractions;
using LDST.Application.Features.Authentication.Commands.Register;
using LDST.Application.Features.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Api.Controllers;

[Route("[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand request) => 
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery request) =>
        GetHandledResult(await _mediator.Send(request));
}