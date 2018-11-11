using System;
using LinqMeta.CollectionWrapper;
using LinqMeta.Functors;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IZip<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>> Zip<TOtherCollect, T2>(TOtherCollect collect2)
            where TOtherCollect : struct, ICollectionWrapper<T2>;

        OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2>
            ZipSelect<TOtherCollect, TSelect, T2>(TOtherCollect collect2, TSelect select)
            where TOtherCollect : struct, ICollectionWrapper<T2>
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>;
        
        OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2>
            ZipSelect<TOtherCollect, T2>(TOtherCollect collect2, Func<Pair<T, T2>, T2> select)
            where TOtherCollect : struct, ICollectionWrapper<T2>;
    }
}