using LinqMetaCore;

namespace LinqMeta.Operators.IOperator
{
    public interface IElementAt<T>
    {
        Option<T> ElementAt(uint index);
    }
}