using LDST.Application.Features.Playground.Shared.Models;

namespace LDST.Application.Features.Playground.Queries.GetPlaygroundOverview;

public class PlaygroundOverviewDto
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
    public string City { get; set; } = null!;
    public WeekSchedule WeekSchedule { get; set; } = null!;
}
