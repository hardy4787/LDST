using FluentValidation;

namespace LDST.Application.Features.Playground.Commands.CreateTimeSlots;

public class CreateTimeSlotCommandValidator : AbstractValidator<CreateTimeSlotsCommand>
{
    public CreateTimeSlotCommandValidator()
    {
        RuleForEach(e => e.TimeSlots).ChildRules(c =>
        {
            c.RuleFor(a => a.Price).GreaterThanOrEqualTo(0);
        });
    }
}
