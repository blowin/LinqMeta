using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ToAnotherCollectionTests
    {
        [Fact]
        public void ToList()
        {
            var arr = GlobalCollection.Arr;
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
            var arr = GlobalCollection.Arr;
            var linq = arr.Take(7).Where(i => i % 2 == 0).ToArray();
            var metaLinq = arr.MetaOperators().Take(7).Where(i => i % 2 == 0).ToArray();
            Assert.Equal(linq, metaLinq);

            var linq2 = arr.Concat(Enumerable.Repeat(2, 5)).Skip(3).ToArray();
            var metaLinq2 = arr.MetaOperators().Concat(Enumerable.Repeat(2, 5).MetaWrapper()).Skip(3).ToArray();
            Assert.Equal(linq2, metaLinq2);
        }
        
        [Fact]
        public void ToDictionary()
        {
            var uniqSeq = Enumerable.Range(0, 100);
            Assert.Equal(uniqSeq.ToDictionary(i => i), uniqSeq.MetaOperators().ToDictionary(i => i));
        }
    }
}