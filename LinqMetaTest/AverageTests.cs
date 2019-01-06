using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class AverageTests
    {
        private int[] arr = GlobalCollection.Arr;
        
        [Fact]
        public void Average()
        {
            var linq = arr.Average();
            var meta = arr.MetaOperators().Average();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void TakeWhereAverage()
        {
            var linq = arr.Take(7).Where(i => i % 2 == 0).Average();
            var meta = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).Average();
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void ConcatSkipAverage()
        {
            var linq = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Average();
            var meta = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).Average();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void AverageDec()
        {
            var linq = arr.Select(i => (decimal) i).Average();
            var meta = arr.MetaOperators().AverageDec();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void TakeWhereSelectAverageDec()
        {
            var linq = arr.Take(7).Where(i => i % 2 == 0).Select(i => (decimal) i).Average();
            var meta = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).AverageDec();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void ConcatSkipSelectAverageDec()
        {
            var linq = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Select(i => (decimal) i).Average();
            var meta = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).AverageDec();
            
            Assert.Equal(linq, meta);
        }
    }
}