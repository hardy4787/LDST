namespace LDST.Domain.EFModels;

public class CitySportEntity
{
    public int CityId { get; set; }
    public CityEnity City { get; set; } = null!;
    public int SportId { get; set; }
    public SportEntity Sport { get; set; } = null!;
}
