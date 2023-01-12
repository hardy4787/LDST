using FluentValidation;

namespace LDST.Application.Features.Location.Commands.AddCity;

internal sealed class AddCityCommandValidator : AbstractValidator<AddCityCommand>
{
    public AddCityCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
}
