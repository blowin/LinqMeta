using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Converters
{
    public static class ToQueueOperators
    {
        public static Queue<T> ToQueueMeta<TCollect, T>(ref TCollect collect, uint? capacity = null)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new Queue<T>((int) capacity.GetValueOrDefault());
                while (collect.HasNext)
                    buff.Enqueue(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var buff = new Queue<T>(size);
                    for (var i = 0u; i < size; ++i)
                        buff.Enqueue(collect[i]);

                    return buff;
                }
            }
            
            return new Queue<T>();
        }
    }
}