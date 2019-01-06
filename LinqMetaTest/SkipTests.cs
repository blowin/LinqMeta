using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class SkipTests
    {
        [Fact]
        public void Skip()
        {
            var arr = GlobalCollection.Arr;
            var linq = arr.SkipWhile(i => i < 6).Sum();
            var linqMeta = arr.MetaOperators().SkipWhile(i => i < 6).Sum();

            Assert.Equal(linq, linqMeta);
        }

        [Fact]
        public void SkipWhile()
        {
            var arr = GlobalCollection.Arr;
            var linq = arr.Skip(3).Sum();
            var linqMeta = arr.MetaOperators().Skip(3).Sum();
            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void SkipWhileIndex()
        {
            var arr = GlobalCollection.Arr;
            var linq = arr.SkipWhile((i, i1) => i1 < 4).Sum();

            var linqMeta = arr.MetaOperators().SkipWhileIndex(pair => pair.Index < 4).Sum();
            
            Assert.Equal(linq, linqMeta);
        }

    }
}