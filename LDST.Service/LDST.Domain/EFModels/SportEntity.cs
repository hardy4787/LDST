namespace LDST.Domain.EFModels;

public class SportEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<CitySportEntity> CitySports { get; set; } = new();
}