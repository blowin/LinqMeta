using System.Linq;
using LinqMeta.DataTypes.Statistic;
using LinqMeta.Extensions;
using LinqMetaCore;
using Xunit;

namespace LinqMetaTest
{   
    public class UnitTest1
    {
        private int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, -2, -3};
        
        [Fact]
        public void Sum()
        {
            Assert.Equal(arr.Sum(), arr.MetaOperators().Sum());
        }
        
        [Fact]
        public void Min()
        {
            Assert.Equal(arr.Min(), arr.MetaOperators().Min());
        }
        
        [Fact]
        public void Max()
        {
            Assert.Equal(arr.Max(), arr.MetaOperators().Max());
        }
        
        [Fact]
        public void Aggregate()
        {
            Assert.Equal(arr.Aggregate(1.0, (i, i1) => i / i1), arr.MetaOperators().Aggregate(1.0, (i, i1) => i / i1));
        }

        [Fact]
        public void Select()
        {
            Assert.Equal(arr.Select(i => (double)i).Sum(), arr.MetaOperators().Select(i => (double)i).Sum());
        }

        [Fact]
        public void Where()
        {
            var first = arr.Where(i => i % 2 == 0).Sum();
            var second = arr.MetaOperators().Where(i => i % 2 == 0).Sum();
            Assert.Equal(first, second);
        }

        [Fact]
        public void SelectIndex()
        {
            var first = arr.Select((i, i1) => i + i1).Sum();
            var second = arr.MetaOperators().SelectIndex(pair => pair.Index + pair.Item).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereIndex()
        {
            var first = arr.Where((i, i1) => i1 < 4).Sum();
            var second = arr.MetaOperators().WhereIndex(pair => pair.Index < 4).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void Take()
        {
            var first = arr.Take(14).Sum();
            var second = arr.MetaOperators().Take(14).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void TakeWhile()
        {
            var first = arr.TakeWhile(i => i.InEq(1, 2, 3, 6, 7)).Sum();
            var second = arr.MetaOperators().TakeWhile(i => i.InEq(1, 2, 3, 6, 7)).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void TakeWhileIndex()
        {
            var first = arr.TakeWhile((i, i1) => i1 != 6).Sum();
            var second = arr.MetaOperators().TakeWhileIndex(pair => pair.Index != 6).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void WhereSelectSum()
        {
            var first = arr.Where(i => i % 2 == 0).Select(i => i - 1).Sum();
            var second = arr.MetaOperators().Where(i => i % 2 == 0).Select(i => i - 1).Sum();
            Assert.Equal(first, second);
        }
        
        [Fact]
        public void MaxMin()
        {
            var minLinq = arr.Min();
            var maxLinq = arr.Max();

            var maxMinMeta = arr.MetaOperators().MaxMin().GetValueOrDefault();
            
            Assert.Equal(minLinq, maxMinMeta.Min);
            Assert.Equal(maxLinq, maxMinMeta.Max);
        }
        
        [Fact]
        public void All()
        {
            Assert.Equal(arr.All(i => i < 100), arr.MetaOperators().All(i => i < 100));
            
            Assert.Equal(arr.All(i => i == 5), arr.MetaOperators().All(i => i == 5));
        }
        
        [Fact]
        public void Any()
        {
            var eith = new Union<string, int>();
            
            Assert.Equal(arr.Any(), arr.MetaOperators().Any());
            Assert.Equal(arr.Any(i => i == 11), arr.MetaOperators().Any(i => i == 11));
        }
        
        /*
         var metaVarUse = arr.MetaOperators().SelectIndex(pair => pair.Index + pair.Item).Where(i => i % 2 == 0)
                .Take(5);
            
            CollectWrapper<TakeOperator<WhereOperator<SelectIndexingOperator<ArrayWrapper<int>, FuncFunctor<ZipPair<int>, int>, int, int>, FuncFunctor<int, bool>, int>, int>, int> metaFullTypeUse = arr.MetaOperators().SelectIndex(pair => pair.Index + pair.Item).Where(i => i % 2 == 0)
                .Take(5);
         */
     /*   
        [Fact]
        public void Zip()
        {
            var linqSum = arr.Zip(Enumerable.Repeat(5, 5), (i, i1) => i + i1).Sum();
            var metaLinqSum = arr.MetaOperators().Zip<ArrayWrapper<int>, int>(Enumerable.Repeat(5, 5).ToArray().GetMetaIter())
                .Select(pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linqSum, metaLinqSum);

            var linqSum2 = arr.Zip(Enumerable.Repeat(5, 5), (i, i1) => i + i1).Sum();
            var metaLinqSum2 = arr.MetaOperators().ZipSelect(Enumerable.Repeat(5, 5).ToArray().GetMetaIter(), pair => pair.First + pair.Second).Sum();
        }
        */
        [Fact]
        public void Statistic()
        {
            var first = arr.MetaOperators().GetStatistic(StatisticValue.Sum | StatisticValue.Minus);
            Assert.True(first.HasValue && 
                        first.Value.Sum.HasValue && 
                        first.Value.Average.HasValue &&
                        first.Value.Minus.HasValue &&
                        !first.Value.Product.HasValue);
        }
    }
}