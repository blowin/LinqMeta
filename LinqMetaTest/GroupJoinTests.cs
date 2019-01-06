using System.Linq;
using LinqMeta.Extensions;
using LinqMetaTest.Types;
using Xunit;

namespace LinqMetaTest
{
    public class GroupJoinTests
    {
        [Fact]
        public void GroupJoin()
        {
            var employees = Employee.GetEmployeesArrayList();
            var empOptions = EmployeeOptionEntry.GetEmployeeOptionEntries();

            var linq = employees
                .GroupJoin(
                    empOptions,
                    e => e.id,
                    o => o.id,
                    (e, os) => new
                    {
                        id = e.id,
                        name = $"{e.firstName} {e.lastName}",
                        options = os.Sum(o => o.optionsCount)
                    }).ToArray();

            var meta = employees.MetaOperators()
                .GroupJoinBox(
                    empOptions.MetaWrapper(),
                    e => e.id,
                    o => o.id,
                    pair => new
                    {
                        id = pair.First.id,
                        name = $"{pair.First.firstName} {pair.First.lastName}",
                        options = pair.Second.MetaOperators().Select(o => o.optionsCount).Sum()
                    }).ToArray();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void GroupJoinFirstHasOverhead()
        {
            var employees = Employee.GetEmployeesArrayList();
            var empOptions = EmployeeOptionEntry.GetEmployeeOptionEntries();

            var linq = employees.Where((employee, i) => i % 2 == 0)
                .GroupJoin(
                    empOptions,
                    e => e.id,
                    o => o.id,
                    (e, os) => new
                    {
                        id = e.id,
                        name = $"{e.firstName} {e.lastName}",
                        options = os.Sum(o => o.optionsCount)
                    }).ToArray();

            var meta = employees.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0)
                .GroupJoinBox(
                    empOptions.MetaWrapper(),
                    e => e.id,
                    o => o.id,
                    pair => new
                    {
                        id = pair.First.id,
                        name = $"{pair.First.firstName} {pair.First.lastName}",
                        options = pair.Second.MetaOperators().Select(o => o.optionsCount).Sum()
                    }).ToArray();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void GroupJoinSecondHasOverhead()
        {
            var employees = Employee.GetEmployeesArrayList();
            var empOptions = EmployeeOptionEntry.GetEmployeeOptionEntries();
            
            
            var linq = employees
                .GroupJoin(
                    empOptions.Where((entry, i) => i % 2 == 0),
                    e => e.id,
                    o => o.id,
                    (e, os) => new
                    {
                        id = e.id,
                        name = $"{e.firstName} {e.lastName}",
                        options = os.Sum(o => o.optionsCount)
                    }).ToArray();

            var meta = employees.MetaOperators()
                .GroupJoinBox(
                    empOptions.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0).Collect,
                    e => e.id,
                    o => o.id,
                    pair => new
                    {
                        id = pair.First.id,
                        name = $"{pair.First.firstName} {pair.First.lastName}",
                        options = pair.Second.MetaOperators().Select(o => o.optionsCount).Sum()
                    }).ToArray();
            
            Assert.Equal(linq, meta);
        }
        
        [Fact]
        public void GroupJoinHasOverhead()
        {
            var employees = Employee.GetEmployeesArrayList();
            var empOptions = EmployeeOptionEntry.GetEmployeeOptionEntries();

            var linq = employees.Where((employee, i) => i % 2 == 0)
                .GroupJoin(
                    empOptions.Where((employee, i) => i % 2 == 0),
                    e => e.id,
                    o => o.id,
                    (e, os) => new
                    {
                        id = e.id,
                        name = $"{e.firstName} {e.lastName}",
                        options = os.Sum(o => o.optionsCount)
                    }).ToArray();

            var meta = employees.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0)
                .GroupJoinBox(
                    empOptions.MetaOperators().WhereIndex(pair => pair.Index % 2 == 0).Collect,
                    e => e.id,
                    o => o.id,
                    pair => new
                    {
                        id = pair.First.id,
                        name = $"{pair.First.firstName} {pair.First.lastName}",
                        options = pair.Second.MetaOperators().Select(o => o.optionsCount).Sum()
                    }).ToArray();
            
            Assert.Equal(linq, meta);
        }
    }
}