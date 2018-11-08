using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface ISelectIndex<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector)
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>;

        OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector);
    }
}