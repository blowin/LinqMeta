using System;
using System.Runtime.CompilerServices;
using LinqMeta.Functors;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class AggregateOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<TCollect, TFolder, T, TRes>(ref TCollect collect, TRes init, ref TFolder folder)
            where TCollect : struct, ICollectionWrapper<T>
            where TFolder : struct , IFunctor<TRes, T, TRes>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                    init = folder.Invoke(init, collect.Value);
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                    init = folder.Invoke(init, collect[i]);
            }

            return init;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<TCollect, TFolder, T>(ref TCollect collect, ref TFolder folder)
            where TCollect : struct, ICollectionWrapper<T>
            where TFolder : struct , IFunctor<T, T, T>
        {
            if (collect.HasIndexOverhead)
            {
                if (collect.HasNext)
                {
                    var init = collect.Value;
                    while (collect.HasNext)
                        init = folder.Invoke(init, collect.Value);

                    return init;
                }
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var init = collect[0];
                    for (var i = 1u; i < size; ++i)
                        init = folder.Invoke(init, collect[i]);

                    return init;
                }
            }

            return default(T);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TRes AggregateMeta<TCollect, T, TRes>(ref TCollect collect, TRes init, Func<TRes, T, TRes> folder)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var functor = new FuncFunctor<TRes, T, TRes>(folder);
            return AggregateMeta<TCollect, FuncFunctor<TRes, T, TRes>, T, TRes>(ref collect, init, ref functor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<TCollect, T>(ref TCollect collect, Func<T, T, T> folder)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var functor = new FuncFunctor<T, T, T>(folder);
            return AggregateMeta<TCollect, FuncFunctor<T, T, T>, T>(ref collect, ref functor);
        }
    }
}