using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.Numbers;

namespace LinqMeta.Extensions.Operators.Sum
{
    public static class SumOperator
    {
        public static T SumMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
            where T : struct 
        {
            var size = collect.Size;
            var sum = default(T);
            for (var i = 0u; i < size; ++i)
            {
                checked
                {
                    sum = default(SumOperator<T>).Invoke(sum, collect[i]);
                }   
            }

            return sum;
        }
    }
}