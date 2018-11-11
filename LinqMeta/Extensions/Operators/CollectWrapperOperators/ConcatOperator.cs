using System.Runtime.CompilerServices;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class ConcatOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ConcatOperator<TCollect, TOtherCollect, T> ConcatMeta<TCollect, TOtherCollect, T>(
            this TCollect collect, TOtherCollect collect2) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TOtherCollect : struct, ICollectionWrapper<T>
        {
            return new ConcatOperator<TCollect, TOtherCollect, T>(collect, collect2);
        }
    }
}