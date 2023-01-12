using FluentValidation;

namespace LDST.Application.Features.Authentication.Commands.Register;

internal sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Password)
            .Equal(customer => customer.ConfirmPassword)
            .WithMessage("The password and confirmation password do not match.");
        RuleFor(x => x.FirstName).MaximumLength(50);
        RuleFor(x => x.LastName).MaximumLength(50);
        RuleFor(x => x.Email).EmailAddress();
    }
}
