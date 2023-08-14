namespace CommonUtilitiesCSharp.Utils
{
    /// <summary>
    /// Helper class containing methods for interacting with exceptions.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Casts <paramref name="value"/> to target type <typeparamref name="TTargetType"/> and throws a standardized <see cref="ArgumentNullException"/> if the value cannot be cast to the expected type.
        /// </summary>
        /// <typeparam name="TTargetType">Type to cast the <paramref name="value"/> to.</typeparam>
        /// <param name="value">Value to be cast</param>
        /// <param name="paramName">Parameter name of the given value. (to be used in the <see cref="ArgumentException"/> message)</param>
        /// <param name="paramDisplayName">Display name for the parameter of the given value. (to be used in the <see cref="ArgumentException"/> message)</param>
        /// <returns><paramref name="value"/> cast as type <typeparamref name="TTargetType"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> cannot be cast to the type <typeparamref name="TTargetType"/>.</exception>
        public static TTargetType CastValue<TTargetType>(object? value, string paramName, string? paramDisplayName = null)
        {
            if (value is TTargetType target)
            {
                return target;
            }

            throw new ArgumentException($"{paramDisplayName ?? paramName} of type \"{value?.GetType().Name ?? "null"}\" is not of expected type \"{typeof(TTargetType).Name}\"", paramName);
        }
    }
}
