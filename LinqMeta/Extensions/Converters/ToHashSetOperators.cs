using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Converters
{
    public static class ToHashSetOperators
    {
        public static HashSet<T> ToMetaHashSetMeta<TCollect, T>(ref TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new HashSet<T>();
                while (collect.HasNext)
                    buff.Add(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var res = new HashSet<T>();
                    for (var i = 0u; i < size; ++i)
                        res.Add(collect[i]);

                    return res;
                }
            }
            
            return new HashSet<T>();
        }
        
        public static HashSet<T> ToMetaHashSetMeta<TCollect, T>(ref TCollect collect, IEqualityComparer<T> comparer)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new HashSet<T>(comparer);
                while (collect.HasNext)
                    buff.Add(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var res = new HashSet<T>(comparer);
                    for (var i = 0u; i < size; ++i)
                        res.Add(collect[i]);

                    return res;
                }
            }
            
            return new HashSet<T>(comparer);
        }
    }
}