using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToList<T>
    {
        List<T> ToList(uint? capacity);
    }
}