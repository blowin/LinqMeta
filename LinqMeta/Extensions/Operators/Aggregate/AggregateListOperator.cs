using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.Aggregate
{
    public static class AggregateListOperator
    {        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<TFolder, T, TRes>(this List<T> collect, TRes init, TFolder folder)
            where TFolder : struct , IFunctor<TRes, T, TRes>
        {
            return new ListWrapper<T>(collect).AggregateMeta<ListWrapper<T>, TFolder, T, TRes>(init, folder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<T, TRes>(this List<T> collect, TRes init, Func<TRes, T, TRes> folder)
        {
            return collect.AggregateMeta(init, new FuncFunctor<TRes, T, TRes>(folder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<T>(this List<T> collect, Func<T, T, T> folder)
        {
            return new ListWrapper<T>(collect).AggregateMeta<ListWrapper<T>, FuncFunctor<T, T, T>, T>(new FuncFunctor<T, T, T>(folder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<TFolder, T>(this List<T> collect, TFolder folder)
            where TFolder : struct , IFunctor<T, T, T>
        {
            return new ListWrapper<T>(collect).AggregateMeta<ListWrapper<T>, TFolder, T>(folder);
        }
    }
}