using System.Collections.Generic;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Converters
{
    public static class ToStackOperators
    {
        public static Stack<T> ToStackMeta<TCollect, T>(ref TCollect collect, uint? capacity = null)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = new Stack<T>((int) capacity.GetValueOrDefault());
                while (collect.HasNext)
                    buff.Push(collect.Value);

                return buff;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var buff = new Stack<T>(size);
                    for (var i = 0u; i < size; ++i)
                        buff.Push(collect[i]);

                    return buff;
                }
            }
            
            return new Stack<T>();
        }
    }
}