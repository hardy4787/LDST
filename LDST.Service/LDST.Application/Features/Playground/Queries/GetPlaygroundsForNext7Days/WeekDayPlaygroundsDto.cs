namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsForNext7Days;

public record WeekDayPlaygroundsDto(
    string DayOfWeek,
    IEnumerable<PlaygroundDto> Playgrounds);
