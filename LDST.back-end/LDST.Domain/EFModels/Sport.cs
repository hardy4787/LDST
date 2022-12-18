namespace LDST.Domain.EFModels;

public class Sport
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<CitySport> CitySports { get; set; } = new();
}