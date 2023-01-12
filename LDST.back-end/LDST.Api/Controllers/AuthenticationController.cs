using LDST.Api.Abstractions;
using LDST.Application.Features.Authentication.Commands.ForgotPassword;
using LDST.Application.Features.Authentication.Commands.Register;
using LDST.Application.Features.Authentication.Commands.ConfirmEmail;
using LDST.Application.Features.Authentication.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LDST.Application.Features.Authentication.Commands.DeleteUser;
using LDST.Application.Features.Authentication.Queries.TwoFactorLogin;

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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteUser(DeleteUserCommand request) =>
        GetHandledResult(await _mediator.Send(request));

    [HttpPost("login/two-factor")]
    public async Task<IActionResult> TwoFactorLogin(TwoFactorLoginQuery request) =>
        GetHandledResult(await _mediator.Send(request));
}
