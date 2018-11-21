using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions;
using Xunit;

namespace LinqMetaTest
{   
    public class UnitTest1
    {
        private int[] arr = {1, 2, 3, -2, 4, 1, 6, 4, 8, 9, 10, 6, -2, -3, 1};

        static string[] cars = { "Alfa Romeo", "Aston Martin", "Audi", "Nissan", "Chevrolet",  "Chrysler", "Dodge", "BMW", 
            "Ferrari",  "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru" };

        private string[] carsFirst = cars.Take(5).ToArray();
        private string[] carsSecond = cars.Skip(4).ToArray();
        
        private Stack<int> stack = new Stack<int>(Enumerable.Range(0, 10));
        private Dictionary<int, string> _dictionary = Enumerable
            .Repeat("Dima", 10)
            .Select((s, i) => new {Key = i, Name = s + i})
            .ToDictionary(arg => arg.Key, arg => arg.Name);
        
        private object[] heterogeneousArray = {"Test", 12, "Home", "Hello", 2.222, 12u, new object(), new Random(), };
        
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
            
            first = arr.TakeWhile((i, i1) => i1 < 8).Sum();
            second = arr.MetaOperators().TakeWhileIndex(pair => pair.Index < 8).Sum();
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
            Assert.Equal(arr.Any(), arr.MetaOperators().Any());
            Assert.Equal(arr.Any(i => i == 11), arr.MetaOperators().Any(i => i == 11));
        }

        [Fact]
        public void TypeOf()
        {
            var linq = heterogeneousArray.OfType<string>()
                .Aggregate(new StringBuilder(), (builder, s) => builder.Append(s)).ToString();

            var metaLinq = heterogeneousArray.MetaOperators().OfType<string>()
                .Aggregate(new StringBuilder(), (builder, s) => builder.Append(s)).ToString();
            
            Assert.Equal(linq, metaLinq);
        }

        [Fact]
        public void SumStack()
        {
            var linq = stack.Sum();
            var linqMeta = stack.MetaOperators().Sum();

            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void ConcatSum()
        {
            var linq = stack.Concat(arr).Sum();
            var linqMeta = stack.MetaOperators().Concat(arr.MetaWrapper()).Sum();

            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void LastWhere()
        {
            var linq = arr.Last(i => i % 2 == 0);
            var linqMeta = arr.MetaOperators().Last(i => i % 2 == 0);

            Assert.Equal(linq, linqMeta.GetValueOrDefault());
        }
        
        [Fact]
        public void Skip()
        {
            var linq = arr.SkipWhile(i => i < 6).Sum();
            var linqMeta = arr.MetaOperators().SkipWhile(i => i < 6).Sum();

            Assert.Equal(linq, linqMeta);
        }

        [Fact]
        public void SkipWhile()
        {
            var linq = arr.Skip(3).Sum();
            var linqMeta = arr.MetaOperators().Skip(3).Sum();
            Assert.Equal(linq, linqMeta);
        }

        [Fact]
        public void SelectMany()
        {
            var selectMany = Enumerable.Range(0, 24).Select(i => Enumerable.Repeat(i, i).ToList()).ToArray();
            
            var linq = selectMany.SelectMany(ints => ints.TakeWhile((i, i1) => i1 < 3)).Where(i => i % 2 == 0).Sum();

            var linqMeta = selectMany.MetaOperators().SelectManyBox(ints => ints.MetaOperators()
                .TakeWhileIndex(pair => pair.Index < 3).Collect).Where(i => i % 2 == 0).Sum();
            
            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void SkipWhileIndex()
        {
            var linq = arr.SkipWhile((i, i1) => i1 < 4).Sum();

            var linqMeta = arr.MetaOperators().SkipWhileIndex(pair => pair.Index < 4).Sum();
            
            Assert.Equal(linq, linqMeta);
        }
        
        [Fact]
        public void Contain()
        {
            Assert.Equal(arr.Contains(2), arr.MetaOperators().Contains(2));
            Assert.Equal(arr.Contains(-200), arr.MetaOperators().Contains(-200));
            
            Assert.Equal(arr.Contains(2), arr.MetaOperators().ContainsEq(2));
            Assert.Equal(arr.Contains(-200), arr.MetaOperators().ContainsEq(-200));
            
            Assert.Equal(arr.FirstOrDefault(i => i == 10) != default(int), 
                arr.MetaOperators().Contains(i => i == 10));
            
            Assert.Equal(arr.FirstOrDefault(i => i == 1000) != default(int), 
                arr.MetaOperators().Contains(i => i == 1000));
        }
        
        [Fact]
        public void DefaultIfEmpty()
        {
            Assert.Equal(arr.DefaultIfEmpty(20).Sum(), arr.MetaOperators().DefaultIfEmpty(20).Sum());
            
            Assert.Equal(arr.DefaultIfEmpty(20).Sum(), arr.MetaOperators().DefaultIfEmpty(() => 20).Sum());
            
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(20).Sum());
            
            Assert.Equal(Enumerable.Empty<int>().DefaultIfEmpty(20).Sum(), 
                Enumerable.Empty<int>().MetaOperators().DefaultIfEmpty(() => 20).Sum());
        }
        
        [Fact]
        public void Count()
        {
            Assert.Equal(arr.Count(), arr.MetaOperators().Count());
            
            Assert.Equal(Enumerable.Empty<int>().Count(), Enumerable.Empty<int>().MetaOperators().Count());
            
            Assert.Equal(arr.Where(i => i % 2 == 0).Count(), arr.MetaOperators().Where(i => i % 2 == 0).Count());
            
            Assert.Equal(arr.Count(i => i % 2 == 0), arr.MetaOperators().Count(i => i % 2 == 0));
            
            Assert.Equal(arr.Count(i => i > 2000), arr.MetaOperators().Count(i => i > 2000));
        }
        
        [Fact]
        public void Zip()
        {
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

        [Fact]
        public void Reverse()
        {
            Assert.Equal(arr.Reverse().ToArray(), arr.MetaOperators().Reverse().ToArray());

            var linq = arr.Where(i => i % 2 == 0).Reverse().ToArray();
            var metaLinq = arr.MetaOperators().Where(i => i % 2 == 0).Reverse().ToArray();
                
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void ToList()
        {
            var linq = arr.Where(i => i % 2 == 0).Select((i, i1) => i * i1).ToList();
            var metaLinq = arr.MetaOperators().Where(i => i % 2 == 0).SelectIndex(pair => pair.Index * pair.Item)
                .ToList();
            Assert.Equal(linq, metaLinq);

            Assert.Equal(arr.ToList(), arr.MetaOperators().ToList());

            var linq2 = arr.SelectMany(i => Enumerable.Range(0, i)).Take(3).ToList();
            var metaLinq2 = arr.MetaOperators().SelectManyBox(i => Enumerable.Range(0, i).MetaWrapper()).Take(3).ToList();
            Assert.Equal(linq2, metaLinq2);
        }

        [Fact]
        public void ToArray()
        {
            var linq = arr.Take(7).Where(i => i % 2 == 0).ToArray();
            var metaLinq = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).ToArray();
            Assert.Equal(linq, metaLinq);

            var linq2 = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).ToArray();
            var metaLinq2 = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).ToArray();
            Assert.Equal(linq2, metaLinq2);
        }
        
        [Fact]
        public void LongCount()
        {
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).LongCount(), arr.MetaOperators().Take(7).Where(i => i % 2 == 0).LongCount());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).LongCount(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).LongCount());
        }
        
        [Fact]
        public void Average()
        {
            Assert.Equal(arr.Average(), arr.MetaOperators().Average());
            
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).Average(), arr.MetaOperators().Take(7).Where(i => i % 2 == 0).Average());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Average(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).Average());
        }
        
        [Fact]
        public void AverageDec()
        {
            Assert.Equal(arr.Select(i => (decimal)i).Average(), arr.MetaOperators().AverageDec());
            
            Assert.Equal(arr.Take(7).Where(i => i % 2 == 0).Select(i => (decimal)i).Average(), 
                arr.MetaOperators().Take(7).Where(i => i % 2 == 0).AverageDec());
            
            Assert.Equal(arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).Select(i => (decimal)i).Average(), 
                arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).AverageDec());
        }
        
        [Fact]
        public void ToDictionary()
        {
            var uniqSeq = Enumerable.Range(0, 100);
            Assert.Equal(uniqSeq.ToDictionary(i => i), uniqSeq.MetaOperators().ToDictionary(i => i));
        }
        
        [Fact]
        public void Distinct()
        {
            Assert.Equal(arr.Distinct().ToArray(), arr.MetaOperators().Distinct().ToArray());
            
            Assert.Equal(arr.Where(i => i % 2 == 0).Distinct().ToArray(), 
                arr.MetaOperators().Where(i => i % 2 == 0).Distinct().ToArray());
        }

        [Fact]
        public void Union()
        {
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
        
        [Fact]
        public void Intersect()
        {
            var linq = carsFirst.Intersect(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().Intersect(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Where((s, i) => i != 1).Intersect(carsSecond).ToArray();
            metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Intersect(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Intersect(carsSecond).Where((s, i) => i != 1).ToArray();
            metaLinq = carsFirst.MetaOperators().Intersect(carsSecond.MetaWrapper()).WhereIndex(pair => pair.Index != 1)
                .ToArray();
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void Except()
        {
            var linq = carsFirst.Except(carsSecond).ToArray();
            var metaLinq = carsFirst.MetaOperators().Except(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Where((s, i) => i != 1).Except(carsSecond).ToArray();
            metaLinq = carsFirst.MetaOperators().WhereIndex(pair => pair.Index != 1).Except(carsSecond.MetaWrapper()).ToArray();
            Assert.Equal(linq, metaLinq);

            linq = carsFirst.Except(carsSecond).Where((s, i) => i != 1).ToArray();
            metaLinq = carsFirst.MetaOperators().Except(carsSecond.MetaWrapper()).WhereIndex(pair => pair.Index != 1)
                .ToArray();
            Assert.Equal(linq, metaLinq);
        }

        [Fact]
        public void SequenceEqual()
        {
            var linq = arr.SequenceEqual(arr);
            var meta = arr.MetaOperators().SequenceEqual(arr.MetaWrapper());
            Assert.Equal(linq, meta);

            linq = arr.SequenceEqual(arr.TakeWhile(i => i < 4));
            meta = arr.MetaOperators().SequenceEqual(arr.MetaOperators().TakeWhile(i => i < 4).Collect);
            Assert.Equal(linq, meta);
            
            linq = arr.SequenceEqual(arr.Take(3));
            meta = arr.MetaOperators().SequenceEqual(arr.MetaOperators().Take(3).Collect);
            Assert.Equal(linq, meta);
            
            linq = new []{2, 4}.SequenceEqual(Enumerable.Range(0, 5).Where(i => i % 2 == 0));
            meta = new []{2, 4}.MetaOperators().SequenceEqual(Enumerable.Range(0, 5).MetaOperators().Where(i => i % 2 == 0).Collect);
            Assert.Equal(linq, meta);
            
            linq = Enumerable.Range(0, 5).Where(i => i % 2 == 0).SequenceEqual(new []{2, 4});
            meta = Enumerable.Range(0, 5).MetaOperators().Where(i => i % 2 == 0).SequenceEqual(new []{2, 4}.MetaWrapper());
            Assert.Equal(linq, meta);

            linq = arr.Where(i => !i.In(1, 4, 6, 8 ,9)).SequenceEqual(arr.Where(i => !i.In(1, 4, 6, 8 ,9)));
            meta = arr.MetaOperators().Where(i => !i.In(1, 4, 6, 8 ,9)).SequenceEqual(arr.MetaOperators().Where(i => !i.In(1, 4, 6, 8 ,9)).Collect);
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void ForEach()
        {
            var linqForEachSum = 0;
            foreach (var i in arr)
                linqForEachSum += i;

            var metaForEachSum = 0;
            arr.MetaOperators().ForEach(i => metaForEachSum += i);
            
            Assert.Equal(linqForEachSum, metaForEachSum);

            metaForEachSum = 0;
            var iter = arr.MetaOperators().BuildEnumerator();
            while (iter.MoveNext())
                metaForEachSum += iter.Current;

            Assert.Equal(linqForEachSum, metaForEachSum);
        }

        [Fact]
        public void Join()
        {
            var linq = arr.Join(arr.Where((i, i1) => i1 % 2 == 0), i => i.ToString(), i => i.ToString(),
                (i, i1) => i + i1).Sum();

            var meta = arr.MetaOperators()
                .JoinBox(arr.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0).Collect, i => i.ToString(), i => i.ToString(), pair => pair.First + pair.Second).Sum();
            
            Assert.Equal(linq, meta);
        }

        [Fact]
        public void JoinFirstHasOverhead()
        {
            var linq = arr.Where((i, i1) => i1 % 2 == 0).Join(arr, i => i, i => i, (i, i1) => i + i1, default(IntComparer)).Sum();
            var meta = arr.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0)
                .JoinBox(arr.MetaWrapper(), i => i, i => i, pair => pair.First + pair.Second, default(IntComparer)).Sum();
            
            Assert.Equal(linq, meta);
        }
        
        struct IntComparer : IEqualityComparer<int>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Equals(int x, int y)
            {
                return x == y;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}