using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.DefaultIfEmpty
{
    public interface ILazyDefaultIfEmpty<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, TFactory>, T> DefaultIfEmpty<TFactory>(TFactory factory)
            where TFactory : struct, IFunctor<T>;
        
        OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>, T> DefaultIfEmpty(Func<T> factory);
    }
}