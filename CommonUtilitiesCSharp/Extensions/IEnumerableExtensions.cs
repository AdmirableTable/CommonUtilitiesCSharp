namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of item within both collections.</typeparam>
        /// <param name="collection">Source collection to act on.</param>
        /// <param name="action">Action executed on each element of the collection.</param>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in collection)
                action(item);
        }
    }
}
