using System.Globalization;

namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="CultureInfo"/>.
    /// </summary>
    public static class CultureInfoExtensions
    {
        /// <summary>
        /// Determines if the <see cref="CultureInfo"/> uses a 24-hour time format or a 12-hour time format.
        /// </summary>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> to evaluate.</param>
        /// <returns>Returns true if the <see cref="CultureInfo"/> uses a 24-hour time format.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="cultureInfo"/> is null.</exception>
        public static bool Is24HTimeFormat(this CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            return cultureInfo.DateTimeFormat.ShortTimePattern.Contains('H');
        }
    }
}
