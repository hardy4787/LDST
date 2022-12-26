using FluentValidation;

namespace LDST.Application.Features.Location.AddCountry;

public class AddCountryCommandValidator : AbstractValidator<AddCountryCommand>
{
    public AddCountryCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
}
