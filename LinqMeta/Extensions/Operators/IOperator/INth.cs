using LinqMeta.Core;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface INth<T>
    {
        Option<T> NthMeta(uint index);
    }
}