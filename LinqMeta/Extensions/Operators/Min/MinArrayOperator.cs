using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators.Min
{
    public static class MinArrayOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<TComparer, T>(this ArrayWrapper<T> collect, TComparer comparer)
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.MinMeta<ArrayWrapper<T>, TComparer, T>(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<T>(this ArrayWrapper<T> collect, Func<T, T, bool> comparer)
        {
            return collect.MinMeta(new FuncFunctor<T, T, bool>(comparer));
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<T>(this T[] collect)
            where T : struct 
        {
            return new ArrayWrapper<T>(collect).MinMeta<ArrayWrapper<T>, LessThanOperator<T>, T>(default(LessThanOperator<T>));
        }        
    }
}