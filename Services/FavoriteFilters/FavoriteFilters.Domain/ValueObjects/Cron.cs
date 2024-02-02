namespace FavoriteFilters.Domain.ValueObjects;

public class Cron
{
    public int? Minute { get; set; }
    public int? Hour { get; set; }
    public int? DayOfMonth { get; set; }
    public int? Month { get; set; }
    public int? DayOfWeek { get; set; }

    public Cron()
    {
    }

    public Cron(int? minute, int? hour, int? dayOfMonth, int? month, int? dayOfWeek)
    {
        Minute = minute;
        Hour = hour;
        DayOfMonth = dayOfMonth;
        Month = month;
        DayOfWeek = dayOfWeek;
    }

    public override string ToString()
    {
        var minute = Minute.HasValue ? Minute.ToString() : "*";
        var hour = Hour.HasValue ? Hour.ToString() : "*";
        var dayOfMonth = DayOfMonth.HasValue ? DayOfMonth.ToString() : "*";
        var month = Month.HasValue ? Month.ToString() : "*";
        var dayOfWeek = DayOfWeek.HasValue ? DayOfWeek.ToString() : "*";
        
        return $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}";
    }
}
