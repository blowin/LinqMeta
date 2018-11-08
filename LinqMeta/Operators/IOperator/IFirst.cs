using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface IFirst<T>
    {
        Option<T> First();
    }
}