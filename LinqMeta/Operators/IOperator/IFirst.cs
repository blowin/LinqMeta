using LinqMeta.Core;

namespace LinqMeta.Operators.IOperator
{
    public interface IFirst<T>
    {
        Option<T> First();
    }
}