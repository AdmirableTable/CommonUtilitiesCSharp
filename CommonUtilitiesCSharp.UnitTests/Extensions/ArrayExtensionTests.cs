using CommonUtilitiesCSharp.Extensions;
using CommonUtilitiesCSharp.NUnit.Constraints;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class ArrayExtensionTests
    {
        private static object[,] Convert2dArray(int[,] array)
        {
            if (array is null)
                return null!;

            var length0 = array.GetLength(0);
            var length1 = array.GetLength(1);
            var result = new object[length0, length1];

            for (int i = 0; i < length0; i++)
            {
                for (int j = 0; j < length1; j++)
                {
                    result[i,j] = array[i, j];
                }
            }

            return result;
        }

        #region AreArraysEquivalent
        [Test]
        public void AreArraysEquivalent_ReturnsTrue_ForEquivalentArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));
            
            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForNonEquivalentArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 7 } };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForDifferentSizedArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForNullArray1()
        {
            int[,] array1 = null!;
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForNullArray2()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] array2 = null!;

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsTrue_ForNullArrays()
        {
            int[,] array1 = null!;
            int[,] array2 = null!;

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsTrue_ForEmptyArrays()
        {
            var array1 = new int[,] { };
            var array2 = new int[,] { };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForEmptyArray1()
        {
            var array1 = new int[,] { };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }

        [Test]
        public void AreArraysEquivalent_ReturnsFalse_ForEmptyArray2()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { };

            var actual1 = ArrayExtensions.AreArraysEquivalent(array1, array2);
            var actual2 = ArrayExtensions.AreArraysEquivalent(Convert2dArray(array1), array2);
            var actual3 = ArrayExtensions.AreArraysEquivalent(array1, Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
            });
        }
        #endregion AreArraysEquivalent

        #region IsEquivalentTo
        [Test]
        public void IsEquivalentTo_ReturnsTrue_ForEquivalentArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));
            
            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
                Assert.That(actual4, Is.True);
            });
        }

        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForNonEquivalentArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 7 } };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }
        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForDifferentSizedArrays()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }
        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForNullArray1()
        {
            int[,] array1 = null!;
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }
        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForNullArray2()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] array2 = null!;

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }
        [Test]
        public void IsEquivalentTo_ReturnsTrue_ForNullArrays()
        {
            int[,] array1 = null!;
            int[,] array2 = null!;

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
                Assert.That(actual4, Is.True);
            });
        }
        [Test]
        public void IsEquivalentTo_ReturnsTrue_ForEmptyArrays()
        {
            var array1 = new int[,] { };
            var array2 = new int[,] { };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.True);
                Assert.That(actual2, Is.True);
                Assert.That(actual3, Is.True);
                Assert.That(actual4, Is.True);
            });
        }

        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForEmptyArray1()
        {
            var array1 = new int[,] { };
            var array2 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }

        [Test]
        public void IsEquivalentTo_ReturnsFalse_ForEmptyArray2()
        {
            var array1 = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var array2 = new int[,] { };

            var actual1 = array1.IsEquivalentTo(array2);
            var actual2 = array1.IsEquivalentTo(Convert2dArray(array2));
            var actual3 = Convert2dArray(array1).IsEquivalentTo(array2);
            var actual4 = Convert2dArray(array1).IsEquivalentTo(Convert2dArray(array2));

            Assert.Multiple(() =>
            {
                Assert.That(actual1, Is.False);
                Assert.That(actual2, Is.False);
                Assert.That(actual3, Is.False);
                Assert.That(actual4, Is.False);
            });
        }
        #endregion IsEquivalentTo

        #region GetLine
        [Test]
        public void GetLine_ReturnsValidArray_ForIndex0()
        {
            var array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            var actual = array.GetLine(0, 0);

            Assert.That(actual, Is.EquivalentTo(new int[] { 1, 4, 7 }));
        }

        [Test]
        public void GetLine_ReturnsValidArray_ForIndex1()
        {
            var array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            var actual = array.GetLine(1, 1);

            Assert.That(actual, Is.EquivalentTo(new int[] { 4, 5, 6 }));
        }

        [Test]
        public void GetLine_ReturnsEmpty_ForEmptyArray()
        {
            var array = new int[,] { };

            var actual = array.GetLine(0, 0);

            Assert.That(actual, Is.EquivalentTo(Array.Empty<int>()));
        }

        [Test]
        public void GetLine_ReturnsEmpty_ForSingleItem()
        {
            var array = new int[,] { { 1 }, { 2 }, { 3 } };

            var actual = array.GetLine(1, 2);

            Assert.That(actual, Is.EquivalentTo(new int[] { 3 }));
        }

        [Test]
        public void GetLine_Throws_ForNullArray()
        {
            int[,] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.GetLine(0, 0));
        }

        [Test]
        public void GetLine_Throws_ForInvalidDimension()
        {
            var array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Assert.Throws<ArgumentException>(() => array.GetLine(2, 0));
        }

        [Test]
        public void GetLine_Throws_ForNegativeDimension()
        {
            var array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

            Assert.Throws<ArgumentException>(() => array.GetLine(-1, 0));
        }
        #endregion GetLine

        #region Select

        #region Select<T>(this T[] source, Func<T, T> selector)
        [Test]
        public void Select1d_ReturnsValidArray_ForValidArray()
        {
            var array = new int[] { 1, 2, 3 };

            var actual = array.Select(i => i * 2);

            Assert.That(actual, Is.TypeOf<int[]>());
            Assert.That(actual, Is.EquivalentTo(new int[] { 2, 4, 6 }));
        }

        [Test]
        public void Select1d_ReturnsEmpty_ForEmptyArray()
        {
            var array = Array.Empty<int>();

            var actual = array.Select(i => i * 2);

            Assert.That(actual, Is.TypeOf<int[]>());
            Assert.That(actual, Is.EquivalentTo(Array.Empty<int>()));
        }

        [Test]
        public void Select1d_Throws_ForNullArray()
        {
            int[] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(i => i * 2));
        }

        [Test]
        public void Select1d_Throws_ForNullSelector()
        {
            var array = new int[] { 1, 2, 3 };
            Func<int, int> selector = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
        #endregion Select<T>(this T[] source, Func<T, T> selector)

        #region Select<T>(this T[] source, Func<T, int, T> selector)
        [Test]
        public void Select1dIndex_ReturnsValidArray_ForValidArray()
        {
            var array = new int[] { 1, 2, 3 };

            var actual = array.Select((i, index) => i * index);

            Assert.That(actual, Is.TypeOf<int[]>());
            Assert.That(actual, Is.EquivalentTo(new int[] { 0, 2, 6 }));
        }

        [Test]
        public void Select1dIndex_ReturnsEmpty_ForEmptyArray()
        {
            var array = Array.Empty<int>();

            var actual = array.Select((i, index) => i * index);

            Assert.That(actual, Is.TypeOf<int[]>());
            Assert.That(actual, Is.EquivalentTo(Array.Empty<int>()));
        }

        [Test]
        public void Select1dIndex_Throws_ForNullArray()
        {
            int[] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select((i, index) => i * index));
        }

        [Test]
        public void Select1dIndex_Throws_ForNullSelector()
        {
            var array = new int[] { 1, 2, 3 };
            Func<int, int, int> selector = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
        #endregion Select<T>(this T[] source, Func<T, int, T> selector)

        #region Select<T>(this T[,] source, Func<T, T> selector)
        [Test]
        public void Select2d_ReturnsValidArray_ForValidArray()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };

            var actual = array.Select(i => i * 2);

            var expected = new int[,] { { 2, 4 }, { 4, 6 }, { 6, 8 } };
            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<int[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<int>(expected));
            });
        }

        [Test]
        public void Select2d_ReturnsEmpty_ForEmptyArray()
        {
            var array = new int[,] { { }, { }, { } };

            var actual = array.Select(i => i * 2);

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<int[,]>());
                Assert.That(actual, Is.EquivalentTo(new int[,] { { }, { }, { } }));
            });
        }

        [Test]
        public void Select2d_Throws_ForNullArray()
        {
            int[,] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(i => i * 2));
        }

        [Test]
        public void Select2d_Throws_ForNullSelector()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            Func<int, int> selector = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
        #endregion Select<T>(this T[,] source, Func<T, T> selector)

        #region Select<T>(this T[,] source, Func<T, int, T> selector)
        [Test]
        public void Select2dIndex_ReturnsValidArray_ForValidArray()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };

            var actual = array.Select((i, index1, index2) => i * index1 + index2);

            Assert.Multiple(() =>
            {
                var expected = new int[,] { { 0, 1 }, { 2, 4 }, { 6, 9 } };
                Assert.That(actual, Is.TypeOf<int[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<int>(expected));
            });
        }

        [Test]
        public void Select2dIndex_ReturnsEmpty_ForEmptyArray()
        {
            var array = new int[,] { { }, { }, { } };

            var actual = array.Select((i, index1, index2) => i * index1 + index2);

            Assert.That(actual, Is.TypeOf<int[,]>());
            Assert.That(actual, Is.EquivalentTo(new int[,] { { }, { }, { } }));
        }

        [Test]
        public void Select2dIndex_Throws_ForNullArray()
        {
            int[,] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select((i, index1, index2) => i * index1 + index2));
        }

        [Test]
        public void Select2dIndex_Throws_ForNullSelector()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            Func<int, int, int, int> selector = null!;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
        #endregion Select<T>(this T[,] source, Func<T, int, T> selector)

        #endregion

        #region Cast
        [Test]
        public void Cast_ReturnsValidArray_ForValidCast()
        {
            var array = new int[,] { { 1, 2 }, { 3, 4 } };

            var actual = array.Cast<int, double>();

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<double[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<double>(new double[,] { { 1, 2 }, { 3, 4 } }));
            });
        }

        [Test]
        public void Cast_ReturnsValidArray_ForCastToObject()
        {
            var array = new int[,] { { 1, 2 }, { 3, 4 } };

            var actual = array.Cast<int, object>();

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<object[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<object>(new object[,] { { 1, 2 }, { 3, 4 } }));
            });
        }

        [Test]
        public void Cast_ReturnsValidArray_ForValidCastWitNullValues()
        {
            var array = new int?[,] { { 1, 2 }, { null, 4 } };

            var actual = array.Cast<int?, double?>();

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<double?[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<double?>(new double?[,] { { 1, 2 }, { null, 4 } }));
            });
        }

        [Test]
        public void Cast_ReturnsEmpty_ForEmptyArray()
        {
            var array = new int[,] { { }, { } };

            var actual = array.Cast<int, object>();

            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.TypeOf<object[,]>());
                Assert.That(actual, new IsEquivalentTo2DArrayConstraint<object>(new object[,] { { }, { } }));
            });
        }

        [Test]
        public void Cast_Throws_ForNullArray()
        {
            int[,] array = null!;

            Assert.Throws<ArgumentNullException>(() => array.Cast<int, object>());
        }

        [Test]
        public void Cast_Throws_ForInvalidCast()
        {
            var array = new int[,] { { 1, 2 }, { 3, 4 } };

            Assert.Throws<InvalidCastException>(() => array.Cast<int, object[]>());
        }
        #endregion Cast
    }
}
