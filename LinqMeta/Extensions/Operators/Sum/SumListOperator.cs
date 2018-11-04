using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Extensions.Operators.Sum
{
    public static class SumListOperators
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SumMeta<T>(this List<T> collect) 
            where T : struct
        {
            return new ListWrapper<T>(collect).SumMeta<ListWrapper<T>, T>();
        }
    }
}