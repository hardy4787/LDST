using FluentValidation;

namespace LDST.Application.Features.Authentication.Commands.ForgotPassword;

internal sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}
