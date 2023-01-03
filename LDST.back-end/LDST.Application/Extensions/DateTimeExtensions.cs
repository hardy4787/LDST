namespace LDST.Application.Extensions;

public static class DateTimeExtensions
{
    public static DateTime EndOfDay(this DateTime dateTime, DateTimeKind timeKind = DateTimeKind.Local)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, timeKind);
    }
}
