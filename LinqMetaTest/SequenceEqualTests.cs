using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using LinqMetaTest.Utility;
using Xunit;

namespace LinqMetaTest
{
    public class SequenceEqualTests
    {
        private int[] arr = GlobalCollection.Arr;
        
        [Fact]
        public void SequenceEqual()
        {
            var linq = arr.SequenceEqual(arr);
            var meta = arr.MetaOperators().SequenceEqual(arr.MetaWrapper());
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void SequenceEqualFirstHasOverhead()
        {
            var linq = arr.SequenceEqual(arr.TakeWhile(i => i < 4));
            var meta = arr.MetaOperators().SequenceEqual(arr.MetaOperators().TakeWhile(i => i < 4).Collect);
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void SequenceEqualSecondHasOverhead()
        {
            var linq = arr.Where(i => !i.In(1, 4, 6, 8 ,9)).SequenceEqual(arr.Where(i => !i.In(1, 4, 6, 8 ,9)));
            var meta = arr.MetaOperators().Where(i => !i.In(1, 4, 6, 8 ,9)).SequenceEqual(arr.MetaOperators().Where(i => !i.In(1, 4, 6, 8 ,9)).Collect);
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void SequenceEqualHasOverhead()
        {
            var linq = new []{2, 4}.SequenceEqual(Enumerable.Range(0, 5).Where(i => i % 2 == 0));
            var meta = new []{2, 4}.MetaOperators().SequenceEqual(Enumerable.Range(0, 5).MetaOperators().Where(i => i % 2 == 0).Collect);
            
            Assert.Equal(linq, meta);
            
            linq = Enumerable.Range(0, 5).Where(i => i % 2 == 0).SequenceEqual(new []{2, 4});
            meta = Enumerable.Range(0, 5).MetaOperators().Where(i => i % 2 == 0).SequenceEqual(new []{2, 4}.MetaWrapper());
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void SequenceEqualTake()
        {
            var linq = arr.SequenceEqual(arr.Take(3));
            var meta = arr.MetaOperators().SequenceEqual(arr.MetaOperators().Take(3).Collect);
            
            Assert.Equal(linq, meta);
        }
    }
}