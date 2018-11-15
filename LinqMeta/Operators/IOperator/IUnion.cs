using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IUnion<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T> Union<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>;
        
        OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T> Union<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>;
    }
}