using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IWhereIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndexMeta<TFilter>(TFilter filter)
            where TFilter : struct, IFunctor<ZipPair<T>, bool>;
        
        CollectWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndexMeta(Func<ZipPair<T>, bool> filter);
    }
}