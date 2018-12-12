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
    public interface IJoin<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer,TKey, TRes>
            (TCollect2 collect2, TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TComparer : IEqualityComparer<TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T2>, TRes> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TCollect2 : ICollectionWrapper<T2>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TKey, TRes>
            (TCollect2 collect2, TSelector selector, TSelector2 selector2, TSelectorRes selectorRes)
            where TSelectorRes : struct, IFunctor<Pair<T, T2>, TRes> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TCollect2 : ICollectionWrapper<T2>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> 
            JoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TComparer,TKey, TRes>
            (TCollect2 collect2, TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TComparer : IEqualityComparer<TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T>, TRes> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TCollect2 : ICollectionWrapper<T>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            JoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TKey, TRes>
            (TCollect2 collect2, TSelector selector, TSelector2 selector2, TSelectorRes selectorRes)
            where TSelectorRes : struct, IFunctor<Pair<T, T>, TRes> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TCollect2 : ICollectionWrapper<T>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, IEqualityComparer<TKey> comparer) 
            where TCollect2 : ICollectionWrapper<T2>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TComparer, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TComparer, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T2>
            where TComparer : IEqualityComparer<TKey>;
        
        OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TKey, TRes>
            (TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes)
            where TCollect2 : ICollectionWrapper<T2>;
        
        OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> 
            JoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, IEqualityComparer<TKey> comparer);
        
        OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TCompare, TKey, TRes>, TRes> 
            JoinBox<T2, TCompare, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, TCompare comparer)
                where TCompare : IEqualityComparer<TKey>;
        
        OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            JoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes);
    }
}