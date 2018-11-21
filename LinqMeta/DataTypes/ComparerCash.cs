using System.Collections.Generic;

namespace LinqMeta.DataTypes
{
    public static class ComparerCash<T>
    {
        public static IEqualityComparer<T> Value = EqualityComparer<T>.Default;
    }
}