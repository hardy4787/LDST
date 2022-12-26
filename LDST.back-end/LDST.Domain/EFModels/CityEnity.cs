namespace LDST.Domain.EFModels;

public class CityEnity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }
    public CountryEntity Country { get; set; } = null!;

    public List<CitySportEntity> CitySports { get; set; } = new();
}