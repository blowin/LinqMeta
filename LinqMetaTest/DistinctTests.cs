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
            
            var linq = arr.Distinct().ToArray();
            var meta = arr.MetaOperators().Distinct().ToArray();
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void WhereDistinct()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Where(i => i % 2 == 0).Distinct().ToArray();
            var meta = arr.MetaOperators().Where(i => i % 2 == 0).Distinct().ToArray();
            
            Assert.Equal(linq, meta);
        }
    }
}