using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class SelectOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectOperator<TCollect, TSelector, TOld, TNew> SelectMeta<TCollect, TSelector, TOld, TNew>(
            this TCollect collect, TSelector selector) 
            where TCollect : struct, ICollectionWrapper<TOld> 
            where TSelector : struct, IFunctor<TOld, TNew>
        {
            return new SelectOperator<TCollect, TSelector, TOld, TNew>(collect, selector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SelectOperator<TCollect, FuncFunctor<TOld, TNew>, TOld, TNew> SelectMeta<TCollect, TOld, TNew>(
            this TCollect collect, Func<TOld, TNew> selector) 
            where TCollect : struct, ICollectionWrapper<TOld> 
        {
            return new SelectOperator<TCollect, FuncFunctor<TOld, TNew>, TOld, TNew>(collect, new FuncFunctor<TOld, TNew>(selector));
        }
    }
}