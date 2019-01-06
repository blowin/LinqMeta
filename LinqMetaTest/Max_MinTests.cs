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
            Assert.Equal(arr.Min(), arr.MetaOperators().Min());
        }
        
        [Fact]
        public void Max()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Max(), arr.MetaOperators().Max());
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