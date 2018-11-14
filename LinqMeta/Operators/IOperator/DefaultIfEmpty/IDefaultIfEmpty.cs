using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.DefaultIfEmpty
{
    public interface IDefaultIfEmpty<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty();
        
        OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty(T defaultVal);
    }
}