using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class Max_MinTests
    {
        [Fact]
        public void Min()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Min();
            var meta = arr.MetaOperators().Min();
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void Max()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Max();
            var meta = arr.MetaOperators().Max();
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void MaxMin()
        {
            var arr = GlobalCollection.Arr;
            
            var minLinq = arr.Min();
            var maxLinq = arr.Max();

            var maxMinMeta = arr.MetaOperators().MaxMin().GetValueOrDefault();
            
            Assert.Equal(minLinq, maxMinMeta.Min);
            Assert.Equal(maxLinq, maxMinMeta.Max);
        }
    }
}