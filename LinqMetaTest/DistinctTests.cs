using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class DistinctTests
    {
        [Fact]
        public void Distinct()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.Distinct().ToArray(), arr.MetaOperators().Distinct().ToArray());
            
            Assert.Equal(arr.Where(i => i % 2 == 0).Distinct().ToArray(), 
                arr.MetaOperators().Where(i => i % 2 == 0).Distinct().ToArray());
        }
    }
}