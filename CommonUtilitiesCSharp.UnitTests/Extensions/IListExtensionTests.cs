using CommonUtilitiesCSharp.Extensions;
using System.Collections.Generic;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class IListExtensionTests
    {
        #region AddRange
        [Test]
        public void AddRange_AddsItemToList_ForMultipleItems()
        {
            var items = new string[] { "a", "b", "c" };
            IList<string> list = new List<string>();

            list.AddRange(items);

            Assert.That(list, Has.Count.EqualTo(items.Length));
            Assert.That(list, Is.EquivalentTo(items));
        }

        [Test]
        public void AddRange_AddsItemToList_ForSingleItem()
        {
            var items = new string[] { "a" };
            IList<string> list = new List<string>();

            list.AddRange(items);

            Assert.That(list, Has.Count.EqualTo(items.Length));
            Assert.That(list, Is.EquivalentTo(items));
        }

        [Test]
        public void AddRange_AddsNothingToList_ForZeroItems()
        {
            var items = Array.Empty<string>();
            IList<string> list = new List<string>();

            list.AddRange(items);

            Assert.That(list, Has.Count.EqualTo(items.Length));
            Assert.That(list, Is.EquivalentTo(items));
        }

#nullable disable
        [Test]
        public void AddRange_ThrowsArgumentNullException_ForNullList()
        {
            IList<string> list = null;
            var items = new string[] { "a", "b", "c" };

            Assert.Throws<ArgumentNullException>(() => list.AddRange(items));
        }

        [Test]
        public void AddRange_ThrowsArgumentNullException_ForNullItems()
        {
            IList<string> list = new List<string>();
            IEnumerable<string> items = null;

            Assert.Throws<ArgumentNullException>(() => list.AddRange(items));
        }
#nullable enable
        #endregion AddRange
    }
}