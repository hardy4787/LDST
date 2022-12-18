namespace LDST.Domain.EFModels;

public class Playground
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal AverageRating { get; set; }
    public string? TitlePhotoPath { get; set; }
    public List<string> PhotoPaths { get; set; } = null!;
    public string Address1 { get; set; } = null!;
    public string Address2 { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

    public int SportId { get; set; }
    public Sport Sport { get; set; } = null!;
    public Guid HostId { get; set; }
    public Host Host { get; set; } = null!;
    public int CityId { get; set; }
    public City City { get; set; } = null!;
    public List<GameTimeslot> GameTimeslots { get; set; } = new();
    public List<PlaygroundGuestRating> PlaygroundGuestRatings { get; set; } = new();
}