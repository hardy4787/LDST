using FluentValidation;

namespace LDST.Application.Features.Location.AddCountry;

internal sealed class AddCountryCommandValidator : AbstractValidator<AddCountryCommand>
{
    public AddCountryCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
}
