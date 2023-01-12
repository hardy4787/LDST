using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Features.Playground.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundOverview;

public sealed class GetPlaygroundOverviewQuery : IQuery<PlaygroundOverviewDto>
{
    [FromRoute(Name = "playgroundId")]
    public int PlaygroundId { get; set; }

    internal class Handler : IQueryHandler<GetPlaygroundOverviewQuery, PlaygroundOverviewDto>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<PlaygroundOverviewDto>> Handle(GetPlaygroundOverviewQuery query, CancellationToken cancellationToken)
        {
            var playground = await _context.Playgrounds.Include(p => p.City).Include(p=>p.WeekSchedule).ThenInclude(w=>w.Days).FirstOrDefaultAsync(p => query.PlaygroundId == p.Id);

            if (playground is null)
            {
                return DomainErrors.Playground.NotFoundPlayground;
            }

            return new PlaygroundOverviewDto
            {
                Id = playground.Id,
                Name = playground.Name,
                Description = playground.Description,
                Address1 = playground.Address1,
                Address2 = playground.Address2,
                AverageRating = playground.AverageRating,
                City = playground.City.Name,
                PhotoPaths = playground.PhotoPaths,
                TitlePhotoPath = playground.TitlePhotoPath,
                State = playground.State,
                ZipCode = playground.ZipCode,
                WeekSchedule = MapFromWeekScheduleEntity(playground.WeekSchedule)
            };
        }

        private WeekSchedule MapFromWeekScheduleEntity(Domain.EFModels.WeekScheduleEntity entity)
        {
            var days = entity.Days.Select(d => new DaySchedule
            {
                IsClosed = d.IsClosed,
                ClosingTime = d.ClosingTime,
                OpeningTime = d.OpeningTime,
                DayOfWeek = d.DayOfWeek
            }).ToList();

            return new WeekSchedule
            {
                Days = days
            };
        }
    }
}

