using LinqMeta.Core;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface ILast<T>
    {
        Option<T> LastMeta();
    }
}