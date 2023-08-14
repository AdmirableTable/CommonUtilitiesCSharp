using CommonUtilitiesCSharp.Extensions;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class IEnumerableExtensionTests
    {
        private class MockObject
        {
            public MockObject(string value)
            {
                Value = value;
            }

            public string? Value { get; set; }

            public override bool Equals(object? obj)
            {
                return obj is MockObject @object && Equals(@object);
            }

            public bool Equals(MockObject? obj)
            {
                return obj != null &&
                       Value == obj.Value;
            }

            public override int GetHashCode()
            {
                return nameof(MockObject).GetHashCode() * ((Value?.GetHashCode() ?? -1) + 7);
            }
        }

        #region ForEach
        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForMultipleItems()
        {
            var items = new MockObject[] { new("a"), new("b"), new("c") };

            items.ForEach(item => item.Value += ".");

            Assert.That(items, Is.EquivalentTo(new MockObject[] { new("a."), new("b."), new("c.") }));
        }

        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForSingleItem()
        {
            var items = new MockObject[] { new("a") };

            items.ForEach(item => item.Value += ".");

            Assert.That(items, Is.EquivalentTo(new MockObject[] { new("a.") }));
        }

        [Test]
        public void ForEach_ExecutesActionOnAllItems_ForZeroItems()
        {
            var items = Array.Empty<MockObject>();

            items.ForEach(item => item.Value += ".");

            Assert.That(items, Is.EquivalentTo(Array.Empty<MockObject>()));
        }

#nullable disable
        [Test]
        public void ForEach_ThrowsArgumentNullException_ForNullEnumerable()
        {
            IEnumerable<string> items = null;

            Assert.Throws<ArgumentNullException>(() => items.ForEach(item => item += "."));
        }

        [Test]
        public void ForEach_ThrowsArgumentNullException_ForNullAction()
        {
            var items = new string[] { "a", "b", "c" };

            Assert.Throws<ArgumentNullException>(() => items.ForEach(null));
        }
#nullable enable
        #endregion ForEach
    }
}