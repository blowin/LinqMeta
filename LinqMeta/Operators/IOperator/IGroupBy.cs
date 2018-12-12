using System;
using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMeta.DataTypes.Grouping;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IGroupBy<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<GroupByOperator<TCollect, T, TSelector, TSelectorRes, TComparer, TKey, TRes>, Pair<TKey, GroupingArray<TRes>>>
            GroupBy<TSelector, TSelectorRes, TComparer, TKey, TRes>
            (TSelector selector, TSelectorRes selectorRes, TComparer comparer)
            where TComparer : IEqualityComparer<TKey>
            where TSelectorRes : struct, IFunctor<T, TRes>
            where TSelector : struct, IFunctor<T, TKey>;
        
        OperatorWrapper<GroupByOperator<TCollect, T, TSelector, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, Pair<TKey, GroupingArray<TRes>>>
            GroupBy<TSelector, TSelectorRes, TKey, TRes>
            (TSelector selector, TSelectorRes selectorRes)
            where TSelectorRes : struct, IFunctor<T, TRes>
            where TSelector : struct, IFunctor<T, TKey>;
        
        OperatorWrapper<GroupByOperator<TCollect, T, TSelector, IdentityOperator<T>, TComparer, TKey, T>, Pair<TKey, GroupingArray<T>>>
            GroupBy<TSelector, TKey, TComparer>
            (TSelector selector, TComparer comparer)
            where TSelector : struct, IFunctor<T, TKey>
            where TComparer : IEqualityComparer<TKey>;
        
        OperatorWrapper<GroupByOperator<TCollect, T, TSelector, IdentityOperator<T>, CompareHashSetHelper<TKey>, TKey, T>, Pair<TKey, GroupingArray<T>>>
            GroupBy<TSelector, TKey>
            (TSelector keySelector)
            where TSelector : struct, IFunctor<T, TKey>;
        
        //Func
        OperatorWrapper<GroupByOperator<TCollect, T, FuncFunctor<T, TKey>, FuncFunctor<T, TRes>, TComparer, TKey, TRes>, Pair<TKey, GroupingArray<TRes>>>
            GroupBy<TComparer, TKey, TRes>
            (Func<T, TKey> selector, Func<T, TRes> selectorRes, TComparer comparer)
            where TComparer : IEqualityComparer<TKey>;
        
        OperatorWrapper<GroupByOperator<TCollect, T, FuncFunctor<T, TKey>, FuncFunctor<T, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, Pair<TKey, GroupingArray<TRes>>>
            GroupBy<TKey, TRes>
            (Func<T, TKey> selector, Func<T, TRes> selectorRes);
        
        OperatorWrapper<GroupByOperator<TCollect, T, FuncFunctor<T, TKey>, IdentityOperator<T>, TComparer, TKey, T>, Pair<TKey, GroupingArray<T>>>
            GroupBy<TKey, TComparer>
            (Func<T, TKey> selector, TComparer comparer)
            where TComparer : IEqualityComparer<TKey>;
        
        OperatorWrapper<GroupByOperator<TCollect, T, FuncFunctor<T, TKey>, IdentityOperator<T>, CompareHashSetHelper<TKey>, TKey, T>, Pair<TKey, GroupingArray<T>>>
            GroupBy<TKey>
            (Func<T, TKey> selector);
    }
}