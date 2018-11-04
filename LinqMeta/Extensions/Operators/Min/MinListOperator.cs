using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators.Min
{
    public static class MinListOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<TComparer, T>(this ListWrapper<T> collect, TComparer comparer)
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.MinMeta<ListWrapper<T>, TComparer, T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<T>(this ListWrapper<T> collect, Func<T, T, bool> comparer)
        {
            return collect.MinMeta(new FuncFunctor<T, T, bool>(comparer));
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<T>(this List<T> collect)
            where T : struct 
        {
            return new ListWrapper<T>(collect).MinMeta<ListWrapper<T>, LessThanOperator<T>, T>(default(LessThanOperator<T>));
        }
    }
}