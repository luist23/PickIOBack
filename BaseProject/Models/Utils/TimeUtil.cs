using System.Globalization;

namespace BaseProject.Models.Utils;

public static class TimeUtil
{
     public static DateTime GetTime(string time, string format = "HHmmss")
    {
        return DateTime.TryParseExact(
            time,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var closedTime
        )
            ? closedTime
            : DateTime.MinValue;
    }

    public static string GetTimeString(DateTime time, string format = "HHmmss")
    {
        return time.ToString(format);
    }

    public static string GetTimeString(string? time, string text = "hh:mm:ss tt", string format = "HHmmss")
    {
        return time == null ? "" : GetTime(time, format).ToString(text);
    }

    public static string GetTimeString(int? time, string text = "hh:mm:ss tt", string format = "HHmmss")
    {
        return time == null ? "" : GetTimeString(time.ToString(), text, format);
    }

    public static string GetDateString(int? time, string format = "yyyyMMdd", string text = "dd/MM/yy")
    {
        return time == null ? "" : GetTime(time.ToString()!, format).ToString(text);
    }
    
    public static string GetDateString(string? time, string text = "yyyyMMdd", string format = "dd/MM/yy")
    {
        return time == null ? "" : GetTime(time.ToString()!, text).ToString(format);
    }

    public static string DateToString(DateTime date, string format)
    {
        return date.ToString(format);
    }

    public static (int Days, int Hours, int Minutes) GetDifferenceTime(
        int? requestDate,
        int? requestTime,
        DateTime? reference = null,
        bool isTo = true
    )
    {
        if (requestDate is null or 0)
            return (0, 0, -1);
        var requestDateTime = DateTime.ParseExact(
            requestDate + requestTime?.ToString("000000"), "yyyyMMddHHmmss",
            CultureInfo.InvariantCulture
        );
        var currentDateTime = reference ?? DateTime.Now;
        var timeDifference = isTo
            ? currentDateTime - requestDateTime
            : requestDateTime - currentDateTime;
        return (
            (int)timeDifference.TotalDays,
            timeDifference.Hours,
            timeDifference.Minutes
        );
    }
    
    public static (int Days, int Hours, int Minutes) GetDifferenceTime(
        string requestDate,
        string requestTime,
        DateTime? reference = null,
        bool isTo = true
    )
    {
        if (string.IsNullOrEmpty(requestDate) || string.IsNullOrEmpty(requestTime))
            return (0, 0, -1);
        var requestDateTime = DateTime.ParseExact(
            requestDate + requestTime, "yyyyMMddHHmmss",
            CultureInfo.InvariantCulture
        );
        var currentDateTime = reference ?? DateTime.Now;
        var timeDifference = isTo
            ? currentDateTime - requestDateTime
            : requestDateTime - currentDateTime;
        return (
            (int)timeDifference.TotalDays,
            timeDifference.Hours,
            timeDifference.Minutes
        );
    }

    public static string ToStringDaysHoursMin(int days, int hours, int minutes)
    {
        if(minutes < 0)
            return "-1";
        if (days > 0)
            return
                $"{days}D {hours}H {minutes}M";

        return hours > 0
            ? $"{hours}H {minutes}M"
            : $"{minutes}M";
    }

    public static (int Days, int Hours, int Minutes) GetDifferenceTime(
        int? requestDate,
        string? requestTime,
        DateTime? reference = null,
        bool isTo = true
    )
    {
        var time = Convert.ToInt32(requestTime ?? "0");
        return GetDifferenceTime(requestDate, time, reference, isTo);
    }
    
    public static long GetTimeLong()
    {
        return long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
    }
    
    public static string FormatTime(this long time, string format)
    {
        var date = DateTime.ParseExact(
            time.ToString(),
            "yyyyMMddHHmmss",
            CultureInfo.InvariantCulture
        );
        return date.ToString(format);
    }
}