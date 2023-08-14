namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates the string to the specified maximum length.
        /// </summary>
        /// <param name="str">String to truncate.</param>
        /// <param name="maxLength">Maximum length of the output string.</param>
        /// <returns>String containing at most the first <paramref name="maxLength"/> characters of the original string.</returns>
        public static string Truncate(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Length <= maxLength ? str : str[..maxLength];
        }
    }
}
