using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions.Operators.IOperator;

namespace LinqMeta.Extensions.Operators
{
    public interface IOperators<TCollect, T> : 
        IAggregate<T>,
        IMax<T>,
        IMin<T>,
        ISum<T>,
        IFirst<T>,
        ILast<T>,
        INth<T>,
        
        ISelect<TCollect, T>,
        ISelectIndex<TCollect, T>,
        IWhere<TCollect, T>,
        IWhereIndex<TCollect, T>,
        ITake<TCollect, T>,

        IToArray<T>,
        IToList<T> 
        
        where TCollect : struct, ICollectionWrapper<T>
    {
    }
}