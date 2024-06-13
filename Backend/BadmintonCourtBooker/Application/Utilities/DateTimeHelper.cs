using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Application.Utilities
{
    public static class DateTimeHelper
    {
        const string DATE_FORMAT = "dd/MM/yyyy";
        const string DATE_WITH_NAME_FORMAT = "dd/MM/yyyy dddd";
        const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        const string DATETIME_WITH_NAME_FORMAT = "dd/MM/yyyy HH:mm:ss dddd";
        const string TIME_FORMAT = @"hh\:mm\:ss";
        const string TIME_FORMAT_WITH_DATE = @"dd\:hh\:mm\:ss";

        /// <summary>
        /// Formats a nullable DateTime value as a string using the specified date format.
        /// </summary>
        /// <param name="dateTime">The nullable DateTime value to format.</param>
        /// <returns>A string representation of the DateTime value in the format "dd/MM/yyyy" (Ex: "31/12/2000").</returns>
        public static string FormatDate(DateTime? dateTime)
        {
            return dateTime?.ToString(DATE_FORMAT) ?? string.Empty;
        }

        /// <summary>
        /// Formats a nullable DateTime value as a string using the specified date format.
        /// </summary>
        /// <param name="dateTime">The nullable DateTime value to format.</param>
        /// <returns>A string representation of the DateTime value in the format "dd/MM/yyyy dddd" (Ex: "31/12/2000 Sunday").</returns>
        public static string FormatDateWithName(DateTime? dateTime)
        {
            return dateTime?.ToString(DATE_WITH_NAME_FORMAT) ?? string.Empty;
        }

        /// <summary>
        /// Formats a nullable DateTime value as a string using the specified date format.
        /// </summary>
        /// <param name="dateTime">The nullable DateTime value to format.</param>
        /// <returns>A string representation of the DateTime value in the format "dd/MM/yyyy HH:mm:ss dddd" (Ex: "31/12/2000 00:00:00 ").</returns>
        public static string FormatDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(DATETIME_FORMAT) ?? string.Empty;
        }

        /// <summary>
        /// Formats a nullable DateTime value as a string using the specified date format.
        /// </summary>
        /// <param name="dateTime">The nullable DateTime value to format.</param>
        /// <returns>A string representation of the DateTime value in the format "dd/MM/yyyy HH:mm:ss dddd" (Ex: "31/12/2000 00:00:00 Sunday").</returns>
        public static string FormatDateTimeWithName(DateTime? dateTime)
        {
            return dateTime?.ToString(DATETIME_WITH_NAME_FORMAT) ?? string.Empty;
        }

        /// <summary>
        /// Converts a date string to a nullable <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateString">The input date string in the format "dd/MM/yyyy".</param>
        /// <returns>
        /// A nullable <see cref="DateTime"/> representing the parsed date if successful;
        /// otherwise, <see langword="null"/>.
        /// </returns>
        public static DateTime? ConvertDateString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return default;
            }

            DateTime dateTime;
            if (DateTime.TryParseExact(dateString, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Converts a date time string to a nullable <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="dateString">The input date string in the format "dd/MM/yyyy HH:mm:ss".</param>
        /// <returns>
        /// A nullable <see cref="DateTime"/> representing the parsed date if successful;
        /// otherwise, <see langword="null"/>.
        /// </returns>
        public static DateTime? ConvertDateTimeString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return default;
            }

            DateTime dateTime;
            if (DateTime.TryParseExact(dateString, DATETIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                return dateTime;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Formats a nullable TimeSpan value as a string using the specified time format.
        /// </summary>
        /// <param name="timeSpan">The nullable TimeSpan value to format.</param>
        /// <returns>A string representation of the TimeSpan value in the format "hh:mm:ss" (Ex: "01:30:00").</returns>
        public static string FormatTime(TimeSpan? timeSpan)
        {
            return timeSpan?.ToString(TIME_FORMAT) ?? string.Empty;
        }

        /// <summary>
        /// Formats a nullable TimeSpan value as a string using the specified time format.
        /// </summary>
        /// <param name="timeSpan">The nullable TimeSpan value to format.</param>
        /// <returns>A string representation of the TimeSpan value in the format "dd:hh:mm:ss" (Ex: "02:01:30:00").</returns>
        public static string FormatTimeWithDay(TimeSpan? timeSpan)
        {
            return timeSpan?.ToString(TIME_FORMAT_WITH_DATE) ?? string.Empty;
        }

        public static TimeSpan? ConvertTimeString(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
            {
                return default;
            }

            TimeSpan timeSpan;
            if (TimeSpan.TryParseExact(timeString, TIME_FORMAT, CultureInfo.InvariantCulture, TimeSpanStyles.None, out timeSpan))
            {
                return timeSpan;
            }
            else
            {
                return default;
            }
        }

        public static TimeSpan? ConvertTimeWithDateString(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
            {
                return default;
            }

            TimeSpan timeSpan;
            if (TimeSpan.TryParseExact(timeString, TIME_FORMAT_WITH_DATE, CultureInfo.InvariantCulture, TimeSpanStyles.None, out timeSpan))
            {
                return timeSpan;
            }
            else
            {
                return default;
            }
        }
    }
}
