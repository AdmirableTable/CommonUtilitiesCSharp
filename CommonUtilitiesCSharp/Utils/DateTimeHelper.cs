namespace CommonUtilitiesCSharp.Utils
{
    /// <summary>
    /// Helper class containing methods for interacting with <see cref="DateTime"/> objects.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns the hour in 12-hour format and whether it is AM or PM.
        /// </summary>
        /// <param name="hour">24-hour value to convert.</param>
        /// <returns>Returns the given 24-hour value into 12-hour format and a boolean that will be true if the hour is PM.</returns>
        /// <exception cref="ArgumentException">Thrown is the given <paramref name="hour"/> parameter is not a 24-hour value. (0 to 23 inclusively)</exception>
        public static (int Hour, bool IsPM) Get12HFormatHour(int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentException("Hour must be between 0 and 23, inclusively.", nameof(hour));
            }

            if (hour == 0)
            {
                return (12, false);
            }

            if (hour == 12)
            {
                return (12, true);
            }

            return (hour % 12, hour > 12);
        }

        /// <summary>
        /// Returns the hour in 24-hour format.
        /// </summary>
        /// <param name="hour">12-hour value to convert.</param>
        /// <param name="isPM">Set this parameter to true if the hour to convert is PM or false if the hour is AM.</param>
        /// <returns>Returns the given 12-hour value into 24-hour format.</returns>
        /// <exception cref="ArgumentException">Thrown if the given <paramref name="hour"/> parameter is not a 12-hour value. (1 to 12 inclusively)</exception>
        public static int Get24HFormatHour(int hour, bool isPM)
        {
            if (hour < 1 || hour > 12)
            {
                throw new ArgumentException("Hour must be between 1 and 12, inclusively.", nameof(hour));
            }

            if (isPM)
            {
                return hour == 12 ? 12 : hour + 12;
            }
            else
            {
                return hour == 12 ? 0 : hour;
            }
        }
    }
}
