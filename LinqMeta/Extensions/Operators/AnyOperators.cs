using System.Runtime.CompilerServices;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class AnyOperators
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyMeta<TCollect, T>(ref TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            return FirstLastOperators.FirstMeta<TCollect, T>(ref collect).HasValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyMeta<TCollect, TFilter, T>(ref TCollect collect, ref TFilter filter)
            where TCollect : struct, ICollectionWrapper<T>
            where TFilter : struct, IFunctor<T, bool>
        {
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    if (filter.Invoke(collect.Value))
                        return true;
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    if (filter.Invoke(collect[i]))
                        return true;
                }
            }
            
            return false;
        }
    }
}