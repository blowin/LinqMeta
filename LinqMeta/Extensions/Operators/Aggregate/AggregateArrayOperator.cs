using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;

namespace LinqMeta.Extensions.Operators.Aggregate
{
    public static class AggregateArrayOperator
    {        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<TFolder, T, TRes>(this T[] collect, TRes init, TFolder folder)
            where TFolder : struct , IFunctor<TRes, T, TRes>
        {
            return new ArrayWrapper<T>(collect).AggregateMeta<ArrayWrapper<T>, TFolder, T, TRes>(init, folder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<T, TRes>(this T[] collect, TRes init, Func<TRes, T, TRes> folder)
        {
            return collect.AggregateMeta(init, new FuncFunctor<TRes, T, TRes>(folder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<T>(this T[] collect, Func<T, T, T> folder)
        {
            return new ArrayWrapper<T>(collect).AggregateMeta<ArrayWrapper<T>, FuncFunctor<T, T, T>, T>(new FuncFunctor<T, T, T>(folder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<TFolder, T>(this T[] collect, TFolder folder)
            where TFolder : struct , IFunctor<T, T, T>
        {
            return new ArrayWrapper<T>(collect).AggregateMeta<ArrayWrapper<T>, TFolder, T>(folder);
        }
    }
}