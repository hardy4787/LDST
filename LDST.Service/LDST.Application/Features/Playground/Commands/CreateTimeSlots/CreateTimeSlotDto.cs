namespace LDST.Application.Features.Playground.Commands.CreateTimeSlots;

public sealed record CreateTimeSlotDto(decimal Price, DateTime StartTime, DateTime EndTime);