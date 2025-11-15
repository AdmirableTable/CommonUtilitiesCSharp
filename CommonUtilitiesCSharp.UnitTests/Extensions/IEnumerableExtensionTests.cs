using CommonUtilitiesCSharp.Extensions;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class IEnumerableExtensionTests
    {
        #region ForEach
        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForMultipleItems()
        {
            var items = new int[] { 1, 2, 3 };
            var result = new List<int>();

            items.ForEach(item => result.Add(item + 1));

            Assert.That(result, Is.EquivalentTo(new int[] { 2, 3, 4 }));
        }

        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForSingleItem()
        {
            var items = new int[] { 1 };
            var result = new List<int>();

            items.ForEach(item => result.Add(item + 1));

            Assert.That(result, Is.EquivalentTo(new int[] { 2 }));
        }

        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForZeroItems()
        {
            var items = Array.Empty<int>();
            var result = new List<int>();

            items.ForEach(item => result.Add(item + 1));

            Assert.That(result, Is.EquivalentTo(Array.Empty<int>()));
        }

        [Test]
        public void ForEach_ThrowsArgumentNullException_ForNullEnumerable()
        {
            IEnumerable<int> items = null!;
            var result = new List<int>();

            Assert.Throws<ArgumentNullException>(() => items.ForEach(item => result.Add(item + 1)));
        }

        [Test]
        public void ForEach_ThrowsArgumentNullException_ForNullAction()
        {
            var items = new int[] { 1, 2, 3 };

            Assert.Throws<ArgumentNullException>(() => items.ForEach(null!));
        }
        #endregion ForEach
    }
}
