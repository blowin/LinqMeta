using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class SelectTests
    {
        [Fact]
        public void Select()
        {
            var arr = GlobalCollection.Arr;
            Assert.Equal(arr.Select(i => (double)i).Sum(), arr.MetaOperators().Select(i => (double)i).Sum());
        }
        
        [Fact]
        public void SelectIndex()
        {
            var arr = GlobalCollection.Arr;
            var first = arr.Select((i, i1) => i + i1).Sum();
            var second = arr.MetaOperators().SelectIndex(pair => pair.Index + pair.Item).Sum();
            Assert.Equal(first, second);
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
    }
}