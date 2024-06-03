using CommonUtilitiesCSharp.NUnit.Constraints;

namespace CommonUtilitiesCSharp.NUnit.UnitTests.Constraints
{
    public class MsgUtilTests
    {
        #region FormatArray
        [Test]
        public void FormatArray_ReturnsExpectedString_ForNullArray()
        {
            var array = (int[,])null!;

            var result = MsgUtils.FormatArray(array);

            Assert.That(result, Is.EqualTo("<null>"));
        }

        [Test]
        public void FormatArray_ReturnsExpectedString_ForEmptyArray()
        {
            var array = new int[,] { };

            var result = MsgUtils.FormatArray(array);

            Assert.That(result, Is.EqualTo("<empty>"));
        }

        [Test]
        public void FormatArray_ReturnsExpectedString_ForSingleDimensionalArray()
        {
            var array = new int[] { 1, 2, 3 };

            var result = MsgUtils.FormatArray(array);

            Assert.That(result, Is.EqualTo("< 1, 2, 3 >"));
        }

        [Test]
        public void FormatArray_ReturnsExpectedString_ForTwoDimensionalArray()
        {
            var array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

            var result = MsgUtils.FormatArray(array);

            Assert.That(result, Is.EqualTo("< < 1, 2, 3 >, < 4, 5, 6 > >"));
        }
        #endregion FormatArray
    }
}