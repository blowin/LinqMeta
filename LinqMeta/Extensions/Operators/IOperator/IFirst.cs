using LinqMeta.Core;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface IFirst<T>
    {
        Option<T> First();
    }
}