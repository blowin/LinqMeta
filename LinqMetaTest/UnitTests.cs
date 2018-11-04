using System.Linq;
using LinqMeta.Extensions.Operators.Aggregate;
using LinqMeta.Extensions.Operators.Max;
using LinqMeta.Extensions.Operators.Min;
using LinqMeta.Extensions.Operators.Sum;
using Xunit;

namespace LinqMetaTest
{   
    public class UnitTest1
    {
        private int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, -2, -3};
        
        [Fact]
        public void Sum()
        {
            Assert.Equal(arr.Sum(), arr.SumMeta());
        }
        
        [Fact]
        public void Min()
        {
            Assert.Equal(arr.Min(), arr.MinMeta());
        }
        
        [Fact]
        public void Max()
        {
            Assert.Equal(arr.Max(), arr.MaxMeta());
        }
        
        [Fact]
        public void Aggregate()
        {
            Assert.Equal(arr.Aggregate(1.0, (i, i1) => i / i1), arr.AggregateMeta(1.0, (i, i1) => i / i1));
        }
    }
}