using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Features.Sports.Queries.Shared.Models;
using LDST.Application.Abstractions;

namespace LDST.Application.Features.Sports.Queries.GetCitySports;

public sealed class GetCitySportsQuery : IQuery<List<CitySportDto>>
{
    [FromRoute(Name = "countryId")]
    public int CountryId { get; set; }

    internal class Handler : IQueryHandler<GetCitySportsQuery, List<CitySportDto>>
    {
        private readonly IAppDbContext _context;

        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<CitySportDto>>> Handle(GetCitySportsQuery query, CancellationToken cancellationToken)
        {
            System.Console.WriteLine("test");
            return await _context.Cities
                .Include(x => x.CitySports)
                .ThenInclude(x => x.Sport)
                .Where(x => x.CountryId == query.CountryId && x.CitySports.Any())
                .Select(x =>
                    new CitySportDto(
                        x.Id, x.Name,
                        x.CitySports
                            .Select(y => new SportDto(y.SportId, y.Sport.Name)))).ToListAsync(cancellationToken);
        }
    }
}
