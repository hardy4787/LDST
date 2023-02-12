using FluentValidation;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundOverview;

internal sealed class GetPlaygroundOverviewQueryValidator : AbstractValidator<GetPlaygroundOverviewQuery>
{
    public GetPlaygroundOverviewQueryValidator()
    {
        RuleFor(x => x.PlaygroundId).GreaterThanOrEqualTo(0);
    }
}
