using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;

namespace LinqMeta.Extensions.Operators.Sum
{
    public static class SumArrayOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SumMeta<T>(this T[] collect) 
            where T : struct
        {
            return new ArrayWrapper<T>(collect).SumMeta<ArrayWrapper<T>, T>();
        }
    }
}