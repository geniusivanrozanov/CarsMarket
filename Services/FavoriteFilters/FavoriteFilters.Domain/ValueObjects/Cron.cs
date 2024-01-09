namespace FavoriteFilters.Domain.ValueObjects;

public class Cron
{
    public int? Minute { get; set; }
    public int? Hour { get; set; }
    public int? DayOfMonth { get; set; }
    public int? Month { get; set; }
    public int? DayOfWeek { get; set; }

    public override string ToString()
    {
        return $"{Minute ?? '*'} {Hour ?? '*'} {DayOfMonth ?? '*'} {Month ?? '*'} {DayOfWeek ?? '*'}";
    }

    public bool IsValid()
    {
        return IsValid(Minute, Hour, DayOfMonth, Month, DayOfWeek);
    }

    public static bool IsValid(int? minute, int? hour, int? dayOfMonth, int? month, int? dayOfWeek)
    {
        return minute is null or >= 0 and <= 59
               && hour is null or >= 0 and <= 23
               && dayOfMonth is null or >= 1 and <= 31
               && month is null or >= 1 and <= 12
               && dayOfWeek is null or >= 0 and <= 6;
    }
}
