using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ToAnotherCollectionTests
    {
        [Fact]
        public void WhereToList()
        {
            var arr = GlobalCollection.Arr;
            
            var linq = arr.Where(i => i % 2 == 0).Select((i, i1) => i * i1).ToList();
            var metaLinq = arr.MetaOperators().Where(i => i % 2 == 0).SelectIndex(pair => pair.Index * pair.Item)
                .ToList();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void ToList()
        {
            var arr = GlobalCollection.Arr;

            var linq = arr.ToList();
            var meta = arr.MetaOperators().ToList();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void SelectManyTakeToList()
        {
            var arr = GlobalCollection.Arr;

            var linq2 = arr.SelectMany(i => Enumerable.Range(0, i)).Take(3).ToList();
            var metaLinq2 = arr.MetaOperators().SelectManyBox(i => Enumerable.Range(0, i).MetaWrapper()).Take(3).ToList();
            
            Assert.Equal(linq2, metaLinq2);
        }

        [Fact]
        public void WhereToArray()
        {
            var arr = GlobalCollection.Arr;
            
            var linq = arr.Take(7).Where(i => i % 2 == 0).ToArray();
            var metaLinq = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).ToArray();
            
            Assert.Equal(linq, metaLinq);
        }
        
        [Fact]
        public void ToArray()
        {
            var arr = GlobalCollection.Arr;
            
            var linq2 = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).ToArray();
            var metaLinq2 = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).ToArray();
            
            Assert.Equal(linq2, metaLinq2);
        }
        
        [Fact]
        public void ToDictionary()
        {
            const int start = 0;
            const int count = 100;

            var linq = Enumerable.Range(start, count).ToDictionary(i => i);
            var meta = Enumerable.Range(start, count).MetaOperators().ToDictionary(i => i);
            
            Assert.Equal(linq, meta);
        }
    }
}