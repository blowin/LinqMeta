using System.Runtime.CompilerServices;
using LinqMeta.Functors.Math;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class MinOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T MinMeta<TCollect, TComparer, T>(ref TCollect collect, ref TComparer firstLess)
            where TCollect : struct, ICollectionWrapper<T>
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return FindOperator.FindElementInAllCollection<TCollect, TComparer, T>(ref collect, ref firstLess);
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinMeta<TCollect, T>(ref TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var lessThan = default(LessThanOperator<T>);
            return MinMeta<TCollect, LessThanOperator<T>, T>(ref collect, ref lessThan);
        }
    }
}