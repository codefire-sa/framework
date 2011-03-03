using System;
using System.Globalization;

namespace Codefire.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class DateUtility
    {
        public static int CountWeekDays(DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate.Subtract(startDate);
            int count = 0;

            for (int i = 0; i < span.Days; i++)
            {
                if (IsWeekDay(startDate.AddDays((double)i)))
                {
                    count++;
                }
            }
            return count;
        }

        public static int CountWeekEnds(DateTime startDate, DateTime endDate)
        {
            TimeSpan span = endDate.Subtract(startDate);
            int count = 0;

            for (int i = 0; i < span.Days; i++)
            {
                if (IsWeekEnd(startDate.AddDays((double)i)))
                {
                    count++;
                }
            }
            return count;
        }

        public static bool IsWeekDay(DateTime checkDate)
        {
            return (checkDate.DayOfWeek != DayOfWeek.Saturday && checkDate.DayOfWeek != DayOfWeek.Sunday);
        }

        public static bool IsWeekEnd(DateTime checkDate)
        {
            return (checkDate.DayOfWeek == DayOfWeek.Saturday || checkDate.DayOfWeek == DayOfWeek.Sunday);
        }

        public static string FormatDate(DateTime dateValue, string format)
        {
            return dateValue.ToString(format);
        }

        public static string FormatDate(DateTime? dateValue, string format)
        {
            return FormatDate(dateValue, format, "");
        }

        public static string FormatDate(DateTime? dateValue, string format, string defaultValue)
        {
            if (dateValue.HasValue)
            {
                return dateValue.Value.ToString(format);
            }
            else
            {
                return defaultValue;
            }
        }

        public static int DiffDays(DateTime startDate, DateTime endDate)
        {
            var diff = endDate.Subtract(startDate);

            return diff.Days;
        }

        public static int DiffMonths(DateTime startDate, DateTime endDate)
        {
            int startYear = startDate.Year;
            int startMonth = startDate.Month;
            int startDay = startDate.Day;

            int endYear = endDate.Year;
            int endMonth = endDate.Month;
            int endDay = endDate.Day;

            int diff = (endYear - startYear) * 12;
            diff += endMonth;
            diff -= startMonth;

            if (startDay > endDay) diff--;

            return diff;
        }

        public static int DiffYears(DateTime startDate, DateTime endDate)
        {
            int startYear = startDate.Year;
            int startMonth = startDate.Month;
            int startDay = startDate.Day;

            int endYear = endDate.Year;
            int endMonth = endDate.Month;
            int endDay = endDate.Day;

            int diff = endYear - startYear;
            if (startMonth > endMonth)
            {
                diff--;
            }
            else if (startMonth == endMonth)
            {
                if (startDay > endDay) diff--;
            }

            return diff;
        }
    }
}