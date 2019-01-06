using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class JoinTests
    {
        [Fact]
        public void Join()
        {
            var arr = GlobalCollection.Arr;
            var linq = arr.Join(arr.Where((i, i1) => i1 % 2 == 0), i => i.ToString(), i => i.ToString(),
                (i, i1) => i + i1).Sum();

            var meta = arr.MetaOperators()
                .JoinBox(arr.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0).Collect, i => i.ToString(), i => i.ToString(), pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void JoinFirstHasOverhead()
        {
            var arr = GlobalCollection.Arr;
            var linq = arr.Where((i, i1) => i1 % 2 == 0).Join(arr, i => i, i => i, (i, i1) => i + i1).Sum();
            var meta = arr.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0)
                .JoinBox(arr.MetaWrapper(), i => i, i => i, pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linq, meta);
        }
    }
}