using System.Runtime.InteropServices;
using LinqMeta.Operators;
using LinqMeta.Operators.Number;
using LinqMetaCore;

namespace LinqMeta.DataTypes.Statistic
{
    [StructLayout(LayoutKind.Auto)]
    public struct StatisticInfo<T>
    {
        public Option<T> Sum { set; get; }
        public Option<T> Minus { set; get; }
        public Option<T> Product { set; get; }
        public uint Count { set; get; }
        
        public double? Average
        {
            get { return !Sum.HasValue ? (double?) null : NumberOperators<T>.DivDouble(Sum.GetValueOrDefault(), (double) Count); }
        }
    }
}