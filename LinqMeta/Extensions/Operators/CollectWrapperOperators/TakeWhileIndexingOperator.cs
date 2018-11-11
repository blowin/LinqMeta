using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class TakeWhileIndexingOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TakeWhileIndexingOperator<TCollect, TFilter, T> TakeWhileIndexMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T>
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new TakeWhileIndexingOperator<TCollect, TFilter, T>(collect, filter);
        }
    }
}