using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class CountTests
    {
        [Fact]
        public void Count()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Count();
            var meta = arr.MetaOperators().Count();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void EmptyCount()
        {
            var linq = Enumerable.Empty<int>().Count();
            var meta = Enumerable.Empty<int>().MetaOperators().Count();
                
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void WhereCount()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Where(i => i % 2 == 0).Count();
            var meta = arr.MetaOperators().Where(i => i % 2 == 0).Count();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void PredicatCount()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Count(i => i % 2 == 0);
            var meta = arr.MetaOperators().Count(i => i % 2 == 0);
            Assert.Equal(linq, meta);

            linq = arr.Count(i => i > 2000);
            meta = arr.MetaOperators().Count(i => i > 2000);
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void LongCount()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.Take(7).Where(i => i % 2 == 0).LongCount();
            var meta = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).LongCount();
            Assert.Equal(linq, meta);

            linq = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).LongCount();
            meta = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).LongCount();
            Assert.Equal(linq, meta);
        }
    }
}