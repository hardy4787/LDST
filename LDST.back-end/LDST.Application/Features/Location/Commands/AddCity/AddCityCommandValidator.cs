using FluentValidation;

namespace LDST.Application.Features.Location.Commands.AddCity;

public class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    public AddCityCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
}
