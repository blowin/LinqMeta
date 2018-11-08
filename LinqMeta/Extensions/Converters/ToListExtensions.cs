using System.Collections.Generic;
using LinqMeta.CollectionWrapper;
using LinqMetaCore;

namespace LinqMeta.Extensions.Converters
{
    public static class ToListExtensions
    {
        public static List<T> ToMetaList<TCollect, T>(this TCollect collect, uint? capacity = null)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new List<T>((int)capacity.GetValueOrDefault(16));
                while (collect.HasNext)
                    buff.Add(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var res = new List<T>(size);
                    for (var i = 0u; i < size; ++i)
                        res.Add(collect[i]);
                
                    return res;
                }
            }
            
            return CollectUtil.ListUtil<T>.Empty;
        }
    }
}