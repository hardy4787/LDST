using FluentValidation;

namespace LDST.Application.Features.Sports.Commands.AddSport;

internal sealed class AddSportCommandValidator : AbstractValidator<AddSportCommand>
{
    public AddSportCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50);
    }
}
