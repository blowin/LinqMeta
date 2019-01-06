using LinqMeta.Operators.IOperator.Orders;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IOrderOperators<TCollect, T> : 
        IOperators<TCollect, T>,
        
        IOrderBy<TCollect, T>,
        IOrderByDescending<TCollect, T>,
        
        IThenBy<TCollect, T>
        
        where TCollect : struct, ICollectionWrapper<T>
    {
        
    }
}