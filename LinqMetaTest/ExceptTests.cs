using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ExceptTests
    {
        [Fact]
        public void Except()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Except(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().Except(carsSecond.MetaWrapper()).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void WhereIndexExcept()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Where((s, i) => i != 1).Except(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Except(carsSecond.MetaWrapper()).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void WhereExcept()
        {
            var carsFirst = GlobalCollection.CarsFirst;
            var carsSecond = GlobalCollection.CarsSecond;

            var linq = carsFirst.Except(carsSecond).Where((s, i) => i != 1).ToArray();
            var metaLinq = carsFirst.MetaOperators().Except(carsSecond.MetaWrapper()).WhereIndex(pair => pair.Index != 1).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
    }
}