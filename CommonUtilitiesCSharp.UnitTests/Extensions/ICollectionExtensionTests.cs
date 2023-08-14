using CommonUtilitiesCSharp.Extensions;
using System.Collections.ObjectModel;

namespace CommonUtilitiesCSharp.UnitTests.Extensions
{
    public class ICollectionExtensionTests : ICollectionExtensionTestBase<ICollection<object>>
    {
        protected override ICollection<object> CreateEmptyCollection() => new Collection<object>();
        
        protected override int GetCount(ICollection<object> collection) => collection.Count;

        protected override void AddRange(ICollection<object> collection, IEnumerable<object> items) => collection.AddRange(items);
    }
}