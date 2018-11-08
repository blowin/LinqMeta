using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface IWhereIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndex<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<ZipPair<T>, bool>;
        
        OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndex(Func<ZipPair<T>, bool> filter);
    }
}