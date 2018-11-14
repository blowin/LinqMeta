using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Converters
{
    public static class ToLinkedListOperators
    {
        public static LinkedList<T> ToLinkedListMeta<TCollect, T>(ref TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new LinkedList<T>();
                while (collect.HasNext)
                    buff.AddLast(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var buff = new LinkedList<T>();
                    for (var i = 0u; i < size; ++i)
                        buff.AddLast(collect[i]);

                    return buff;
                }
            }
            
            return new LinkedList<T>();
        }
    }
}