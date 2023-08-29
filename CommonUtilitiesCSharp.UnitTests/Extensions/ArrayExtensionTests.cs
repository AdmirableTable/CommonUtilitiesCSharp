using CommonUtilitiesCSharp.Extensions;
using CommonUtilitiesCSharp.NUnit;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class ArrayExtensionTests
    {
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

#nullable disable
        [Test]
        public void GetLine_Throws_ForNullArray()
        {
            int[,] array = null;

            Assert.Throws<ArgumentNullException>(() => array.GetLine(0, 0));
        }
#nullable enable
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

#nullable disable
        [Test]
        public void Select1d_Throws_ForNullArray()
        {
            int[] array = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(i => i * 2));
        }

        [Test]
        public void Select1d_Throws_ForNullSelector()
        {
            var array = new int[] { 1, 2, 3 };
            Func<int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
#nullable enable
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

#nullable disable
        [Test]
        public void Select1dIndex_Throws_ForNullArray()
        {
            int[] array = null;

            Assert.Throws<ArgumentNullException>(() => array.Select((i, index) => i * index));
        }

        [Test]
        public void Select1dIndex_Throws_ForNullSelector()
        {
            var array = new int[] { 1, 2, 3 };
            Func<int, int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
#nullable enable
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
                Assert.That(ArrayTests.IsEquivalentTo2DArray(actual, expected), Is.True);
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

#nullable disable
        [Test]
        public void Select2d_Throws_ForNullArray()
        {
            int[,] array = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(i => i * 2));
        }

        [Test]
        public void Select2d_Throws_ForNullSelector()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            Func<int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
#nullable enable
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
                Assert.That(ArrayTests.IsEquivalentTo2DArray(actual, expected), Is.True);
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

#nullable disable
        [Test]
        public void Select2dIndex_Throws_ForNullArray()
        {
            int[,] array = null;

            Assert.Throws<ArgumentNullException>(() => array.Select((i, index1, index2) => i * index1 + index2));
        }

        [Test]
        public void Select2dIndex_Throws_ForNullSelector()
        {
            var array = new int[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            Func<int, int, int, int> selector = null;

            Assert.Throws<ArgumentNullException>(() => array.Select(selector));
        }
#nullable enable
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
                Assert.That(ArrayTests.IsEquivalentTo2DArray(actual, new double[,] { { 1, 2 }, { 3, 4 } }), Is.True);
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
                Assert.That(ArrayTests.IsEquivalentTo2DArray(actual, new object[,] { { 1, 2 }, { 3, 4 } }), Is.True);
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
                Assert.That(ArrayTests.IsEquivalentTo2DArray(actual, new object[,] { { }, { } }), Is.True);
            });
        }

#nullable disable
        [Test]
        public void Cast_Throws_ForNullArray()
        {
            int[,] array = null;

            Assert.Throws<ArgumentNullException>(() => array.Cast<int, object>());
        }

        [Test]
        public void Cast_Throws_ForInvalidCast()
        {
            var array = new int[,] { { 1, 2 }, { 3, 4 } };

            Assert.Throws<InvalidCastException>(() => array.Cast<int, object[]>());
        }
#nullable enable
        #endregion Cast
    }
}
