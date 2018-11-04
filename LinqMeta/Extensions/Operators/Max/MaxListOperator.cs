using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions.Operators.Min;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators.Max
{
    public static class MaxListOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TComparer, T>(this ListWrapper<T> collect, TComparer comparer)
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.MaxMeta<ListWrapper<T>, TComparer, T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<T>(this ListWrapper<T> collect, Func<T, T, bool> comparer)
        {
            return collect.MaxMeta(new FuncFunctor<T, T, bool>(comparer));
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<T>(this List<T> collect)
            where T : struct 
        {
            return new ListWrapper<T>(collect).MaxMeta<ListWrapper<T>, GreaterThan<T>, T>(default(GreaterThan<T>));
        }
    }
}