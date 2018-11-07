using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.Core;
using LinqMeta.Core.Statistic;
using LinqMeta.Extensions.Converters;
using LinqMeta.Extensions.Operators;
using LinqMeta.Extensions.Operators.CollectWrapperOperators;
using LinqMeta.Functors;
using LinqMeta.Operators;
using LinqMeta.Operators.Numbers;

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
        public T Max<TComparer>(TComparer firstGreat) 
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return _collect.MaxMeta<TCollect, TComparer, T>(firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Max(Func<T, T, bool> firstGreat)
        {
            return _collect.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(firstGreat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Max()
        {
            return _collect.MaxMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Min<TComparer>(TComparer firstGreat) 
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return _collect.MinMeta<TCollect, TComparer, T>(firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Min(Func<T, T, bool> firstGreat)
        {
            return _collect.MinMeta<TCollect, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(firstGreat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Min()
        {
            return _collect.MinMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Aggregate<TFolder, TRes>(TRes init, TFolder folder) where TFolder : struct, IFunctor<TRes, T, TRes>
        {
            return _collect.AggregateMeta<TCollect, TFolder, T, TRes>(init, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Aggregate<TFolder>(TFolder folder) 
            where TFolder : struct, IFunctor<T, T, T>
        {
            return _collect.AggregateMeta<TCollect, TFolder, T>(folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Aggregate<TRes>(TRes init, Func<TRes, T, TRes> folder)
        {
            return _collect.AggregateMeta<TCollect, T, TRes>(init, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Aggregate(Func<T, T, T> folder)
        {
            return _collect.AggregateMeta<TCollect, T>(folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Sum()
        {
            return _collect.SumMeta<TCollect, T>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First()
        {
            return _collect.FirstMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last()
        {
            return _collect.LastMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> ElementAt(uint index)
        {
            return _collect.NthMeta<TCollect, T>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return !First().HasValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatisticInfo<T>? GetStatistic(StatisticFlags statisticFlags)
        {
            return _collect.GetStatisticMeta<TCollect, T>(statisticFlags);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> Select<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<T, TNew>
        {
            return new CollectWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> Select<TNew>(Func<T, TNew> selector)
        {
            return new CollectWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew>(_collect.SelectMeta<TCollect, T, TNew>(selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>
        {
            return new CollectWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectIndexMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector)
        {
            return new CollectWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew>(_collect.SelectIndexMeta<TCollect, T, TNew>(selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereOperator<TCollect, TFilter, T>, T> Where<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new CollectWrapper<WhereOperator<TCollect, TFilter, T>, T>(_collect.WhereMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> Where(Func<T, bool> filter)
        {
            return new CollectWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(_collect.WhereMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new CollectWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T>(_collect.WhereIndexMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndex(Func<ZipPair<T>, bool> filter)
        {
            return new CollectWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(_collect.WhereIndexMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeOperator<TCollect, T>, T> Take(uint count)
        {
            return new CollectWrapper<TakeOperator<TCollect, T>, T>(_collect.TakeMeta<TCollect, T>(count));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeWhile<TCollect, TFilter, T>, T> TakeWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new CollectWrapper<TakeWhile<TCollect, TFilter, T>, T>(_collect.TakeWhileMeta<TCollect, TFilter, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T> TakeWhile(Func<T, bool> filter)
        {
            return new CollectWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T>(_collect.TakeWhileMeta<TCollect, FuncFunctor<T, bool>, T>(new FuncFunctor<T, bool>(filter)));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter) where TFilter : 
            struct, IFunctor<ZipPair<T>, bool>
        {
            return new CollectWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T>(_collect.TakeWhileIndexMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CollectWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            return new CollectWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(_collect.TakeWhileIndexMeta<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(new FuncFunctor<ZipPair<T>, bool>(filter)));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray(uint? capacity)
        {
            return _collect.ToMetaArray<TCollect, T>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToList(uint? capacity)
        {
            return _collect.ToMetaList<TCollect, T>(capacity);
        }
    }
}