using System.Globalization;
using System;

namespace Utility
{
    public static class WeeKNumClass
    {
        public static int WeekNum()
        {
            ////code for week number in a month

            var date = DateTime.Now;
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            var weekNum = (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
            return weekNum;
        }
    }
}
