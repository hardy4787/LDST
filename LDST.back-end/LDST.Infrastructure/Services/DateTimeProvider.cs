using LDST.Application.Common.Interfaces.Services;

namespace LDST.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
