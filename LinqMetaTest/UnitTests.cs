using System.Linq;
using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions;
using LinqMeta.Extensions.Converters;
using LinqMeta.Extensions.Operators;
using LinqMeta.Functors;
using LinqMeta.Operators;
using Xunit;

namespace LinqMetaTest
{   
    public class UnitTest1
    {
        private int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, -2, -3};
        
        [Fact]
        public void Sum()
        {
            Assert.Equal(arr.Sum(), arr.MetaOperators().SumMeta());
        }
        
        [Fact]
        public void Min()
        {
            Assert.Equal(arr.Min(), arr.MetaOperators().MinMeta());
        }
        
        [Fact]
        public void Max()
        {
            Assert.Equal(arr.Max(), arr.MetaOperators().MaxMeta());
        }
        
        [Fact]
        public void Aggregate()
        {
            Assert.Equal(arr.Aggregate(1.0, (i, i1) => i / i1), arr.MetaOperators().AggregateMeta(1.0, (i, i1) => i / i1));
        }

        [Fact]
        public void Select()
        {
            Assert.Equal(arr.Select(i => (double)i).Sum(), arr.MetaOperators().SelectMeta(i => (double)i).SumMeta());
        }

        [Fact]
        public void Where()
        {
            Assert.Equal(arr.Where(i => i % 2 == 0).ToArray().Sum(), arr.MetaOperators().WhereMeta(i => i % 2 == 0).SumMeta());
        }

        [Fact]
        public void SelectIndex()
        {
            var first = arr.Select((i, i1) => i + i1).Sum();
            var second = arr.MetaOperators().SelectIndexMeta(pair => pair.Index + pair.Item).SumMeta();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereIndex()
        {
            var first = arr.Where((i, i1) => i1 < 4).Sum();
            var second = arr.MetaOperators().WhereIndexMeta(pair => pair.Index < 4).SumMeta();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void Take()
        {
            var first = arr.Take(14).Sum();
            var second = arr.MetaOperators().TakeMeta(14).SumMeta();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereSelectSum()
        {
            var first = arr.Where(i => i % 2 == 0).Select(i => i - 1).Sum();
            var second = arr.MetaOperators().WhereMeta(i => i % 2 == 0).SelectMeta(i => i - 1).SumMeta();
            Assert.Equal(first, second);
        }
    }
}