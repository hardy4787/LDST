using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Application.Features.Sports.Commands.AddSportToCity;

public sealed class AddSportToCityCommand : ICommand<Unit>
{
    [FromRoute(Name = "sportId")]
    public int SportId { get; set; }
    [FromRoute(Name = "cityId")]
    public int CityId { get; set; }

    internal class Handler : ICommandHandler<AddSportToCityCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(AddSportToCityCommand command, CancellationToken cancellationToken)
        {
            if (!_context.Cities.Any(x => x.Id == command.CityId))
            {
                return DomainErrors.City.NotFoundCity;
            }

            if (!_context.Sports.Any(x => x.Id == command.SportId))
            {
                return DomainErrors.Sport.NotFoundSport;
            }

            _context.CitySports.Add(new CitySportEntity { SportId = command.SportId, CityId = command.CityId });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}