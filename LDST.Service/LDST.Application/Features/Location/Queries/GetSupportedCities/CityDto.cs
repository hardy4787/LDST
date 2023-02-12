namespace LDST.Application.Features.Location.Queries.GetSupportedCities;

public sealed record CityDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}
