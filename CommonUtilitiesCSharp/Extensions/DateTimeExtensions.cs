namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="DateTime"/>.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a new <see cref="DateTime"/> as a copy of this instance, where the hour is set to the specified number.
        /// </summary>
        /// <param name="dateTime">Source <see cref="DateTime"/> instance.</param>
        /// <param name="hour">Target hour to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="hour"/> is not between 0 and 23.</exception>
        /// <returns>Returns a new <see cref="DateTime"/> as a copy of this instance, where the hour is set to the specified number.</returns>
        public static DateTime SetHour(this DateTime dateTime, int hour)
        {
            if (hour < 0 || hour > 23)
            {
                throw new ArgumentOutOfRangeException(nameof(hour), $"{nameof(hour)} must be between 0 and 23.");
            }

            var currentHours = dateTime.Hour;
            var resultHours = hour - currentHours;

            return dateTime.AddHours(resultHours);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> as a copy of this instance, where the minute is set to the specified number.
        /// </summary>
        /// <param name="dateTime">Source <see cref="DateTime"/> instance.</param>
        /// <param name="minute">Target minute to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="minute"/> is not between 0 and 59.</exception>
        /// <returns>Returns a new <see cref="DateTime"/> as a copy of this instance, where the minute is set to the specified number.</returns>
        public static DateTime SetMinute(this DateTime dateTime, int minute)
        {
            if (minute < 0 || minute > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(minute), $"{nameof(minute)} must be between 1 and 59.");
            }

            var currentMinute = dateTime.Minute;
            var resultMinute = minute - currentMinute;

            return dateTime.AddMinutes(resultMinute);
        }

        /// <summary>
        /// Returns a new <see cref="DateTime"/> as a copy of this instance, where the seconds are set to the specified number.
        /// </summary>
        /// <param name="dateTime">Source <see cref="DateTime"/> instance.</param>
        /// <param name="seconds">Target seconds to set.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="seconds"/> is not between 1 and 59.</exception>
        /// <returns>Returns a new <see cref="DateTime"/> as a copy of this instance, where the seconds are set to the specified number.</returns>
        public static DateTime SetSeconds(this DateTime dateTime, int seconds)
        {
            if (seconds < 0 || seconds > 59)
            {
                throw new ArgumentOutOfRangeException(nameof(seconds), $"{nameof(seconds)} must be between 1 and 59.");
            }

            var currentSeconds = dateTime.Second;
            var resultSeconds = seconds - currentSeconds;

            return dateTime.AddSeconds(resultSeconds);
        }
    }
}
