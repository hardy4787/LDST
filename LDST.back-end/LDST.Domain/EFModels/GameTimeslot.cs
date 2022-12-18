using LDST.Domain.Enums;

namespace LDST.Domain.EFModels;

public class GameTimeslot
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public GameTimeslotStatus GameTimeslotStatus { get; set; }

    public Playground Playground { get; set; } = null!;
    public GameReservation? GameReservation { get; set; }
    public int GameReservationId { get; set; }
}
