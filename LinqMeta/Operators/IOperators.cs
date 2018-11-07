using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.IOperator;

namespace LinqMeta.Operators
{
    public interface IOperators<TCollect, T> : 
        IAggregate<T>,
        IMax<T>,
        IMin<T>,
        ISum<T>,
        IFirst<T>,
        ILast<T>,
        IElementAt<T>,
        IEmpty,
        IStatistic<T>,
        IMaxMin<T>,
        IAny<T>,
        IAll<T>,

        ISelect<TCollect, T>,
        ISelectIndex<TCollect, T>,
        IWhere<TCollect, T>,
        IWhereIndex<TCollect, T>,
        ITake<TCollect, T>,
        ITakeWhile<TCollect, T>,
        ITakeWhileIndex<TCollect, T>,

        IToArray<T>,
        IToList<T> 
        
        where TCollect : struct, ICollectionWrapper<T>
    {
    }
}