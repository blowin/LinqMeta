using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class TakeWhileOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TakeWhile<TCollect, TFilter, T> TakeWhileMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T>
            where TFilter : struct, IFunctor<T, bool>
        {
            return new TakeWhile<TCollect, TFilter, T>(collect, filter);
        }
    }
}