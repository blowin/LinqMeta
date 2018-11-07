using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Operators.IOperator
{
    public interface ITakeWhile<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<TakeWhile<TCollect, TFilter, T>, T> TakeWhile<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<T, bool>;

        CollectWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T> TakeWhile(Func<T, bool> filter);
    }
}