namespace LDST.Domain.EFModels;

public class WeekScheduleEntity
{
    public int Id { get; set; }
    public List<DayScheduleEntity> Days { get; set; } = new();
    public int PlaygroundId { get; set; }
    public PlaygroundEntity Playground { get; set; } = null!;
}
