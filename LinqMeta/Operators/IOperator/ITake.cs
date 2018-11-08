using LinqMeta.CollectionWrapper;

namespace LinqMeta.Operators.IOperator
{
    public interface ITake<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<TakeOperator<TCollect, T>, T> Take(uint count);
    }
}