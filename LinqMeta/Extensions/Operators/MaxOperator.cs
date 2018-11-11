using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Functors.Math;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class MaxOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, TComparer, T>(this TCollect collect, TComparer firstGreat)
            where TCollect : struct, ICollectionWrapper<T>
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.FindElementInAllCollection<TCollect, TComparer, T>(firstGreat);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, T>(this TCollect collect, Func<T, T, bool> firstGreat)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return collect.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(firstGreat));
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return collect.MaxMeta<TCollect, GreaterThan<T>, T>(default(GreaterThan<T>));
        }
    }
}