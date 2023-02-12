using FluentValidation;
using LDST.Application.Features.Authentication.Queries.Login;

namespace LDST.Application.Features.Authentication.Commands.Register;

internal sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}
