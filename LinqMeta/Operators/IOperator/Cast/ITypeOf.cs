using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.Cast;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator.Cast
{
    public interface ITypeOf<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>, TNew> 
            OfType<TNew>() where TNew : class;
    }
}