using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators.CollectWrapperOperators
{
    public static class WhereOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WhereOperator<TCollect, TFilter, T> WhereMeta<TCollect, TFilter, T>(
            this TCollect collect, TFilter filter) 
            where TCollect : struct, ICollectionWrapper<T> 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new WhereOperator<TCollect, TFilter, T>(collect, filter);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WhereOperator<TCollect, FuncFunctor<T, bool>, T> WhereMeta<TCollect, T>(
            this TCollect collect, Func<T, bool> filter) 
            where TCollect : struct, ICollectionWrapper<T> 
        {
            return new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(collect, new FuncFunctor<T, bool>(filter));
        }
    }
}