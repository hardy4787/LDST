using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Features.Playground.Shared.Models;
using LDST.Application.Features.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByHostId;
public sealed class GetPlaygroundsByHostIdQuery : IQuery<List<PlaygroundGeneral>>
{
    [FromRoute(Name = "hostId")]
    public string HostId { get; set; } = null!;

    internal class Handler : IQueryHandler<GetPlaygroundsByHostIdQuery, List<PlaygroundGeneral>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<PlaygroundGeneral>>> Handle(GetPlaygroundsByHostIdQuery query, CancellationToken cancellationToken)
        {
            var playgrounds = await _context.Playgrounds
                .Include(p => p.City)
                .Include(p => p.WeekSchedule)
                .ThenInclude(w => w.Days)
                .Where(p=>p.HostId == query.HostId)
                .Select(p=> 
                    new PlaygroundGeneral(
                        p.Name,
                        p.AverageRating,
                        p.City.Name,
                        p.WeekSchedule.Days.Select(d => new DaySchedule
                        {
                            IsClosed = d.IsClosed,
                            ClosingTime = d.ClosingTime,
                            OpeningTime = d.OpeningTime,
                            DayOfWeek = d.DayOfWeek
                        }),
                        p.TitlePhotoPath
                        )
                    )
                .ToListAsync(cancellationToken);

            return playgrounds;
        }
    }
}

