using LinqMeta.DataTypes.Buffers;
using LinqMetaCore.Intefaces;
using LinqMetaCore.Utils;

namespace LinqMeta.Extensions.Converters
{
    public static class ToArrayOperators
    {
        // TODO CREATE ADAPTER FOR CREATE COLLECT
        public static T[] ToMetaArray<TCollect, T>(ref TCollect collect, uint? capacity = null)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                var buff = ArrayBuffer<T>.CreateBuff(capacity.GetValueOrDefault(16));
                while (collect.HasNext)
                    buff.Add(collect.Value);

                var arr = buff.ToArray();
                return arr;
            }
            else
            {
                var size = collect.Size;
                if (size > 0)
                {
                    var resArr = new T[size];
                    for (var i = 0u; i < size; ++i)
                        resArr[i] = collect[i];

                    return resArr;
                }
            }
            
            return CollectUtil.ArrayUtil<T>.Empty;
        }
    }
}