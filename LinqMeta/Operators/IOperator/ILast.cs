using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface ILast<T>
    {
        Option<T> Last();
    }
}