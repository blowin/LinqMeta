using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IWhere<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<WhereOperator<TCollect, TFilter, T>, T> WhereMeta<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<T, bool>;
        
        CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> WhereMeta(Func<T, bool> filter);
    }
}