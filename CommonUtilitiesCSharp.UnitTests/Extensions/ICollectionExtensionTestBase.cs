namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public abstract class ICollectionExtensionTestBase<TCollectionType> where TCollectionType : IEnumerable<object>
    {
        protected abstract TCollectionType CreateEmptyCollection();

        protected abstract int GetCount(TCollectionType collection);

        protected abstract void AddRange(TCollectionType collection, IEnumerable<object> items);

        #region AddRange
        [Test]
        public void AddRange_AddsItemToCollection_ForMultipleItems()
        {
            var items = new object[] { "a", "b", "c" };
            TCollectionType collection = CreateEmptyCollection();

            AddRange(collection, items);

            Assert.Multiple(() =>
            {
                Assert.That(GetCount(collection), Is.EqualTo(items.Length));
                Assert.That(collection, Is.EquivalentTo(items));
            });
        }

        [Test]
        public void AddRange_AddsItemToCollection_ForSingleItem()
        {
            var items = new object[] { "a" };
            TCollectionType collection = CreateEmptyCollection();

            AddRange(collection, items);

            Assert.Multiple(() =>
            {
                Assert.That(GetCount(collection), Is.EqualTo(items.Length));
                Assert.That(collection, Is.EquivalentTo(items));
            });
        }

        [Test]
        public void AddRange_AddsNothingToCollection_ForZeroItems()
        {
            var items = Array.Empty<object>();
            TCollectionType collection = CreateEmptyCollection();

            AddRange(collection, items);

            Assert.Multiple(() =>
            {
                Assert.That(GetCount(collection), Is.EqualTo(items.Length));
                Assert.That(collection, Is.EquivalentTo(items));
            });
        }

#nullable disable
        [Test]
        public void AddRange_ThrowsArgumentNullException_ForNullCollection()
        {
            TCollectionType collection = default; // Can't be set to null because of the generic type constraint.
            if (collection is not null) throw new Exception("This should never happen.");

            var items = new object[] { "a", "b", "c" };

            Assert.Throws<ArgumentNullException>(() => AddRange(collection, items));
        }

        [Test]
        public void AddRange_ThrowsArgumentNullException_ForNullItems()
        {
            TCollectionType collection = CreateEmptyCollection();
            IEnumerable<object> items = null;

            Assert.Throws<ArgumentNullException>(() => AddRange(collection, items));
        }
#nullable enable
        #endregion AddRange
    }
}
