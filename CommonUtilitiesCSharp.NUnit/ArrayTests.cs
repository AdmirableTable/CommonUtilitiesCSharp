namespace CommonUtilitiesCSharp.NUnit
{
    /// <summary>
    /// Contains standardised methods for testing arrays.
    /// </summary>
    public static class ArrayTests
    {
        /// <summary>
        /// Checks if two 2D int arrays are equivalent.
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <returns>True if both arrays have equal elements.</returns>
        public static bool IsEquivalentTo2DArray(int[,] array1, int[,] array2)
        {
            if (array1.GetLength(0) != array2.GetLength(0) ||
                array1.GetLength(1) != array2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    if (array1[i, j] != array2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if two 2D double arrays are equivalent.
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <returns>True if both arrays have equal elements.</returns>
        public static bool IsEquivalentTo2DArray(double[,] array1, double[,] array2)
        {
            if (array1.GetLength(0) != array2.GetLength(0) ||
                array1.GetLength(1) != array2.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    if (array1[i, j] != array2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if two 2D object arrays are equivalent.
        /// </summary>
        /// <param name="array1">First array to compare.</param>
        /// <param name="array2">Second array to compare.</param>
        /// <returns>True if both arrays have equal elements.</returns>
        public static bool IsEquivalentTo2DArray(object[,] array1, object[,] array2)
        {
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
    }
}
