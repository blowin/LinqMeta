using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Intersect
{
    public interface IIntersectRestartable<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T> IntersectRestartable<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>;
        
        OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T> IntersectRestartable<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>;
    }
}