using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class IntersectTests
    {
        [Fact]
        public void Intersect()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Intersect(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().Intersect(carsSecond.MetaWrapper()).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void WhereIntersect()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Where((s, i) => i != 1).Intersect(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Intersect(carsSecond.MetaWrapper()).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void WhereIndexIntersect()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Where((s, i) => i != 1).Intersect(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Intersect(carsSecond.MetaWrapper()).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
    }
}