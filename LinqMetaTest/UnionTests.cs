using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class UnionTests
    {
        [Fact]
        public void Union()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;
            
            var linq = carsFirst.Union(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().Union(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Where((s, i) => i != 1).Union(carsSecond).ToArray();
            metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Union(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Union(carsSecond).Where((s, i) => i != 1).ToArray();
            metaLinq = carsFirst.MetaOperators().Union(carsSecond.MetaWrapper()).WhereIndex(pair => pair.Index != 1)
                .ToArray();
            Assert.Equal(linq, metaLinq);
        }
    }
}