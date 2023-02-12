using LDST.Domain.Enums;

namespace LDST.Domain.EFModels;

public class GameTimeSlotEntity
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public GameTimeSlotStatus GameTimeSlotStatus { get; set; }

    public int PlaygroundId { get; set; }
    public PlaygroundEntity Playground { get; set; } = null!;
    public GameReservationEntity? GameReservation { get; set; }
    public int GameReservationId { get; set; }
}
