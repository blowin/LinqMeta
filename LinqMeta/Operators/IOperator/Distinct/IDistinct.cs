using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Distinct
{
    public interface IDistinct<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<DistinctOperator<TCollect, T>, T> Distinct();
        
        OperatorWrapper<DistinctOperator<TCollect, T>, T> Distinct(IEqualityComparer<T> comparer);
    }
}