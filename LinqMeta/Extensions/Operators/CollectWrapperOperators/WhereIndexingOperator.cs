using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;
using LinqMetaCore;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class WhereIndexingOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WhereIndexingOperator<TCollect, TFilter, T> WhereIndexMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new WhereIndexingOperator<TCollect, TFilter, T>(collect, filter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T> WhereIndexMeta<TCollect, T>(
            this TCollect collect, Func<ZipPair<T>, bool> filter) 
            where TCollect : struct, ICollectionWrapper<T> 
        {
            return new WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(collect, new FuncFunctor<ZipPair<T>, bool>(filter));
        }
    }
}