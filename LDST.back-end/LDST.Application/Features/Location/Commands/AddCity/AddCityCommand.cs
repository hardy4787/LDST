using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Location.Commands.AddCity;

public record AddCityCommand : ICommand<Unit>
{
    public string Name { get; set; } = null!;

    public sealed class Handler : ICommandHandler<AddCityCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(AddCityCommand command, CancellationToken cancellationToken)
        {
            if (!await _context.Countries.AnyAsync(x => x.Id == 1, cancellationToken: cancellationToken))
            {
                return new ErrorOr<Unit>();
            }

            _context.Cities.Add(new CityEnity { Name = command.Name, CountryId = 1 });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}