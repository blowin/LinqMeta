using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Skip
{
    public interface ISkipWhileIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<SkipWhileIndexingOperator<TCollect, TFilter, T>, T> SkipWhileIndex<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<ZipPair<T>, bool>;

        OperatorWrapper<SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> SkipWhileIndex(Func<ZipPair<T>, bool> filter);
    }
}