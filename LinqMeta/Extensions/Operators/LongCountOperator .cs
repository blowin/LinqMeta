using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class LongCountOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long LongCountMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            if (collect.HasIndexOverhead)
            {
                long count = 0;
                while (collect.HasNext)
                    ++count;

                return count;
            }
            else
            {
                return collect.Size;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long LongCountMeta<TCollect, T, TPredicat>(this TCollect collect, TPredicat predicat)
            where TCollect : struct, ICollectionWrapper<T>
            where TPredicat : struct, IFunctor<T, bool>
        {
            long count = 0;
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if(predicat.Invoke(collect.Value))
                        ++count;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if(predicat.Invoke(collect[i]))
                        ++count;
                }
            }

            return count;
        }
    }
}