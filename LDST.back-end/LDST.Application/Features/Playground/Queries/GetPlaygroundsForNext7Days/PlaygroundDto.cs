namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public record PlaygroundDto(
    int Id,
    string Name,
    double AverageRating,
    string? TitlePhotoPath,
    IEnumerable<GameTimeSlotDto> TimeSlots);