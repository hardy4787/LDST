namespace LDST.Domain.EFModels;

public class BillEntity
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public UserEntity Guest { get; set; } = null!;
    public GameReservationEntity GameReservation { get; set; } = null!;
    public int GameReservationId { get; set; }
}
