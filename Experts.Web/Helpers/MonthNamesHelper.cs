using System;
using System.Collections.Generic;
using Experts.Core.ViewModels;

namespace Experts.Web.Helpers
{
    public static class MonthNamesHelper
    {
        public static string PolishMonthNames(int month, int? year = null)
        {
            switch (month)
            {
                case 1:
                    return Resources.Dates.MonthNames_January + year;
                case 2:
                    return Resources.Dates.MonthNames_February + year;
                case 3:
                    return Resources.Dates.MonthNames_March + year;
                case 4:
                    return Resources.Dates.MonthNames_April + year;
                case 5:
                    return Resources.Dates.MonthNames_May + year;
                case 6:
                    return Resources.Dates.MonthNames_June + year;
                case 7:
                    return Resources.Dates.MonthNames_July + year;
                case 8:
                    return Resources.Dates.MonthNames_August + year;
                case 9:
                    return Resources.Dates.MonthNames_September + year;
                case 10:
                    return Resources.Dates.MonthNames_October + year;
                case 11:
                    return Resources.Dates.MonthNames_November + year;
                case 12:
                    return Resources.Dates.MonthNames_December + year;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public static string PolishMonthAbbreviations(int month, int? year = null)
        {
            switch (month)
            {
                case 1:
                    return Resources.Dates.MonthAbbreviations_January + year;
                case 2:
                    return Resources.Dates.MonthAbbreviations_February + year;
                case 3:
                    return Resources.Dates.MonthAbbreviations_March + year;
                case 4:
                    return Resources.Dates.MonthAbbreviations_April + year;
                case 5:
                    return Resources.Dates.MonthAbbreviations_May + year;
                case 6:
                    return Resources.Dates.MonthAbbreviations_June + year;
                case 7:
                    return Resources.Dates.MonthAbbreviations_July + year;
                case 8:
                    return Resources.Dates.MonthAbbreviations_August + year;
                case 9:
                    return Resources.Dates.MonthAbbreviations_September + year;
                case 10:
                    return Resources.Dates.MonthAbbreviations_October + year;
                case 11:
                    return Resources.Dates.MonthAbbreviations_November + year;
                case 12:
                    return Resources.Dates.MonthAbbreviations_December + year;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public static string PolishDayOfTheWeekGenitives(string day)
        {
            switch (day)
            {
                case "Monday":
                    return Resources.Dates.DayOfTheWeekGenitive_Monday;
                case "Tuesday":
                    return Resources.Dates.DayOfTheWeekGenitive_Tuesday;
                case "Wednesday":
                    return Resources.Dates.DayOfTheWeekGenitive_Wednesday;
                case "Thursday":
                    return Resources.Dates.DayOfTheWeekGenitive_Thursday;
                case "Friday":
                    return Resources.Dates.DayOfTheWeekGenitive_Friday;
                case "Saturday":
                    return Resources.Dates.DayOfTheWeekGenitive_Saturday;
                case "Sunday":
                    return Resources.Dates.DayOfTheWeekGenitive_Sunday;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        public static string[] GetPolishMonthNamesArray(int howmuch = 12, bool fullnames = true)
        {
            int now = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            string[] result = new string[howmuch];
            for (int i = howmuch-1; i >= 0; --i)
            {
                result[i] = fullnames ? PolishMonthNames(now--, year) : PolishMonthAbbreviations(now--, year);
                if (now == 0)
                {
                    now = 12;
                    --year;
                }
            }
            return result;
        }

        public static int[] Resort(IEnumerable<int> chartSeriesData)
        {
            int now = DateTime.Now.Month - 1;

            int[] chart = new List<int>(chartSeriesData).ToArray();
            int[] result = new int[chart.Length];

            for (int i=11; i>=0; --i)
            {
                result[i] = chart[now--];
                if (now == -1)
                {
                    now = 11;
                }
            }

            return result;
        }

        public static decimal[] ResortDecimal(IEnumerable<decimal> chartSeriesData)
        {
            int now = DateTime.Now.Month - 1;

            decimal[] chart = new List<decimal>(chartSeriesData).ToArray();
            decimal[] result = new decimal[chart.Length];

            for (int i = 11; i >= 0; --i)
            {
                result[i] = chart[now--];
                if (now == -1)
                {
                    now = 11;
                }
            }

            return result;
        }
    }
}