using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;

namespace LinqMeta.Operators.IOperator
{
    public interface ITakeWhileIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<ZipPair<T>, bool>;

        CollectWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter);
    }
}