using CommonUtilitiesCSharp.Extensions;
using System.Globalization;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class CultureInfoExtensionTests
    {
        #region Is24HTimeFormat
        [Test]
        public void Is24HTimeFormat_ThrowsArgumentNullException_ForNull()
        {
            CultureInfo cultureInfo = null!;

            Assert.Throws<ArgumentNullException>(() => cultureInfo.Is24HTimeFormat());
        }

        [Test]
        public void Is24HTimeFormat_ReturnsTrue_For24HTimeFormat()
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";

            Assert.Multiple(() =>
            {
                Assert.That(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0).ToString("t", cultureInfo), Is.EqualTo("13:00"));
                Assert.That(cultureInfo.Is24HTimeFormat(), Is.True);
            });
        }

        [Test]
        public void Is24HTimeFormat_ReturnsFalse_For12HTimeFormat()
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.DateTimeFormat.ShortTimePattern = "h:mm tt";

            Assert.Multiple(() =>
            {
                Assert.That(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0).ToString("t", cultureInfo), Is.EqualTo("1:00 PM"));
                Assert.That(cultureInfo.Is24HTimeFormat(), Is.False);
            });
        }
        #endregion Is24HTimeFormat
    }
}
