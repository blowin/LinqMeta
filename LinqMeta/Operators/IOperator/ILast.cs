using LinqMeta.Core;

namespace LinqMeta.Operators.IOperator
{
    public interface ILast<T>
    {
        Option<T> Last();
    }
}