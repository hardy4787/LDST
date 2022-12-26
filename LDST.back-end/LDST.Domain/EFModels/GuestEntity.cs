namespace LDST.Domain.EFModels;

public class GuestEntity
{
    public Guid Id { get; set; }
    public string? ProfileImagePath { get; set; }

    public UserEntity User { get; set; } = null!;
    public Guid UserId { get; set; }
    public List<BillEntity> Bills { get; set; } = null!;
    public List<GameReservationEntity> GameReservations { get; set; } = new();
    public List<PlaygroundGuestRatingEntity> PlaygroundGuestRatings { get; set; } = new();
}
