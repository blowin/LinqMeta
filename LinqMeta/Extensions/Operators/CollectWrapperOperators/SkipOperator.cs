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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SkipWhileOperator<TCollect, TFilter, T> SkipWhileMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new SkipWhileOperator<TCollect, TFilter, T>(collect, filter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SkipWhileIndexingOperator<TCollect, TFilter, T> SkipWhileIndexMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new SkipWhileIndexingOperator<TCollect, TFilter, T>(collect, filter);
        }
    }
}