using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Intersect
{
    public interface IIntersect<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T> Intersect<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>;
        
        OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T> Intersect<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>;
    }
}