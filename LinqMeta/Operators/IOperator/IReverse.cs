using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IReverse<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<ReverseOperator<TCollect, T>, T> Reverse();
    }
}