using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;

namespace LinqMeta.Operators.IOperator
{
    public interface ISelectIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector)
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>;

        CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector);
    }
}