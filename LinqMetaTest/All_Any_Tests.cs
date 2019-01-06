using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class All_Any_Tests
    {
        [Fact]
        public void All()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.All(i => i < 100), arr.MetaOperators().All(i => i < 100));
            
            Assert.Equal(arr.All(i => i == 5), arr.MetaOperators().All(i => i == 5));
        }
        
        [Fact]
        public void Any()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.Any(), arr.MetaOperators().Any());
            Assert.Equal(arr.Any(i => i == 11), arr.MetaOperators().Any(i => i == 11));
        }
    }
}