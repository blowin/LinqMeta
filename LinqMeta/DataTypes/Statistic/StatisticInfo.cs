using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LinqMeta.Operators.Number;
using LinqMetaCore;

namespace LinqMeta.DataTypes.Statistic
{
    [StructLayout(LayoutKind.Auto)]
    public struct StatisticInfo<T> : IComparable<StatisticInfo<T>>
    {
        public static IEqualityComparer<StatisticInfo<T>> StatisticInfoComparer
        {
            get { return new StatisticInfoEqualityComparer(); }
        }

        public Option<T> Sum { set; get; }
        public Option<T> Minus { set; get; }
        public Option<T> Product { set; get; }
        public uint Count { set; get; }
        
        public double? Average
        {
            get { return !Sum.HasValue ? (double?) null : NumberOperators<T>.DivDouble(Sum.GetValueOrDefault(), (double) Count); }
        }
        
        public bool Equals(StatisticInfo<T> other)
        {
            return Sum.Equals(other.Sum) && Minus.Equals(other.Minus) && Product.Equals(other.Product) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StatisticInfo<T> && Equals((StatisticInfo<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Sum.GetHashCode();
                hashCode = (hashCode * 397) ^ Minus.GetHashCode();
                hashCode = (hashCode * 397) ^ Product.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) Count;
                return hashCode;
            }
        }
        
        public int CompareTo(StatisticInfo<T> other)
        {
            var sumComparison = Sum.CompareTo(other.Sum);
            if (sumComparison != 0) return sumComparison;
            var minusComparison = Minus.CompareTo(other.Minus);
            if (minusComparison != 0) return minusComparison;
            var productComparison = Product.CompareTo(other.Product);
            if (productComparison != 0) return productComparison;
            return Count.CompareTo(other.Count);
        }
        
        private sealed class StatisticInfoEqualityComparer : IEqualityComparer<StatisticInfo<T>>
        {
            public bool Equals(StatisticInfo<T> x, StatisticInfo<T> y)
            {
                return x.Sum.Equals(y.Sum) && x.Minus.Equals(y.Minus) && x.Product.Equals(y.Product) && x.Count == y.Count;
            }

            public int GetHashCode(StatisticInfo<T> obj)
            {
                unchecked
                {
                    var hashCode = obj.Sum.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Minus.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.Product.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) obj.Count;
                    return hashCode;
                }
            }
        }
    }
}