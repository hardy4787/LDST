using LDST.Application.Features.Playground.Shared.Models;

namespace LDST.Application.Features.Shared.Models;
public record PlaygroundGeneral(
    string Name,
    double AverageRating,
    string CityName,
    IEnumerable<DaySchedule> DaySchedules,
    string? TitlePhotoPath
    );
