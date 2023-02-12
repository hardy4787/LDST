using FluentValidation;

namespace LDST.Application.Features.Sports.Queries.GetCitySports;

internal sealed class GetCitySportsQueryValidator : AbstractValidator<GetCitySportsQuery>
{
    public GetCitySportsQueryValidator()
    {
        RuleFor(x => x.CountryId).GreaterThanOrEqualTo(0);
    }
}
