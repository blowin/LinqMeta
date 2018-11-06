using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface ISelect<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> SelectMeta<TSelector, TNew>(TSelector selector)
            where TSelector : struct, IFunctor<T, TNew>;

        CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> SelectMeta<TNew>(Func<T, TNew> selector);
    }
}