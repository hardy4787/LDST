namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public record PlaygroundDto(
    int Id,
    string Name,
    IEnumerable<GameTimeSlotDto> TimeSlots,
    double AverageRating,
    string? TitlePhotoPath);
