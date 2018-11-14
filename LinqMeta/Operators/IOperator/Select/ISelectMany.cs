using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Select
{
    public interface ISelectMany<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>, T2> SelectMany<TOtherCollect,
            TSelector, T2>(TSelector @select)
            where TOtherCollect : struct, ICollectionWrapper<T2>
            where TSelector : struct, IFunctor<T, TOtherCollect>;
        
        OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>, T2> 
            SelectMany<TOtherCollect, T2>(Func<T, TOtherCollect> @select)
            where TOtherCollect : struct, ICollectionWrapper<T2>;
        
        OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>, T2> SelectManyBox<TSelector, T2>(TSelector @select)
            where TSelector : struct, IFunctor<T, ICollectionWrapper<T2>>;

        OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>, T2>
            SelectManyBox<T2>(Func<T, ICollectionWrapper<T2>> @select);
    }
}