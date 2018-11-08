using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class SelectIndexingOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectIndexingOperator<TCollect, TSelector, TOld, TNew> SelectIndexMeta<TCollect, TSelector, TOld, TNew>(
            this TCollect collect, TSelector selector) 
            where TCollect : struct, ICollectionWrapper<TOld> 
            where TSelector : struct, IFunctor<ZipPair<TOld>, TNew>
        {
            return new SelectIndexingOperator<TCollect, TSelector, TOld, TNew>(collect, selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<TOld>, TNew>, TOld, TNew> SelectIndexMeta<TCollect, TOld, TNew>(
            this TCollect collect, Func<ZipPair<TOld>, TNew> selector) 
            where TCollect : struct, ICollectionWrapper<TOld> 
        {
            return new SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<TOld>, TNew>, TOld, TNew>(collect, new FuncFunctor<ZipPair<TOld>, TNew>(selector));
        }
    }
}