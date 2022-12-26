using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public sealed class GetPlaygroundsByCityQuery : IQuery<List<PlaygroundDto>>
{
    [FromRoute(Name = "sportId")]
    public int SportId { get; set; }

    [FromRoute(Name = "cityId")]
    public int CityId { get; set; }

    public sealed class Handler : IQueryHandler<GetPlaygroundsByCityQuery, List<PlaygroundDto>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        // TODO: add ids in query
        public async Task<ErrorOr<List<PlaygroundDto>>> Handle(GetPlaygroundsByCityQuery query, CancellationToken cancellationToken)
        {
            return await _context.Playgrounds
                    .Select(x =>
                        new PlaygroundDto(
                            x.Id,
                            x.Name,
                            x.GameTimeSlots
                                .Select(g =>
                                    new GameTimeSlotDto
                                        (
                                            g.Id,
                                            g.Price,
                                            (GameTimeSlotStatus)g.GameTimeSlotStatus
                                        )
                                ),
                            x.AverageRating,
                            x.TitlePhotoPath)
                    )
                    .ToListAsync(cancellationToken);
        }
    }
}

