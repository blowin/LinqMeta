using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Operators.IOperator
{
    public interface IWhere<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<WhereOperator<TCollect, TFilter, T>, T> Where<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<T, bool>;
        
        CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> Where(Func<T, bool> filter);
    }
}