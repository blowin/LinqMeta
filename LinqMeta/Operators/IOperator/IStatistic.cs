using LinqMeta.DataTypes.Statistic;

namespace LinqMeta.Operators.IOperator
{
    public interface IStatistic<T>
    {
        StatisticInfo<T>? GetStatistic(StatisticValue flagsBuff);
    }
}