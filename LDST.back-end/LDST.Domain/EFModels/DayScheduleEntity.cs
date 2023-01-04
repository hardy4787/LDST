namespace LDST.Domain.EFModels;

public class DayScheduleEntity
{
    public int Id { get; set; }
    public bool IsClosed { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }

    public int WeekScheduleId { get; set; }
    public WeekScheduleEntity WeekSchedule { get; set; } = null!;
}
