using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Core;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators
{
    public static class MaxMinMetaOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MinMaxPair<T>? MaxMinMeta<TCollect, TMaxComparer, TMinComparer, T>(this TCollect collect, TMaxComparer maxComparer, TMinComparer minComparer)
            where TCollect : struct, ICollectionWrapper<T>
            where TMaxComparer : struct, IFunctor<T, T, bool>
            where TMinComparer : struct, IFunctor<T, T, bool>
        {
            if (collect.HasIndexOverhead)
            {
                if (collect.HasNext)
                {
                    var item = collect.Value;
                    var maxMin = new MinMaxPair<T>(item, item);
                    while (collect.HasNext)
                    {
                        item = collect.Value;
                        if (maxComparer.Invoke(item, maxMin.Max))
                            maxMin.Max = item;

                        if (minComparer.Invoke(item, maxMin.Min))
                            maxMin.Min = item;
                        
                    }

                    return maxMin;
                }
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var item = collect[0];
                    var maxMin = new MinMaxPair<T>(item, item);
                    for (var i = 1u; i < size; ++i)
                    {
                        item = collect[i];
                        if (maxComparer.Invoke(item, maxMin.Max))
                            maxMin.Max = item;

                        if (minComparer.Invoke(item, maxMin.Min))
                            maxMin.Min = item;
                    }

                    return maxMin;
                }
            }

            return null;
        }
        
        // For number collection
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MinMaxPair<T>? MaxMinMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return collect.MaxMinMeta<TCollect, GreaterThan<T>, LessThanOperator<T>, T>(default(GreaterThan<T>),
                default(LessThanOperator<T>));
        }
    }
}