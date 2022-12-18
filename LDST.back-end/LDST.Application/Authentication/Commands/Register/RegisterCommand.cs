using ErrorOr;
using LDST.Application.Authentication.Common;
using MediatR;

namespace LDST.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}