using System.Runtime.CompilerServices;
using LinqMeta.Functors.Math;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class MinOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T MinMeta<TCollect, TComparer, T>(this TCollect collect, TComparer firstLess)
            where TCollect : struct, ICollectionWrapper<T>
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return collect.FindElementInAllCollection<TCollect, TComparer, T>(firstLess);
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return MinMeta<TCollect, LessThanOperator<T>, T>(collect, default(LessThanOperator<T>));
        }
    }
}