namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Adds all items supplied to the end of the <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of item within both collections.</typeparam>
        /// <param name="collection">Source list to add items to.</param>
        /// <param name="items">Collection of items that will be added to the end of the source list.</param>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (list is null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (var item in items)
                list.Add(item);
        }
    }
}
