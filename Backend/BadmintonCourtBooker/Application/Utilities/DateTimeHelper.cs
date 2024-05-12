using System.Globalization;

namespace Application.Utilities
{
    public static class DateTimeHelper
    {
        const string DATE_FORMAT = "dd/MM/yyyy";
        const string DATE_WITH_NAME_FORMAT = "dd/MM/yyyy dddd";
        const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        const string DATETIME_WITH_NAME_FORMAT = "dd/MM/yyyy HH:mm:ss dddd";

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
    }
}
