using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface ISelectIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndexMeta<TSelector, TNew>(TSelector selector)
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>;

        CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndexMeta<TNew>(Func<ZipPair<T>, TNew> selector);
    }
}