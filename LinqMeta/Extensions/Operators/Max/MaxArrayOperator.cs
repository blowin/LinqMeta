using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators.Max
{
    public static class MaxArrayOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TComparer, T>(this ArrayWrapper<T> collect, TComparer comparer)
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.MaxMeta<ArrayWrapper<T>, TComparer, T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<T>(this ArrayWrapper<T> collect, Func<T, T, bool> comparer)
        {
            return collect.MaxMeta(new FuncFunctor<T, T, bool>(comparer));
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<T>(this T[] collect)
            where T : struct 
        {
            return new ArrayWrapper<T>(collect).MaxMeta<ArrayWrapper<T>, GreaterThan<T>, T>(default(GreaterThan<T>));
        }        
    }
}