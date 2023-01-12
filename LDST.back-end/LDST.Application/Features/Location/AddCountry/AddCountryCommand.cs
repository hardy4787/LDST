using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using MediatR;

namespace LDST.Application.Features.Location.AddCountry;

public sealed class AddCountryCommand : ICommand<Unit>
{
    public string Name { get; set; } = null!;

    internal class Handler : ICommandHandler<AddCountryCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(AddCountryCommand command, CancellationToken cancellationToken)
        {
            _context.Countries.Add(new Domain.EFModels.CountryEntity { Name = command.Name });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}