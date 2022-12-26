using LDST.Domain.Enums;

namespace LDST.Domain.EFModels;

// TODO: can be changed after UI implementation
public class GameReservationEntity
{
    public int Id { get; set; }
    public ReservationStatus Status { get; set; }
    public BillEntity? Bill { get; set; }
    public GuestEntity Guest { get; set; } = null!;
    public GameTimeSlotEntity GameTimeSlot { get; set; } = null!;
    public int GameTimeSlotId { get; set; }
}
