using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class WhereTests
    {
        [Fact]
        public void Where()
        {
            var arr = GlobalCollection.Arr;
            
            var first = arr.Where(i => i % 2 == 0).Sum();
            var second = arr.MetaOperators().Where(i => i % 2 == 0).Sum();
            
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereIndex()
        {
            var arr = GlobalCollection.Arr;
            
            var first = arr.Where((i, i1) => i1 < 4).Sum();
            var second = arr.MetaOperators().WhereIndex(pair => pair.Index < 4).Sum();
            
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereSelectSum()
        {
            var arr = GlobalCollection.Arr;
            
            var first = arr.Where(i => i % 2 == 0).Select(i => i - 1).Sum();
            var second = arr.MetaOperators().Where(i => i % 2 == 0).Select(i => i - 1).Sum();
            
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void LastWhere()
        {
            var arr = GlobalCollection.Arr;
            
            var linq = arr.Last(i => i % 2 == 0);
            var linqMeta = arr.MetaOperators().Last(i => i % 2 == 0);

            Assert.Equal(linq, linqMeta.GetValueOrDefault());
        }
    }
}