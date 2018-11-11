using LinqMeta.Operators.IOperator;
using LinqMeta.Operators.IOperator.Take;
using LinqMetaCore.Intefaces;

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
        
        ICast<TCollect, T>,
        IUnsafeCast<TCollect, T>,
        ITypeOf<TCollect, T>,
        ISelect<TCollect, T>,
        ISelectIndex<TCollect, T>,
        IWhere<TCollect, T>,
        IWhereIndex<TCollect, T>,
        ITake<TCollect, T>,
        ISkip<TCollect, T>,
        ITakeWhile<TCollect, T>,
        ITakeWhileIndex<TCollect, T>,
        IZip<TCollect, T>,
        IConcat<TCollect, T>,

        IToArray<T>,
        IToList<T> 
        
        where TCollect : struct, ICollectionWrapper<T>
    {
    }
}