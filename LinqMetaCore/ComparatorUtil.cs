using System.Collections.Generic;

namespace LinqMetaCore
{
    public static class ComparatorUtil<T>
    {
        public static readonly Comparer<T> Default = Comparer<T>.Default;
    }
}