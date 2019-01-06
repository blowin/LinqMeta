using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class CountTests
    {
        [Fact]
        public void Count()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Count(), arr.MetaOperators().Count());
            
            Assert.Equal(Enumerable.Empty<int>().Count(), Enumerable.Empty<int>().MetaOperators().Count());
            
            Assert.Equal(arr.Where(i => i % 2 == 0).Count(), arr.MetaOperators().Where(i => i % 2 == 0).Count());
            
            Assert.Equal(arr.Count(i => i % 2 == 0), arr.MetaOperators().Count(i => i % 2 == 0));
            
            Assert.Equal(arr.Count(i => i > 2000), arr.MetaOperators().Count(i => i > 2000));
        }
        
        [Fact]
        public void LongCount()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).LongCount(), arr.MetaOperators().Take(7).Where(i => i % 2 == 0).LongCount());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).LongCount(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).LongCount());
        }
    }
}