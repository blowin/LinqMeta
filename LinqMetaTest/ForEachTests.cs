using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class ForEachTests
    {
        [Fact]
        public void LambdaForEach()
        {
            var arr = GlobalCollection.Arr;
            
            var linqForEachSum = 0;
            foreach (var i in arr)
                linqForEachSum += i;

            var metaForEachSum = 0;
            arr.MetaOperators().ForEach(i => metaForEachSum += i);
            
            Assert.Equal(linqForEachSum, metaForEachSum);

            metaForEachSum = 0;
            foreach (var item in arr.MetaOperators())
                metaForEachSum += item;

            Assert.Equal(linqForEachSum, metaForEachSum);
        }
    }
}