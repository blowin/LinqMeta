using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;
using LinqMeta.Functors.Math;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class MaxOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, TComparer, T>(ref TCollect collect, ref TComparer firstGreat)
            where TCollect : struct, ICollectionWrapper<T>
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return FindOperator.FindElementInAllCollection<TCollect, TComparer, T>(ref collect, ref firstGreat);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, T>(ref TCollect collect, Func<T, T, bool> firstGreat)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var func = new FuncFunctor<T, T, bool>(firstGreat);
            return MaxOperator.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(ref collect, ref func);
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxMeta<TCollect, T>(ref TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var greatThan = default(GreaterThan<T>);
            return MaxMeta<TCollect, GreaterThan<T>, T>(ref collect, ref greatThan);
        }
    }
}