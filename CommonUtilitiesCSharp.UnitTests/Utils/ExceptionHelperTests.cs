using CommonUtilitiesCSharp.Utils;

namespace CommonUtilitiesCSharp.UnitTests.Utils
{
    public class ExceptionHelperTests
    {
        #region CastValue
        [Test]
        public void CastValue_ReturnsValue_ForValidValue()
        {
            object value = "test";
            
            var result = ExceptionHelper.CastValue<string>(value, "parameter");

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(value));
                Assert.That(result, Is.TypeOf<string>());
            });
        }

        [Test]
        public void CastValue_ReturnsValue_ForInterfaceCasting()
        {
            object value = new List<string>();

            object result = ExceptionHelper.CastValue<IEnumerable<string>>(value, "parameter");

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(value));
                Assert.That(result is IEnumerable<string>);
            });
        }

        [Test]
        public void CastValue_Throws_ForInvalidValue()
        {
            object value = 1;

            Assert.Throws<ArgumentException>(() => ExceptionHelper.CastValue<string>(value, "parameter"));
        }

        [Test]
        public void CastValue_Throws_ForNullValue()
        {
            object value = null!;

            Assert.Throws<ArgumentException>(() => ExceptionHelper.CastValue<string>(value, "parameter"));
        }
        #endregion CastValue
    }
}
