using System.Runtime.CompilerServices;

namespace LinqMeta.Core.Statistic
{
    public struct StatisticFlags
    {
        private byte _flags;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatisticFlags Add(StatisticValue statisticValue)
        {
            _flags |= (byte)statisticValue;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddAll()
        {
            _flags |= (byte) StatisticValue.Sum | 
                      (byte) StatisticValue.Minus | 
                      (byte) StatisticValue.Minus;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(StatisticValue val)
        {
            return (_flags & (byte) val) == (byte) val;
        }
    }
}