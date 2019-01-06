using System.Linq;
using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ZipTests
    {
        [Fact]
        public void Zip()
        {
            var arr = GlobalCollection.Arr;
            
            var linqSum = arr.Zip(Enumerable.Repeat(5, 5), (i, i1) => i + i1).Sum();
            var metaLinqSum = arr.MetaOperators().Zip<ArrayWrapper<int>, int>(Enumerable.Repeat(5, 5).ToArray().MetaWrapper())
                .Select(pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linqSum, metaLinqSum);

            var linqSum2 = arr.Zip(Enumerable.Repeat(5, 5), (i, i1) => i + i1).Sum();
            var metaLinqSum2 = arr.MetaOperators().ZipSelect<EnumeratorWrapper<int>, int>(Enumerable.Repeat(5, 5).MetaWrapper(), pair => pair.First + pair.Second).Sum();
            Assert.Equal(linqSum2, metaLinqSum2);

            linqSum = arr.Zip(Enumerable.Repeat(5, 5), (i, i1) => i + i1).Sum();
            metaLinqSum = arr.MetaOperators()
                .ZipBoxSelect(Enumerable.Repeat(5, 5).MetaWrapper(), pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linqSum, metaLinqSum);
        }
    }
}