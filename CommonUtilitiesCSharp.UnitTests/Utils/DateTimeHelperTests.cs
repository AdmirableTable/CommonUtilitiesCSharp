using CommonUtilitiesCSharp.Utils;

namespace CommonUtilitiesCSharp.UnitTests.Utils
{
    public class DateTimeHelperTests
    {
        #region Get12HFormatHour
        [Test]
        public void Get12HFormatHour_Throws_ForInvalidHour()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get12HFormatHour(-1));
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get12HFormatHour(24));
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get12HFormatHour(25));
            });
        }

        [Test]
        public void Get12HFormatHour_Returns12AM_For0()
        {
            Assert.That(DateTimeHelper.Get12HFormatHour(0), Is.EqualTo((12, false)));
        }

        [Test]
        public void Get12HFormatHour_Returns12PM_For12()
        {
            Assert.That(DateTimeHelper.Get12HFormatHour(12), Is.EqualTo((12, true)));
        }

        [Test]
        public void Get12HFormatHour_ReturnsExpected_ForValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(DateTimeHelper.Get12HFormatHour(1), Is.EqualTo((1, false)));
                Assert.That(DateTimeHelper.Get12HFormatHour(6), Is.EqualTo((6, false)));
                Assert.That(DateTimeHelper.Get12HFormatHour(13), Is.EqualTo((1, true)));
                Assert.That(DateTimeHelper.Get12HFormatHour(18), Is.EqualTo((6, true)));
            });
        }
        #endregion Get12HFormatHour

        #region Get24HFormatHour
        [Test]
        public void Get24HFormatHour_Throws_ForInvalidHour()
        {
            Assert.Multiple(() =>
            {
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get24HFormatHour(-1, true));
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get24HFormatHour(0, true));
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get24HFormatHour(13, true));
                Assert.Throws<ArgumentException>(() => DateTimeHelper.Get24HFormatHour(24, true));
            });
        }

        [Test]
        public void Get24HFormatHour_Returns0_For12AM()
        {
            Assert.That(DateTimeHelper.Get24HFormatHour(12, false), Is.EqualTo(0));
        }

        [Test]
        public void Get24HFormatHour_Returns12_For12PM()
        {
            Assert.That(DateTimeHelper.Get24HFormatHour(12, true), Is.EqualTo(12));
        }

        [Test]
        public void Get24HFormatHour_ReturnsExpected_ForValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(DateTimeHelper.Get24HFormatHour(1, false), Is.EqualTo(1));
                Assert.That(DateTimeHelper.Get24HFormatHour(6, false), Is.EqualTo(6));
                Assert.That(DateTimeHelper.Get24HFormatHour(1, true), Is.EqualTo(13));
                Assert.That(DateTimeHelper.Get24HFormatHour(6, true), Is.EqualTo(18));
            });
        }
        #endregion Get24HFormatHour
    }
}
