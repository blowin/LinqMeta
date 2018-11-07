using LinqMeta.CollectionWrapper;

namespace LinqMeta.Operators.IOperator
{
    public interface ITake<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<TakeOperator<TCollect, T>, T> Take(uint count);
    }
}