using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IExcept<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T> Except<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>;
        
        OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T> Except<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>;
    }
}