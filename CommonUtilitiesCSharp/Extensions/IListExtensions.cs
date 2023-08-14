﻿namespace CommonUtilitiesCSharp.Extensions
{
    public static class IListExtensions
    {
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
