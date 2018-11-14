using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToHashSet<T>
    {
        HashSet<T> ToHashSet();
        
        HashSet<T> ToHashSet(IEqualityComparer<T> comparer);
    }
}