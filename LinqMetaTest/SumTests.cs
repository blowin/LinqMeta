using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class SumTests
    {
        [Fact]
        public void Sum()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.Sum(), arr.MetaOperators().Sum());
        }
        
        [Fact]
        public void SumStack()
        {
            var stack = GlobalCollection.Stack;
            
            var linq = stack.Sum();
            var linqMeta = stack.MetaOperators().Sum();

            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void ConcatSum()
        {
            var arr = GlobalCollection.Arr;
            var stack = GlobalCollection.Stack;
            
            var linq = stack.Concat(arr).Sum();
            var linqMeta = stack.MetaOperators().Concat(arr.MetaWrapper()).Sum();

            Assert.Equal(linq, linqMeta);
        }
    }
}