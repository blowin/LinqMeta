using LinqMetaCore;

namespace LinqMeta.Operators.IOperator.ElementAt
{
    public interface IElementAt<T>
    {
        Option<T> ElementAt(uint index);
    }
}