using System;
using LinqMeta.Operators.Number;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Extensions.Operators
{
    public static class AverageOperator
    {
        public static double Average<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            long number = 0;
            T sum = default(T);
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    checked
                    {
                        ++number;
                        sum = NumberOperators<T>.Sum(sum, collect.Value);
                    }
                }
            }
            else
            {
                for (var size = collect.Size; number < size;)
                {
                    checked
                    {
                        sum = NumberOperators<T>.Sum(sum, collect[(uint) number]);
                        ++number;
                    }
                }
            }

            
            return number > 0 ? NumberOperators<T>.DivDouble(sum, number) : 0;
        }
        
        public static Decimal AverageDec<TCollect, T>(this TCollect collect)
            where TCollect : struct, ICollectionWrapper<T>
        {
            long number = 0;
            T sum = default(T);
            if (collect.HasIndexOverhead)
            {
                while (collect.HasNext)
                {
                    checked
                    {
                        ++number;
                        sum = NumberOperators<T>.Sum(sum, collect.Value);
                    }
                }
            }
            else
            {
                for (var size = collect.Size; number < size;)
                {
                    checked
                    {
                        sum = NumberOperators<T>.Sum(sum, collect[(uint) number]);
                        ++number;
                    }
                }
            }

            
            return number > 0 ? NumberOperators<T>.DivDecimal(sum, number) : 0;
        }
    }
}