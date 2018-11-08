using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface ITakeWhileIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<ZipPair<T>, bool>;

        OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter);
    }
}