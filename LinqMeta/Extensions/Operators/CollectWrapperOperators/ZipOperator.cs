using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class ZipOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ZipOperator<TCollect, T, TOtherCollect, T2> ZipMeta<TCollect, T, TOtherCollect, T2>(
            this TCollect collect, TOtherCollect collect2) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new ZipOperator<TCollect, T, TOtherCollect, T2>(collect, collect2);
        }
    }
}