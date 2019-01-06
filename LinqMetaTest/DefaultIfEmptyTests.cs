using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class DefaultIfEmptyTests
    {
        [Fact]
        public void DefaultIfEmpty()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.DefaultIfEmpty(20).Sum(), arr.MetaOperators().DefaultIfEmpty(20).Sum());
            
            Assert.Equal(arr.DefaultIfEmpty(20).Sum(), arr.MetaOperators().DefaultIfEmpty(() => 20).Sum());
            
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(20).Sum());
            
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(() => 20).Sum());
        }
    }
}