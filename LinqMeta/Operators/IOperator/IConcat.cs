using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IConcat<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T> Concat<TOther>(TOther other)
                where TOther : struct, ICollectionWrapper<T>;
    }
}