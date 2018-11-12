using System.Collections.Generic;
using LinqMeta.CollectionWrapper.EnumeratorWrapper;
using LinqMetaCore.Intefaces;

namespace LinqMeta.Operators.IOperator
{
    public interface IBuildEnumerator<TCollect, T>
        where TCollect : struct, ICollectionWrapper<T>
    {
        CollectEnumeratorWrapper<TCollect, T> BuildEnumerator();

        IEnumerator<T> BuildBoxEnumerator();
    }
}