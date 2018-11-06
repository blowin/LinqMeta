using System.Collections.Generic;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IToList<T>
    {
        List<T> ToMetaList(uint? capacity);
    }
}