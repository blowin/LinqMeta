using System;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class AggregateOperator
    {
        public static TRes AggregateMeta<TCollect, TFolder, T, TRes>(this TCollect collect, TRes init, TFolder folder)
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
        
        public static T AggregateMeta<TCollect, TFolder, T>(this TCollect collect, TFolder folder)
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
        public static TRes AggregateMeta<TCollect, T, TRes>(this TCollect collect, TRes init, Func<TRes, T, TRes> folder)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return collect.AggregateMeta<TCollect, FuncFunctor<TRes, T, TRes>, T, TRes>(init,
                new FuncFunctor<TRes, T, TRes>(folder));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateMeta<TCollect, T>(this TCollect collect, Func<T, T, T> folder)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return collect.AggregateMeta<TCollect, FuncFunctor<T, T, T>, T>(new FuncFunctor<T, T, T>(folder));
        }
    }
}