using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToQueue<T>
    {
        Queue<T> ToQueue(uint? capacity);
    }
}