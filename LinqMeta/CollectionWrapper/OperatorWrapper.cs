using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.DataTypes.Statistic;
using LinqMeta.Extensions.Converters;
using LinqMeta.Extensions.Operators;
using LinqMeta.Extensions.Operators.CollectWrapperOperators;
using LinqMeta.Functors;
using LinqMeta.Operators;
using LinqMeta.Operators.Cast;
using LinqMeta.Operators.CollectOperator;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace LinqMeta.CollectionWrapper
{
    public struct OperatorWrapper<TCollect, T> : 
        IOperators<TCollect, T> 
        where TCollect : struct, ICollectionWrapper<T>
    {
        private TCollect _collect;

        public TCollect Collect
        {
            get { return _collect; }
        }
        
        public OperatorWrapper(TCollect collect)
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
        public MinMaxPair<T>? MaxMin<TMaxComparer, TMinComparer>(TMaxComparer maxComparer, TMinComparer minComparer) 
            where TMaxComparer : struct, IFunctor<T, T, bool> 
            where TMinComparer : struct, IFunctor<T, T, bool>
        {
            return _collect.MaxMinMeta<TCollect, TMaxComparer, TMinComparer, T>(maxComparer, minComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MinMaxPair<T>? MaxMin(Func<T, T, bool> maxComparer, Func<T, T, bool> minComparer)
        {
            return _collect.MaxMinMeta<TCollect, FuncFunctor<T, T, bool>, FuncFunctor<T, T, bool>, T>(new FuncFunctor<T, T, bool>(maxComparer), new FuncFunctor<T, T, bool>(minComparer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MinMaxPair<T>? MaxMin()
        {
            return _collect.MaxMinMeta<TCollect, T>();
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
        public StatisticInfo<T>? GetStatistic(StatisticValue flagsBuff)
        {
            return _collect.GetStatisticMeta<TCollect, T>(flagsBuff);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any()
        {
            return _collect.AnyMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return _collect.AnyMeta<TCollect, TFilter, T>(filter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any(Func<T, bool> filter)
        {
            return _collect.AnyMeta<TCollect, FuncFunctor<T, bool>, T>(new FuncFunctor<T, bool>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return _collect.AllMeta<TCollect, TFilter, T>(filter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All(Func<T, bool> filter)
        {
            return _collect.AllMeta<TCollect, FuncFunctor<T, bool>, T>(new FuncFunctor<T, bool>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>, TNew> Cast<TNew>() 
            where TNew : T
        {
            return Select<CastOperator<T, TNew>, TNew>(default(CastOperator<T, TNew>));
        }
        
        public OperatorWrapper<SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>, TNew> UnsafeCast<TNew>()
        {
            return new OperatorWrapper<SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>, TNew>(
                new SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>(_collect, default(UnsafeCast<T, TNew>))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>, TNew> OfType<TNew>() 
            where TNew : class
        {
            return new OperatorWrapper<WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>, TNew>(
                    new WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>(
                            new SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>(_collect, default(TypeOfOperator<T, TNew>)), 
                            default(NotNullOperator<TNew>)
                        )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> Select<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<T, TNew>
        {
            return new OperatorWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> Select<TNew>(Func<T, TNew> selector)
        {
            return new OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew>(_collect.SelectMeta<TCollect, T, TNew>(selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>
        {
            return new OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew>(_collect.SelectIndexMeta<TCollect, TSelector, T, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector)
        {
            return SelectIndex<FuncFunctor<ZipPair<T>, TNew>, TNew>(new FuncFunctor<ZipPair<T>, TNew>(selector));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T> Where<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T>(_collect.WhereMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> Where(Func<T, bool> filter)
        {
            return new OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(_collect.WhereMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T>(_collect.WhereIndexMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndex(Func<ZipPair<T>, bool> filter)
        {
            return new OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(_collect.WhereIndexMeta<TCollect, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeOperator<TCollect, T>, T> Take(uint count)
        {
            return new OperatorWrapper<TakeOperator<TCollect, T>, T>(_collect.TakeMeta<TCollect, T>(count));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T> TakeWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T>(_collect.TakeWhileMeta<TCollect, TFilter, T>(filter));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T> TakeWhile(Func<T, bool> filter)
        {
            return new OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T>(_collect.TakeWhileMeta<TCollect, FuncFunctor<T, bool>, T>(new FuncFunctor<T, bool>(filter)));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter) where TFilter : 
            struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T>(_collect.TakeWhileIndexMeta<TCollect, TFilter, T>(filter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(_collect.TakeWhileIndexMeta<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(new FuncFunctor<ZipPair<T>, bool>(filter)));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>> Zip<TOtherCollect, T2>(TOtherCollect collect2) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>>(_collect.ZipMeta<TCollect, T, TOtherCollect, T2>(collect2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, TSelect, T2>(TOtherCollect collect2, TSelect select) 
            where TOtherCollect : struct, ICollectionWrapper<T2> 
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>
        {
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2>(_collect.ZipMeta<TCollect, T, TOtherCollect, T2>(collect2).SelectMeta<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>(select));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, T2>(TOtherCollect collect2, Func<Pair<T, T2>, T2> @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return ZipSelect<TOtherCollect, FuncFunctor<Pair<T, T2>, T2>, T2>(collect2, new FuncFunctor<Pair<T, T2>, T2>(select));
        }

        public OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T> Concat<TOther>(TOther other) 
            where TOther : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T>(_collect.ConcatMeta<TCollect, TOther, T>(other));
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