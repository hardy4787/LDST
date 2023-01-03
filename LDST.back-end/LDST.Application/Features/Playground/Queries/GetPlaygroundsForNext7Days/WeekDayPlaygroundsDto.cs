namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public record WeekDayPlaygroundsDto(
    string DayOfWeek,
    IEnumerable<PlaygroundDto> Playgrounds);
