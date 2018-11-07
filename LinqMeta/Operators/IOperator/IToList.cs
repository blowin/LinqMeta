using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator
{
    public interface IToList<T>
    {
        List<T> ToList(uint? capacity);
    }
}