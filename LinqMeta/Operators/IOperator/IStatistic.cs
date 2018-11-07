using LinqMeta.Core.Statistic;

namespace LinqMeta.Operators.IOperator
{
    public interface IStatistic<T>
    {
        StatisticInfo<T>? GetStatistic(StatisticFlags statisticFlags);
    }
}