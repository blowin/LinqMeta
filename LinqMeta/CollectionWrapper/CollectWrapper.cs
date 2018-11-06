using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.Core;
using LinqMeta.Extensions.Converters;
using LinqMeta.Extensions.Operators;
using LinqMeta.Extensions.Operators.CollectWrapperOperators;
using LinqMeta.Extensions.Operators.IOperator;
using LinqMeta.Functors;
using LinqMeta.Operators;

namespace LinqMeta.CollectionWrapper
{
    public struct CollectWrapper<TCollect, T> : 
        IOperators<TCollect, T> 
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;
        
        public CollectWrapper(TCollect collect)
        {
            _collect = collect;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MaxMeta<TComparer>(TComparer firstGreat) 
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return _collect.MaxMeta<TCollect, TComparer, T>(firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MaxMeta(Func<T, T, bool> firstGreat)
        {
            return _collect.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(firstGreat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MaxMeta()
        {
            return _collect.MaxMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MinMeta<TComparer>(TComparer firstGreat) 
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return _collect.MinMeta<TCollect, TComparer, T>(firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MinMeta(Func<T, T, bool> firstGreat)
        {
            return _collect.MinMeta<TCollect, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(firstGreat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T MinMeta()
        {
            return _collect.MinMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes AggregateMeta<TFolder, TRes>(TRes init, TFolder folder) where TFolder : struct, IFunctor<TRes, T, TRes>
        {
            return _collect.AggregateMeta<TCollect, TFolder, T, TRes>(init, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T AggregateMeta<TFolder>(TFolder folder) 
            where TFolder : struct, IFunctor<T, T, T>
        {
            return _collect.AggregateMeta<TCollect, TFolder, T>(folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes AggregateMeta<TRes>(TRes init, Func<TRes, T, TRes> folder)
        {
            return _collect.AggregateMeta<TCollect, T, TRes>(init, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T AggregateMeta(Func<T, T, T> folder)
        {
            return _collect.AggregateMeta<TCollect, T>(folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T SumMeta()
        {
            return _collect.SumMeta<TCollect, T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First()
        {
            return _collect.FirstMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> LastMeta()
        {
            return _collect.LastMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> NthMeta(uint index)
        {
            return _collect.NthMeta<TCollect, T>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> SelectMeta<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<T, TNew>
        {
            return new CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> SelectMeta<TNew>(Func<T, TNew> selector)
        {
            return new CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew>(_collect.SelectMeta<TCollect, T, TNew>(selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndexMeta<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>
        {
            return new CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectIndexMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndexMeta<TNew>(Func<ZipPair<T>, TNew> selector)
        {
            return new CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew>(_collect.SelectIndexMeta<TCollect, T, TNew>(selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereOperator<TCollect, TFilter, T>, T> WhereMeta<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new CollectWrapper<WhereOperator<TCollect, TFilter, T>, T>(_collect.WhereMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> WhereMeta(Func<T, bool> filter)
        {
            return new CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(_collect.WhereMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndexMeta<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new CollectWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T>(_collect.WhereIndexMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndexMeta(Func<ZipPair<T>, bool> filter)
        {
            return new CollectWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(_collect.WhereIndexMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeOperator<TCollect, T>, T> TakeMeta(uint count)
        {
            return new CollectWrapper<TakeOperator<TCollect, T>, T>(_collect.TakeMeta<TCollect, T>(count));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToMetaArray(uint? capacity)
        {
            return _collect.ToMetaArray<TCollect, T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToMetaList(uint? capacity)
        {
            return _collect.ToMetaList<TCollect, T>(capacity);
        }
    }
}