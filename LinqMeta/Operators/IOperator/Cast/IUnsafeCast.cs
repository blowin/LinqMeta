using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.Cast;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Cast
{
    public interface IUnsafeCast<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>, TNew> UnsafeCast<TNew>();
    }
}