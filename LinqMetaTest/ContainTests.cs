using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ContainTests
    {
        [Fact]
        public void Contain()
        {
            var arr = GlobalCollection.Arr;
            
            Assert.Equal(arr.Contains(2), arr.MetaOperators().Contains(2));
            Assert.Equal(arr.Contains(-200), arr.MetaOperators().Contains(-200));
            
            Assert.Equal(arr.Contains(2), arr.MetaOperators().ContainsEq(2));
            Assert.Equal(arr.Contains(-200), arr.MetaOperators().ContainsEq(-200));
            
            Assert.Equal(arr.FirstOrDefault(i => i == 10) != default(int), 
                arr.MetaOperators().Contains(i => i == 10));
            
            Assert.Equal(arr.FirstOrDefault(i => i == 1000) != default(int), 
                arr.MetaOperators().Contains(i => i == 1000));
        }
    }
}