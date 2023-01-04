namespace LDST.Application.Features.Playground.Shared.Models;

public class DaySchedule
{
    public bool IsClosed { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }
}

