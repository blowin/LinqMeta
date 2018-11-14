using System.Runtime.CompilerServices;
using LinqMeta.Operators.Number;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class SumOperator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SumMeta<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            var sum = default(T);
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    checked
                    {
                        sum = NumberOperators<T>.Sum(sum, collect.Value);
                    }
                }
            }
            else
            {
                var size = collect.Size;
                for (var i = 0u; i < size; ++i)
                {
                    checked
                    {
                        sum = NumberOperators<T>.Sum(sum, collect[i]);
                    }   
                }   
            }

            return sum;
        }
    }
}