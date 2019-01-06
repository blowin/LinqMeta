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
        }
        
        [Fact]
        public void FirstOrDefaultContain()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.FirstOrDefault(i => i == 10) != default(int);
            var meta = arr.MetaOperators().Contains(i => i == 10);
            
            Assert.Equal(linq, meta);

            linq = arr.FirstOrDefault(i => i == 1000) != default(int);
            meta = arr.MetaOperators().Contains(i => i == 1000);
            
            Assert.Equal(linq, meta);
        }
    }
}