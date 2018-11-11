using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class SkipOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SkipOperator<TCollect, T> SkipMeta<TCollect, T>(
            this TCollect collect, uint count) 
            where TCollect : struct, ICollectionWrapper<T>
        {
            return new SkipOperator<TCollect, T>(collect, count);
        }
    }
}