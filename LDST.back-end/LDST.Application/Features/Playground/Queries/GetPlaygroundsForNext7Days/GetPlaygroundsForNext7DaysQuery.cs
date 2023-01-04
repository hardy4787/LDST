using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Features.Playground.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public sealed class GetPlaygroundsForNext7DaysQuery : IQuery<List<WeekDayPlaygroundsDto>>
{
    [FromRoute(Name = "sportId")]
    public int SportId { get; set; }

    [FromRoute(Name = "cityId")]
    public int CityId { get; set; }

    public sealed class Handler : IQueryHandler<GetPlaygroundsForNext7DaysQuery, List<WeekDayPlaygroundsDto>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<WeekDayPlaygroundsDto>>> Handle(GetPlaygroundsForNext7DaysQuery query, CancellationToken cancellationToken)
        {
            DateTime currentDay = DateTime.Now;

            IEnumerable<PlaygroundDto> playgrounds =
                await GetPlaygroundsForNaxt7Days(currentDay, cancellationToken);

            return GroupPlaygroundsByWeekDay(currentDay, playgrounds);
        }

        private async Task<IEnumerable<PlaygroundDto>> GetPlaygroundsForNaxt7Days(DateTime currentDay, CancellationToken cancellationToken)
        {
            DateTime dayIn6Days = currentDay.AddDays(6).EndOfDay();

            return (await _context.Playgrounds
                            .Include(p => p.GameTimeSlots)
                            .Include(p => p.WeekSchedule)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken))
                            .Select(p => new PlaygroundDto(
                                p.Id,
                                p.Name,
                                p.AverageRating,
                                p.TitlePhotoPath,
                                p.GameTimeSlots
                                    .Where(gt => gt.StartTime >= currentDay && gt.StartTime <= dayIn6Days).Select(gt =>
                                        new GameTimeSlotDto(
                                            gt.Id,
                                            gt.Price,
                                            gt.StartTime,
                                            gt.EndTime,
                                            (GameTimeSlotStatus)gt.GameTimeSlotStatus)
                                        )
                                    )
                            );
        }

        private static List<WeekDayPlaygroundsDto> GroupPlaygroundsByWeekDay(DateTime currentDay, IEnumerable<PlaygroundDto> playgrounds)
        {
            var weekDays = GetWeekDaysAccordingToCurrentDay(currentDay);
            return weekDays
                            .Select(weekDay =>
                                new WeekDayPlaygroundsDto
                                (
                                    weekDay.ToString(),
                                    playgrounds
                                        .Select(p => new PlaygroundDto
                                            (
                                                p.Id,
                                                p.Name,
                                                p.AverageRating,
                                                p.TitlePhotoPath,
                                                p.TimeSlots.Where(gt => gt.StartTime.DayOfWeek == weekDay)
                                            ))
                                        .Where(p => p.TimeSlots.Any())
                                )
                            )
                            .ToList();
        }

        private static DayOfWeek[] GetWeekDaysAccordingToCurrentDay(DateTime currentDay)
        {
            DayOfWeek[] dayOfWeeks = new DayOfWeek[7];
            for (int i = 0; i < 7; i++)
            {
                dayOfWeeks[i] = currentDay.AddDays(i).DayOfWeek;
            }
            return dayOfWeeks;
        }
    }
}

