using System;

public static class DateTimeExtensions
{
    public static string TimeAgo(this DateTime publishedTime)
    {
        var timeDifference = DateTime.Now - publishedTime;

        if (timeDifference.TotalMinutes < 1)
        {
            return $"{timeDifference.Seconds} saniye önce";
        }
        else if (timeDifference.TotalHours < 1)
        {
            return $"{timeDifference.Minutes} dakika önce";
        }
        else if (timeDifference.TotalDays < 1)
        {
            return $"{timeDifference.Hours} saat önce";
        }
        else
        {
            return $"{timeDifference.Days} gün önce";
        }
    }
}
