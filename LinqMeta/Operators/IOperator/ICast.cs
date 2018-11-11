using LinqMeta.CollectionWrapper;
using LinqMeta.Operators.Cast;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface ICast<TCollect, TOld>
        where TCollect : struct, ICollectionWrapper<TOld>
    {
        OperatorWrapper<SelectOperator<TCollect, CastOperator<TOld, TNew>, TOld, TNew>, TNew> Cast<TNew>() 
            where TNew : TOld;
    }
}