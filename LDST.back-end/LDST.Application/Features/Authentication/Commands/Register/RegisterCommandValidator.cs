using FluentValidation;

namespace LDST.Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
    }
}
