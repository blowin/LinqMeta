using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class AverageTests
    {
        [Fact]
        public void Average()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Average(), arr.MetaOperators().Average());
            
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).Average(), arr.MetaOperators().Take(7).Where(i => i % 2 == 0).Average());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Average(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).Average());
        }
        
        [Fact]
        public void AverageDec()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Select(i => (decimal)i).Average(), arr.MetaOperators().AverageDec());
            
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).Select(i => (decimal)i).Average(), 
                arr.MetaOperators().Take(7).Where(i => i % 2 == 0).AverageDec());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Select(i => (decimal)i).Average(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).AverageDec());
        }
    }
}