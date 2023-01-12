using Microsoft.AspNetCore.Identity;

namespace LDST.Domain.EFModels;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public List<BillEntity> Bills { get; set; } = null!;
    public List<GameReservationEntity> GameReservations { get; set; } = new();
    public List<PlaygroundGuestRatingEntity> PlaygroundGuestRatings { get; set; } = new();
    public List<PlaygroundEntity> Playgrounds { get; set; } = new();
}