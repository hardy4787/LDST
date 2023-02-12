namespace LDST.Domain.EFModels;

public class CountryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<CityEnity> Cities { get; set; } = new();
}