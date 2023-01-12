namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsForNext7Days;

public record GameTimeSlotDto
(
    int Id,
    decimal Price,
    DateTime StartTime,
    DateTime EndTime,
    GameTimeSlotStatus GameTimeSlotStatus
);