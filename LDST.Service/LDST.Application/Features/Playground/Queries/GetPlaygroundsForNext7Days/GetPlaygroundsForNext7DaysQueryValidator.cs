using FluentValidation;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsForNext7Days;

internal sealed class GetPlaygroundsForNext7DaysQueryValidator : AbstractValidator<GetPlaygroundsForNext7DaysQuery>
{
    public GetPlaygroundsForNext7DaysQueryValidator()
    {
        RuleFor(x => x.SportId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CityId).GreaterThanOrEqualTo(0);
    }
}
