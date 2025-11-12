using CommonUtilitiesCSharp.Extensions;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class StringExtensionTests
    {
        #region Truncate
        [Test]
        public void Truncate_ReturnsCorrectResult_ForLongString()
        {
            var result = "1234567890".Truncate(5);

            Assert.That(result, Is.EqualTo("12345"));
        }

        [Test]
        public void Truncate_ReturnsCorrectResult_ForShortString()
        {
            var result = "12345".Truncate(10);

            Assert.That(result, Is.EqualTo("12345"));
        }

        [Test]
        public void Truncate_ReturnsCorrectResult_ForEmptyString()
        {
            var result = string.Empty.Truncate(10);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Truncate_ReturnsCorrectResult_ForNullString()
        {
            string str = null!;
            var result = str.Truncate(10);

            Assert.That(result, Is.Null);
        }
        #endregion Truncate
    }
}
