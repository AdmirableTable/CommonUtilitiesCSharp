using CommonUtilitiesCSharp.Extensions;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class IListExtensionTests : ICollectionExtensionTestBase<IList<object>>
    {
        protected override void AddRange(IList<object> list, IEnumerable<object> items) => list.AddRange(items);

        protected override IList<object> CreateEmptyCollection() => new List<object>();

        protected override int GetCount(IList<object> list) => list.Count;
    }
}