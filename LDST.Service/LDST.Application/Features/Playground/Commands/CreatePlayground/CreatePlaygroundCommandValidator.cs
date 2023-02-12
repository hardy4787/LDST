using FluentValidation;

namespace LDST.Application.Features.Playground.Commands.CreatePlayground;

internal sealed class CreatePlaygroundCommandValidator : AbstractValidator<CreatePlaygroundCommand>
{
    public CreatePlaygroundCommandValidator()
    {
        RuleFor(x => x.Playground).SetValidator(new CreatePlaygroundDtoValidator());
    }
}

internal class CreatePlaygroundDtoValidator : AbstractValidator<CreatePlaygroundDto>
{
    public CreatePlaygroundDtoValidator()
    {
        RuleFor(e => e.Name).MaximumLength(50);
        RuleFor(e => e.Description).MaximumLength(1000);
        RuleFor(e => e.Address1).MaximumLength(50);
        RuleFor(e => e.Address2).MaximumLength(50);
        RuleFor(e => e.State).MaximumLength(50);
        RuleFor(e => e.ZipCode).MaximumLength(10);
    }
}