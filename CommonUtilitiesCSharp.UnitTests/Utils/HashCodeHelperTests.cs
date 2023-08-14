using CommonUtilitiesCSharp.Utils;

namespace CommonUtilitiesCSharp.UnitTests.Utils
{
    public class HashCodeHelperTests
    {
        #region CombineSequence
        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValues()
        {
            var hash1 = HashCodeHelper.CombineSequence(1, 2, 3);
            var hash2 = HashCodeHelper.CombineSequence(1, 2, 3);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsDifferentHashForDifferentValues()
        {
            var hash1 = HashCodeHelper.CombineSequence(1, 2, 3);
            var hash2 = HashCodeHelper.CombineSequence(1, 2, 4);

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithDifferentOrder()
        {
            var hash1 = HashCodeHelper.CombineSequence(1, 2, 3);
            var hash2 = HashCodeHelper.CombineSequence(3, 2, 1);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithDuplicates()
        {
            var hash1 = HashCodeHelper.CombineSequence(1, 2, 3, 3);
            var hash2 = HashCodeHelper.CombineSequence(1, 2, 3, 3);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsDifferentHashForIdenticalValuesWithDuplicates()
        {
            var hash1 = HashCodeHelper.CombineSequence(1, 2, 3 ,3);
            var hash2 = HashCodeHelper.CombineSequence(1, 2 ,3);

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithNulls()
        {
            var hash1 = HashCodeHelper.CombineSequence<int?>(1, 2, 3, null);
            var hash2 = HashCodeHelper.CombineSequence<int?>(1, 2, 3, null);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithDifferentOrderAndNulls()
        {
            var hash1 = HashCodeHelper.CombineSequence<int?>(1, 2, 3, null);
            var hash2 = HashCodeHelper.CombineSequence<int?>(3, 2, 1, null);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithDuplicatesAndNulls()
        {
            var hash1 = HashCodeHelper.CombineSequence<int?>(1, 2, 3, 3, null);
            var hash2 = HashCodeHelper.CombineSequence<int?>(1, 2, 3, 3, null);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesWithDifferentTypes()
        {
            var hash1 = HashCodeHelper.CombineSequence<object>(1, 2, 3, "4");
            var hash2 = HashCodeHelper.CombineSequence<object>(1, 2, 3, "4");

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalValuesOfNonComparableTypes()
        {
            var arr1 = new[] { 1 };
            var arr2 = new[] { 2 };
            var arr3 = new[] { 3 };

            var hash1 = HashCodeHelper.CombineSequence(arr1, arr2, arr3);
            var hash2 = HashCodeHelper.CombineSequence(arr1, arr2, arr3);

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsDifferentHashForIdenticalValuesOfNonComparableTypesWithDifferentOrder()
        {
            var arr1 = new[] { 1 };
            var arr2 = new[] { 2 };
            var arr3 = new[] { 3 };

            var hash1 = HashCodeHelper.CombineSequence(arr1, arr2, arr3);
            var hash2 = HashCodeHelper.CombineSequence(arr3, arr2, arr1);

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForEmptyCollections()
        {
            var hash1 = HashCodeHelper.CombineSequence(Array.Empty<int>());
            var hash2 = HashCodeHelper.CombineSequence(Array.Empty<int>());

            Assert.That(hash1, Is.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ReturnsDifferentHashForEmptyCollectionsOfDifferentTypes()
        {
            var hash1 = HashCodeHelper.CombineSequence(Array.Empty<int>());
            var hash2 = HashCodeHelper.CombineSequence(Array.Empty<object>());

            Assert.That(hash1, Is.Not.EqualTo(hash2));
        }

        [Test]
        public void CombineSequence_ThrowsForSingleCollection()
        {
            var values = new List<int>() { 1, 2, 3 };
            Assert.Throws<InvalidOperationException>(() =>
            {
                HashCodeHelper.CombineSequence(values);
                // Expected and required usage is HashCodeHelper.CombineSequence<int>(values);
            });
        }

        [Test]
        public void CombineSequence_ReturnsSameHashForIdenticalCollections()
        {
            var values1 = new List<int>() { 1, 2, 3 };
            var values2 = new List<int>() { 1, 2, 3 };

            var hash1 = HashCodeHelper.CombineSequence<int>(values1);
            var hash2 = HashCodeHelper.CombineSequence<int>(values2);

            Assert.That(hash1, Is.EqualTo(hash2));
        }
        #endregion CombineSequence
    }
}
