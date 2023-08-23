namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="Array"/>.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns all items from the target dimension at an index of the other dimension.
        ///
        /// Example: GetLine(0,0) returns items from dimension 0 with an opposite index of 0 : array[0,0], array[1,0], array[2,0]...
        /// </summary>
        /// <typeparam name="T">Array element type</typeparam>
        /// <param name="array">Source array</param>
        /// <param name="targetDimension">The dimension whose items the method will return</param>
        /// <param name="index">The static index for the other dimension the method will process</param>
        /// <returns>Returns a single-dimension array containing all elements of the target dimension, </returns>
        public static T[] GetLine<T>(this T[,] array, int targetDimension, int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var arrayLength = array.GetLength(targetDimension);
            var result = new T[arrayLength];

            Action<int> addToResultSet;

            if (targetDimension == 0)
                addToResultSet = (i) => result[i] = array[i, index];
            else if (targetDimension == 1)
                addToResultSet = (i) => result[i] = array[index, i];
            else
                throw new ArgumentException($"Array has 2 dimensions, hence {nameof(targetDimension)} must be 0 or 1", nameof(targetDimension));

            for (var i = 0; i < arrayLength; i++)
            {
                addToResultSet(i);
            }

            return result;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <param name="source">An <see cref="Array"/> of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/></typeparam>
        /// <returns>An <see cref="Array"/> whose elements are the result of invoking the transform function of each element of <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>"
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, TResult> selector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var arrayLength = source.Length;
            var result = new TResult[arrayLength];

            for (var i = 0; i < arrayLength; i++)
                result[i] = selector(source[i]);

            return result;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's index.
        /// </summary>
        /// <param name="source">An <see cref="Array"/> of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element. The second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/></typeparam>
        /// <returns>An <see cref="Array"/> whose elements are the result of invoking the transform function of each element of <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>"
        public static TResult[] Select<TSource, TResult>(this TSource[] source, Func<TSource, int, TResult> selector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var length = source.Length;
            var result = new TResult[length];

            for (var i = 0; i < length; i++)
                result[i] = selector(source[i], i);

            return result;
        }

        /// <inheritdoc cref="Select{TSource, TResult}(TSource[], Func{TSource, TResult})"/>
        public static TResult[,] Select<TSource, TResult>(this TSource[,] source, Func<TSource, TResult> selector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var arrayLength0 = source.GetLength(0);
            var arrayLength1 = source.GetLength(1);
            var result = new TResult[arrayLength0, arrayLength1];

            for (var i = 0; i < arrayLength0; i++)
            {
                for (var j = 0; j < arrayLength1; j++)
                    result[i, j] = selector(source[i, j]);
            }

            return result;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's 2D index.
        /// </summary>
        /// <param name="source">An <see cref="Array"/> of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element. The second and third parameters of the function represent the 2D index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/></typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/></typeparam>
        /// <returns>An <see cref="Array"/> whose elements are the result of invoking the transform function of each element of <paramref name="source"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if either parameter is null.</exception>"
        public static TResult[,] Select<TValue, TResult>(this TValue[,] source, Func<TValue, int, int, TResult> selector)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var a = source.GetLength(0);
            var b = source.GetLength(1);
            var result = new TResult[a, b];

            for (var i = 0; i < a; i++)
            {
                for (var j = 0; j < b; j++)
                    result[i, j] = selector(source[i, j], i, j);
            }

            return result;
        }
    }
}
