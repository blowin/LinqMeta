using System.Linq;
using LinqMeta.Extensions;
using Xunit;

namespace LinqMetaTest
{
    public class GrupByTest
    {
        
        [Fact]
        public void GroupBy()
        {
            var linq = EmployeeOptionEntry
                .GetEmployeeOptionEntries()
                .GroupBy(entry => entry.id)
                .Select(entries => entries.ToList())
                .ToList();

            var meta = EmployeeOptionEntry
                .GetEmployeeOptionEntries().MetaOperators()
                .GroupBy(entry => entry.id)
                .Select(pair => pair.Second.MetaOperators().ToList())
                .ToList();
            
            Assert.True(EmployeeOptionEntry.Compare(linq, meta));
        }
    }
}