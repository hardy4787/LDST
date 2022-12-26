using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Location.Queries.GetSupportedCities;

public sealed class GetSupportedCitiesQuery : IQuery<List<CityDto>>
{
    [FromRoute(Name = "countryId")]
    public int CountryId { get; set; }

    public sealed class Handler : IQueryHandler<GetSupportedCitiesQuery, List<CityDto>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<CityDto>>> Handle(GetSupportedCitiesQuery query, CancellationToken cancellationToken)
        {
            return await _context.Cities
                    .Where(x => x.CountryId == query.CountryId)
                    .Select(x => new CityDto { Id = x.Id, Name = x.Name })
                    .ToListAsync(cancellationToken);
        }
    }
}
