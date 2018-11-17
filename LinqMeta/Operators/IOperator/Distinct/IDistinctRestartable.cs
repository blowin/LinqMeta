using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Distinct
{
    public interface IDistinctRestartable<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T> DistinctRestartable();
        
        OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T> DistinctRestartable(IEqualityComparer<T> comparer);
    }
}