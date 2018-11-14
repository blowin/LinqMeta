using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class AllOperators
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllMeta<TCollect, TFilter, T>(this TCollect collect, TFilter filter)
            where TCollect : struct, ICollectionWrapper<T>
            where TFilter : struct, IFunctor<T, bool>
        {
            if (collect.HasIndexOverhead)
            {
                if (collect.HasNext && filter.Invoke(collect.Value))
                {
                    while (collect.HasNext)
                    {
                        if (filter.Invoke(collect.Value) == false)
                            return false;
                    }

                    return true;
                }
            }
            else
            {
                var size = collect.Size;
                if (size > 0 && filter.Invoke(collect[0]))
                {
                    for (var i = 1u; i < size; ++i)
                    {
                        if (filter.Invoke(collect[i]) == false)
                            return false;
                    }

                    return true;
                }
            }
            
            return false;
        }
    }
}