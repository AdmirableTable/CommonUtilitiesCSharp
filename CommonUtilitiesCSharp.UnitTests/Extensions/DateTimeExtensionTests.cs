using CommonUtilitiesCSharp.Extensions;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class DateTimeExtensionTests
    {
        #region SetHour
        [Test]
        public void SetHours_ReturnsInstanceWithNewHour_ForEarlierHour()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetHour(7);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 7, 30, 30)));
        }

        [Test]
        public void SetHours_ReturnsInstanceWithNewHour_ForLaterHour()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetHour(11);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 11, 30, 30)));
        }

        [Test]
        public void SetHours_ReturnsInstanceWithNewHour_ForMidnight()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetHour(0);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 0, 30, 30)));
        }

        [Test]
        public void SetHours_ReturnsInstanceWithNewHour_For23h()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetHour(23);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 23, 30, 30)));
        }

        [Test]
        public void SetHours_Throws_ForNegativeHour()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetHour(-1));
        }

        [Test]
        public void SetHours_Throws_ForOver23h()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetHour(24));
        }
        #endregion SetHour

        #region SetMinute
        [Test]
        public void SetMinutes_ReturnsInstanceWithNewMinute_ForEarlierMinute()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetMinute(15);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 15, 30)));
        }

        [Test]
        public void SetMinutes_ReturnsInstanceWithNewMinute_ForLaterMinute()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetMinute(45);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 45, 30)));
        }

        [Test]
        public void SetMinutes_ReturnsInstanceWithNewMinute_ForZero()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetMinute(0);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 00, 30)));
        }

        [Test]
        public void SetMinutes_ReturnsInstanceWithNewMinute_For59m()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetMinute(59);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 59, 30)));
        }

        [Test]
        public void SetMinutes_Throws_ForNegativeMinute()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetMinute(-1));
        }

        [Test]
        public void SetMinutes_Throws_ForOver59m()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetMinute(60));
        }
        #endregion SetMinute

        #region SetSeconds
        [Test]
        public void SetSeconds_ReturnsInstanceWithNewSeconds_ForEarlierSeconds()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetSeconds(15);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 30, 15)));
        }

        [Test]
        public void SetSeconds_ReturnsInstanceWithNewSeconds_ForLaterSeconds()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetSeconds(45);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 30, 45)));
        }

        [Test]
        public void SetSeconds_ReturnsInstanceWithNewSeconds_ForZero()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetSeconds(0);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 30, 00)));
        }

        [Test]
        public void SetSeconds_ReturnsInstanceWithNewSeconds_For59s()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            var result = dateTime.SetSeconds(59);

            Assert.That(result, Is.EqualTo(new DateTime(2025, 11, 01, 10, 30, 59)));
        }

        [Test]
        public void SetSeconds_Throws_ForNegativeSeconds()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetSeconds(-1));
        }

        [Test]
        public void SetSeconds_Throws_ForOver59s()
        {
            var dateTime = new DateTime(2025, 11, 01, 10, 30, 30);

            Assert.Throws<ArgumentOutOfRangeException>(() => dateTime.SetSeconds(60));
        }
        #endregion SetSeconds
    }
}
