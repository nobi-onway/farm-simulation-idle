using System;

public static class Utils
{
    public static string TimeFormatter(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public static string BalanceFormatter(int balance)
    {
        return balance.ToString("N0");
    }
}