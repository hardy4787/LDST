using LDST.Domain.Enums;

namespace LDST.Domain.EFModels;

// TODO: can be changed after UI implementation
public class GameReservation
{
    public int Id { get; set; }
    public ReservationStatus Status { get; set; }
    public Bill? Bill { get; set; }
    public Guest Guest { get; set; } = null!;
    public GameTimeslot GameTimeslot { get; set; } = null!;
    public int GameTimeslotId { get; set; }
}
