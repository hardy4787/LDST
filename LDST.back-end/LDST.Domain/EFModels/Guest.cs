namespace LDST.Domain.EFModels;

public class Guest
{
    public Guid Id { get; set; }
    public string? ProfileImagePath { get; set; }

    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public List<Bill> Bills { get; set; } = null!;
    public List<GameReservation> GameReservations { get; set; } = new();
    public List<PlaygroundGuestRating> PlaygroundGuestRatings { get; set; } = new();
}
