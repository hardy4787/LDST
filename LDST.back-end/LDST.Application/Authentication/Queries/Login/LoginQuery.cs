using ErrorOr;
using LDST.Application.Authentication.Common;
using MediatR;

namespace LDST.Application.Authentication.Queries.Login
{
    public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}