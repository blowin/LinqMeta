using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class AggregateTests
    {
        [Fact]
        public void Aggregate()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Aggregate(1.0, (i, i1) => i / i1);
            var meta = arr.MetaOperators().Aggregate(1.0, (i, i1) => i / i1);
            
            Assert.Equal(linq, meta);
        }
    }
}