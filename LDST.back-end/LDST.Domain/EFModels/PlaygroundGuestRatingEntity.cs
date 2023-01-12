namespace LDST.Domain.EFModels;

public class PlaygroundGuestRatingEntity
{
    public int Id { get; set; }
    public int Rating { get; set; }

    public UserEntity Guest { get; set; } = new();
    public PlaygroundEntity Playground { get; set; } = null!;
}