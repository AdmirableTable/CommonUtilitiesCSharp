namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="ICollection{T}"/>.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds all items supplied to the end of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of item within both collections.</typeparam>
        /// <param name="collection">Source collection to add items to.</param>
        /// <param name="items">Collection of items that will be added to the end of the source collection.</param>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
                collection.Add(item);
        }
    }
}
