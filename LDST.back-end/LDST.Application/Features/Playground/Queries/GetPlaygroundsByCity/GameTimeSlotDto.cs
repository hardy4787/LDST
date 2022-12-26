namespace LDST.Application.Features.Playground.Queries.GetPlaygroundsByCity;

public record GameTimeSlotDto
(
    int Id,
    decimal Price,
    GameTimeSlotStatus GameTimeSlotStatus
);
