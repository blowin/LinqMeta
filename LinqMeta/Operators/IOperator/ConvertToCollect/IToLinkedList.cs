using System.Collections.Generic;

namespace LinqMeta.Operators.IOperator.ConvertToCollect
{
    public interface IToLinkedList<T>
    {
        LinkedList<T> ToLinkedList();
    }
}