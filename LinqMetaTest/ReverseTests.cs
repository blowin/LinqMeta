using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ReverseTests
    {
        [Fact]
        public void Reverse()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.Reverse().ToArray(), arr.MetaOperators().Reverse().ToArray());

            var linq = arr.Where(i => i % 2 == 0).Reverse().ToArray();
            var metaLinq = arr.MetaOperators().Where(i => i % 2 == 0).Reverse().ToArray();
                
            Assert.Equal(linq, metaLinq);
        }
    }
}