using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToStack<T>
    {
        Stack<T> ToStack(uint? capacity);
    }
}