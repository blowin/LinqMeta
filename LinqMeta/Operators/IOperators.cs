using LinqMeta.Operators.IOperator;
using LinqMeta.Operators.IOperator.Cast;
using LinqMeta.Operators.IOperator.Skip;
using LinqMeta.Operators.IOperator.Take;
using LinqMeta.Operators.IOperator.Select;
using LinqMeta.Operators.IOperator.ConvertToCollect;
using LinqMeta.Operators.IOperator.Count;
using LinqMeta.Operators.IOperator.DefaultIfEmpty;
using LinqMeta.Operators.IOperator.Distinct;
using LinqMeta.Operators.IOperator.ElementAt;
using LinqMeta.Operators.IOperator.First;
using LinqMeta.Operators.IOperator.Intersect;
using LinqMeta.Operators.IOperator.Last;
using LinqMeta.Operators.IOperator.MaxMin;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators
{
    public interface IOperators<TCollect, T> : 
        IAverage,
        IAggregate<T>,
        IMax<T>,
        IMin<T>,
        ISum<T>,
        IFirst<T>,
        IFirstOrDefault<T>,
        ILast<T>,
        ILastOrDefault<T>,
        IElementAt<T>,
        IElementAtOrDefault<T>,
        IEmpty,
        IStatistic<T>,
        IMaxMin<T>,
        IAny<T>,
        IAll<T>,
        IContain<T>,
        ICount<T>,
        ILongCount<T>,
        ISequenceEqual<T>,
        IForEach<T>,

        ICast<TCollect, T>,
        IUnsafeCast<TCollect, T>,
        ITypeOf<TCollect, T>,
        ISelect<TCollect, T>,
        ISelectIndex<TCollect, T>,
        ISelectMany<TCollect, T>,
        IWhere<TCollect, T>,
        IWhereIndex<TCollect, T>,
        ITake<TCollect, T>,
        ITakeWhile<TCollect, T>,
        ITakeWhileIndex<TCollect, T>,
        ISkip<TCollect, T>,
        ISkipWhile<TCollect, T>,
        ISkipWhileIndex<TCollect, T>,
        IZip<TCollect, T>,
        IConcat<TCollect, T>,
        IDefaultIfEmpty<TCollect, T>,
        ILazyDefaultIfEmpty<TCollect, T>,
        IReverse<TCollect, T>,
        IDistinct<TCollect, T>,
        IDistinctRestartable<TCollect, T>,
        IExcept<TCollect, T>,
        IIntersect<TCollect, T>,
        IIntersectRestartable<TCollect, T>,
        IUnion<TCollect, T>,

        IToArray<T>,
        IToList<T>,
        IToHashSet<T>,
        IToLinkedList<T>,
        IToQueue<T>,
        IToStack<T>,
        IToDictionary<T>,

        IBuildEnumerator<TCollect, T>
        
        where TCollect : struct, ICollectionWrapper<T>
    {
    }
}