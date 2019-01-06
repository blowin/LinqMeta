using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using LinqMetaTest.Utility;
using Xunit;

namespace LinqMetaTest
{
    public class TakeTests
    {
        [Fact]
        public void Take()
        {
            var arr = GlobalCollection.Arr;
            var first = arr.Take(14).Sum();
            var second = arr.MetaOperators().Take(14).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void TakeWhile()
        {
            var arr = GlobalCollection.Arr;
            var first = arr.TakeWhile(i => i.InEq(1, 2, 3, 6, 7)).Sum();
            var second = arr.MetaOperators().TakeWhile(i => i.InEq(1, 2, 3, 6, 7)).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void TakeWhileIndex()
        {
            var arr = GlobalCollection.Arr;
            var first = arr.TakeWhile((i, i1) => i1 != 6).Sum();
            var second = arr.MetaOperators().TakeWhileIndex(pair => pair.Index != 6).Sum();
            Assert.Equal(first, second);
            
            first = arr.TakeWhile((i, i1) => i1 < 8).Sum();
            second = arr.MetaOperators().TakeWhileIndex(pair => pair.Index < 8).Sum();
            Assert.Equal(first, second);
        }
    }
}