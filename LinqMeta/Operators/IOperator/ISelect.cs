using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Operators.IOperator
{
    public interface ISelect<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> Select<TSelector, TNew>(TSelector selector)
            where TSelector : struct, IFunctor<T, TNew>;

        CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> Select<TNew>(Func<T, TNew> selector);
    }
}