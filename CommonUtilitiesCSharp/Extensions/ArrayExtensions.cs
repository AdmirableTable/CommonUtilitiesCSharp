namespace CommonUtilitiesCSharp.Extensions
{
    /// <summary>
    /// Static class containing extension methods for <see cref="Array"/>.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <inheritdoc cref="AreArraysEquivalent(object[,], object[,])"/>
        public static bool AreArraysEquivalent<T>(T[,]? array1, T[,]? array2) => AreArraysEquivalent(array1?.Cast<T, object>(), array2?.Cast<T, object>());

        /// <inheritdoc cref="AreArraysEquivalent(object[,], object[,])"/>
        public static bool AreArraysEquivalent<T>(object[,]? array1, T[,]? array2) => AreArraysEquivalent(array1, array2?.Cast<T, object>());

        /// <inheritdoc cref="AreArraysEquivalent(object[,], object[,])"/>
        public static bool AreArraysEquivalent<T>(T[,]? array1, object[,]? array2) => AreArraysEquivalent(array1?.Cast<T, object>(), array2);

        /// <summary>
        /// Tests whether two two-dimensional arrays are equivalent (all items are equal).
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <returns>Returns true if all items of one array are equal to all items in the second array, or both arrays are null or empty.</returns>
        public static bool AreArraysEquivalent(object[,]? array1, object[,]? array2)
        {
            if (array1 is null)
                return array2 is null;

            if (array2 is null)
                return false;

            if (array1.GetLength(0) != array2.GetLength(0) ||
                array1.GetLength(1) != array2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    if (!Equals(array1[i, j], array2[i, j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Tests whether the source array is equivalent to the target array (all items are equal).
        /// </summary>
        /// <param name="source">Soruce array to compare.</param>
        /// <param name="target">Array to compare the source against.</param>
        /// <returns>Returns true if all items of one array are equal to all items in the second array, or both arrays are null or empty.</returns>
        public static bool IsEquivalentTo(this object[,]? source, object[,]? target) => AreArraysEquivalent(source, target);

        /// <inheritdoc cref="IsEquivalentTo(object[,], object[,])"/>
        public static bool IsEquivalentTo<T>(this T[,]? source, T[,]? target) => AreArraysEquivalent(source, target);

        /// <inheritdoc cref="IsEquivalentTo(object[,], object[,])"/>
        public static bool IsEquivalentTo<T>(this T[,]? source, object[,]? target) => AreArraysEquivalent(source, target);

        /// <inheritdoc cref="IsEquivalentTo(object[,], object[,])"/>
        public static bool IsEquivalentTo<T>(this object[,]? source, T[,]? target) => AreArraysEquivalent(source, target);

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
            if (targetDimension < 0 || targetDimension > 1) throw new ArgumentException($"{nameof(targetDimension)} \'{targetDimension}\' does not match valid dimension indexes 0 and 1", nameof(targetDimension));

            var arrayLength = array.GetLength(targetDimension);
            var result = new T[arrayLength];

            Action<int> addToResultSet;

            if (targetDimension == 0)
                addToResultSet = (i) => result[i] = array[i, index];
            else // targetDimension == 1
                addToResultSet = (i) => result[i] = array[index, i];

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

        /// <summary>
        /// Casts the elements of two-dimensional <see cref="Array"/> to the specified type.
        /// </summary>
        /// <typeparam name="TSource">Type of items from the <paramref name="source"/> array.</typeparam>
        /// <typeparam name="TResult">Target type to cast the items of <paramref name="source"/> to.</typeparam>
        /// <param name="source">The <see cref="Array"/> that contains the elements to be cast to type <typeparamref name="TResult"/>.</param>
        /// <returns>A two-dimensional <see cref="Array"/> that contains each element of the source cast to the specified type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="InvalidCastException">Thrown if an item of <paramref name="source"/> could not be cast to the type <typeparamref name="TResult"/>.</exception>
        public static TResult[,] Cast <TSource, TResult>(this TSource[,] source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var length0 = source.GetLength(0);
            var length1 = source.GetLength(1);
            var result = new TResult[length0, length1];
        
            for (var i = 0; i < length0; i++)
            {
                for (var j = 0; j < length1; j++)
                {
                    var sourceItem = source[i, j];
                    result[i, j] = CastItem<TSource, TResult>(sourceItem);
                }
            }

            return result;
        }

        private static TResult CastItem<TSource, TResult>(TSource sourceItem)
        {
            if (sourceItem is null)
                return default!;

            var targetType = typeof(TResult);

            var underlyingType = Nullable.GetUnderlyingType(targetType);
            if (underlyingType is not null)
            {
                return (TResult)Convert.ChangeType(sourceItem, underlyingType);
            }

            return (TResult)Convert.ChangeType(sourceItem, targetType);
        }
    }
}
