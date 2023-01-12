using FluentValidation;

namespace LDST.Application.Features.Sports.Commands.AddSportToCity;

internal sealed class AddSportToCityCommandValidator : AbstractValidator<AddSportToCityCommand>
{
    public AddSportToCityCommandValidator()
    {
        RuleFor(x => x.SportId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CityId).GreaterThanOrEqualTo(0);
    }
}
