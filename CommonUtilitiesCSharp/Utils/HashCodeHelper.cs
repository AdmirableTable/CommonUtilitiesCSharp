using System.Collections;

namespace CommonUtilitiesCSharp.Utils
{
    /// <summary>
    /// Helper class containing methods to assist with hash codes.
    /// </summary>
    public static class HashCodeHelper
    {
        /// <summary>
        /// Combines a set of values into a single hash code. The order of the values does not matter if the values implement IComparable. Otherwise, it will be required for a matching hash.<para/>
        /// Empty sets of values of type <typeparamref name="T"/> will all have the same hash.
        /// </summary>
        /// <typeparam name="T">The type of value to add to the hash.</typeparam>
        /// <param name="values">Set of values that will be added to the hash.</param>
        /// <returns>A hash code unique to a set of values.</returns>
        public static int CombineSequence<T>(IEnumerable<T> values)
        {
            if (!values.Any())
                return typeof(T).GetHashCode() * nameof(HashCodeHelper).GetHashCode(); // Custom hash code to differentiate empty sets of values by their type.

            var hash = new HashCode();
            IEnumerable<T> valueList = values.ToList();

            // TODO Find a way to order types that don't implement IComparable.
            var nullableType = Nullable.GetUnderlyingType(typeof(T));
            var valueType = nullableType ?? typeof(T);
            if (typeof(IComparable).IsAssignableFrom(valueType))
                valueList = valueList.OrderBy(item => item);

            foreach (var value in valueList)
                hash.Add(value);

            return hash.ToHashCode();
        }

        /// <inheritdoc cref="CombineSequence{T}(IEnumerable{T})" />
        /// <exception cref="InvalidOperationException">Thrown to avoid accidental misuse of the method, when the given argument array contains a single enumerable of values, whereas it would be expected to combine the values within that enumerable instead.</exception>
        public static int CombineSequence<T>(params T[] values)
        {
            if (values.Length == 1 && values[0] is IEnumerable)
                throw new InvalidOperationException($"Method called with a single {nameof(IEnumerable)} parameter. This is blocked to avoid unintended behaviour. Specify the generic type {nameof(T)} manually to obtain the correct behaviour.");

            return CombineSequence(values.AsEnumerable());
        }
    }
}
