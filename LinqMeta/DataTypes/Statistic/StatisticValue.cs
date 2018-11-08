using System;

namespace LinqMeta.DataTypes.Statistic
{
    [Flags]
    public enum StatisticValue : byte
    {
        Sum = 1, 
        Minus = 1 << 2, 
        Product = 1 << 3,
        
        All = Sum | Minus | Product,
        
        SumMinus = Sum | Minus,
        SumProduct = Sum | Product,
        
        MinusProduct = Minus | Product
    }
}