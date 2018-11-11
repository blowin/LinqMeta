using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class TakeOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TakeOperator<TCollect, T> TakeMeta<TCollect, T>(
            this TCollect collect, uint count) 
            where TCollect : struct, ICollectionWrapper<T>
        {
            return new TakeOperator<TCollect, T>(collect, count);
        }
    }
}