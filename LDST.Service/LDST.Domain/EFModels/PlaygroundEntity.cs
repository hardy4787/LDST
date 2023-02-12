namespace LDST.Domain.EFModels;

public class PlaygroundEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double AverageRating { get; set; }
    public string? TitlePhotoPath { get; set; }
    public List<string> PhotoPaths { get; set; } = null!;
    public string Address1 { get; set; } = null!;
    public string Address2 { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public bool Reviewed { get; set; }

    public WeekScheduleEntity WeekSchedule { get; set; } = null!;
    public int SportId { get; set; }
    public SportEntity Sport { get; set; } = null!;
    public string HostId { get; set; } = null!;
    public UserEntity Host { get; set; } = null!;
    public int CityId { get; set; }
    public CityEnity City { get; set; } = null!;
    public List<GameTimeSlotEntity> GameTimeSlots { get; set; } = new();
    public List<PlaygroundGuestRatingEntity> PlaygroundGuestRatings { get; set; } = new();
}