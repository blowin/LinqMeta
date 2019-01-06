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

            var linq = arr.All(i => i < 100);
            var meta = arr.MetaOperators().All(i => i < 100);
            
            Assert.Equal(linq, meta);

            linq = arr.All(i => i == 5);
            meta = arr.MetaOperators().All(i => i == 5);
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void Any()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Any();
            var meta = arr.MetaOperators().Any();
            Assert.Equal(linq, meta);

            linq = arr.Any(i => i == 11);
            meta = arr.MetaOperators().Any(i => i == 11);
            Assert.Equal(linq, meta);
        }
    }
}