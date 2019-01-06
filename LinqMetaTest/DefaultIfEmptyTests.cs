using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class DefaultIfEmptyTests
    {
        [Fact]
        public void DefaultIfEmptySum()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.DefaultIfEmpty(20).Sum();
            var meta = arr.MetaOperators().DefaultIfEmpty(20).Sum();
            Assert.Equal(linq, meta);

            linq = arr.DefaultIfEmpty(20).Sum();
            meta = arr.MetaOperators().DefaultIfEmpty(() => 20).Sum();
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void EmptyDefaultIfEmpty()
        {
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(20).Sum());
            
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(() => 20).Sum());
        }
    }
}