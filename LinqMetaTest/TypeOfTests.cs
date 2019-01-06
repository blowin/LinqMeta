using System.Linq;
using System.Text;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class TypeOfTests
    {
        [Fact]
        public void TypeOf()
        {
            var heterogeneousArray = GlobalCollection.HeterogeneousArray;
            
            var linq = heterogeneousArray.OfType<string>()
                .Aggregate(new StringBuilder(), (builder, s) => builder.Append(s)).ToString();

            var metaLinq = heterogeneousArray.MetaOperators().OfType<string>()
                .Aggregate(new StringBuilder(), (builder, s) => builder.Append(s)).ToString();
            
            Assert.Equal(linq, metaLinq);
        }
    }
}