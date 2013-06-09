using System;
using System.Web.Mvc;

namespace Experts.Web.Helpers
{
    public static class DateHelper
    {
        public static string ToTimeSinceFormat(this DateTime? value)
        {
            return value == null ? "-" : value.Value.ToTimeSinceFormat();
        }

        public static string ToTimeSinceFormat(this DateTime value)
        {
            const string space = " ";
            var ts = new TimeSpan(DateTime.Now.Ticks - value.Ticks);

            // no date set
            if (value <= DateTime.MinValue)
                return string.Empty;

            // less than one minute
            if (ts.TotalMinutes < 1)
                return ts.Seconds == 1 ? Resources.Global.DateOneSecondAgo : ts.Seconds + space + Resources.Global.DateSecondsAgo;

            // less than one hour
            if (ts.TotalHours < 1)
                return ts.Minutes + space + Resources.Global.DateMinutesAgo;

            if (ts.TotalHours < 48)
                return (int)Math.Round(ts.TotalHours) + space + Resources.Global.DateHoursAgo;

            if (ts.TotalDays < 30)
                return ts.Days + space + Resources.Global.DateDaysAgo;

            return value.ToString(Resources.Global.DateFormat);
        }

        public static string Date(this HtmlHelper html, DateTime dateTime)
        {
            return dateTime.ToTimeSinceFormat();
        }
    }
}
