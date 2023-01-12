using FluentValidation;

namespace LDST.Application.Features.Location.Queries.GetSupportedCities;

internal sealed class GetSupportedCitiesQueryValidator : AbstractValidator<GetSupportedCitiesQuery>
{
    public GetSupportedCitiesQueryValidator() => RuleFor(x => x.CountryId).GreaterThanOrEqualTo(0);
}
