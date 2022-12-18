namespace LDST.Domain.EFModels;

public class Bill
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public Guest Guest { get; set; } = null!;
    public GameReservation GameReservation { get; set; } = null!;
    public int GameReservationId { get; set; }
}
