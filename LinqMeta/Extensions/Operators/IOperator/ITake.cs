using LinqMeta.CollectionWrapper;
using LinqMeta.Operators;

namespace LinqMeta.Extensions.Operators.IOperator
{
    public interface ITake<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectWrapper<TakeOperator<TCollect, T>, T> TakeMeta(uint count);
    }
}