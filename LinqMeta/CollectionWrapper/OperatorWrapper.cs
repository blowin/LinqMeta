using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqMeta.CollectionWrapper.EnumeratorWrapper;
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
using LinqMetaCore.Utils;

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
        public double Average()
        {
            return _collect.Average<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public decimal AverageDec()
        {
            return _collect.AverageDec<TCollect, T>();
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
            var functor = new FuncFunctor<T, T, bool>(firstGreat);
            return _collect.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Max()
        {
            return MaxOperator.MaxMeta<TCollect, T>(ref _collect);
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
            var functor = new FuncFunctor<T, T, bool>(firstGreat);
            return _collect.MinMeta<TCollect, FuncFunctor<T, T, bool>, T>(functor);
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
        public Option<T> First<TFilter>(TFilter predicat) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new WhereOperator<TCollect, TFilter, T>(_collect, predicat).FirstMeta<WhereOperator<TCollect, TFilter, T>, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First(Func<T, bool> predicat)
        {
            return new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(_collect, new FuncFunctor<T, bool>(predicat)).FirstMeta<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last()
        {
            return _collect.LastMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new WhereOperator<TCollect, TFilter, T>(_collect, filter).LastMeta<WhereOperator<TCollect, TFilter, T>, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last(Func<T, bool> filter)
        {
            return new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(_collect, new FuncFunctor<T, bool>(filter)).LastMeta<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>();
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
        public bool Contains(T val)
        {
            return _collect.ContainsMeta<TCollect, T>(val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return _collect.ContainsMeta<TCollect, T, TPredicat>(predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Func<T, bool> predicat)
        {
            return _collect.ContainsMeta<TCollect, T, FuncFunctor<T, bool>>(new FuncFunctor<T, bool>(predicat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T2, TPredicat>(TPredicat predicat, T2 val) 
            where TPredicat : struct, IFunctor<T, T2, bool>
        {
            return _collect.ContainsMeta<TCollect, T, TPredicat, T2>(predicat, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T2>(Func<T, T2, bool> predicat, T2 val)
        {
            return _collect.ContainsMeta<TCollect, T, FuncFunctor<T, T2, bool>, T2>(new FuncFunctor<T, T2, bool>(predicat), val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsEq<T2>(T2 val) 
            where T2 : IEquatable<T>
        {
            return _collect.ContainsEqMeta<TCollect, T, T2>(val);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count()
        {
            return _collect.CountMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return _collect.CountMeta<TCollect, T, TPredicat>(predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(Func<T, bool> predicat)
        {
            return _collect.CountMeta<TCollect, T, FuncFunctor<T, bool>>(new FuncFunctor<T, bool>(predicat));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount()
        {
            return _collect.LongCountMeta<TCollect, T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return _collect.LongCountMeta<TCollect, T, TPredicat>(predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount(Func<T, bool> predicat)
        {
            return _collect.LongCountMeta<TCollect, T, FuncFunctor<T, bool>>(new FuncFunctor<T, bool>(predicat));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>, TNew> Cast<TNew>() 
            where TNew : T
        {
            return new OperatorWrapper<SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>, TNew>(
                    new SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>(_collect, default(CastOperator<T, TNew>))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            return new OperatorWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew>(
                    new SelectOperator<TCollect, TSelector, T, TNew>(_collect, selector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> Select<TNew>(Func<T, TNew> selector)
        {
            return new OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew>(
                    new SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>(_collect, new FuncFunctor<T, TNew>(selector))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>
        {
            return new OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew>(
                    new SelectIndexingOperator<TCollect, TSelector, T, TNew>(_collect, selector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector)
        {
            return new OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew>(
                    new SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>(_collect, new FuncFunctor<ZipPair<T>, TNew>(selector))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>, T2> SelectMany<TOtherCollect, TSelector, T2>(TSelector @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2> where TSelector : struct, IFunctor<T, TOtherCollect>
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>, T2>(
                    new SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>(_collect, select)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>, T2> SelectMany<TOtherCollect, T2>(Func<T, TOtherCollect> @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>, T2>(
                    new SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>(_collect, new FuncFunctor<T, TOtherCollect>(select)))
                ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>, T2> SelectManyBox<TSelector, T2>(TSelector @select) 
            where TSelector : struct, IFunctor<T, ICollectionWrapper<T2>>
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>, T2>(
                    new SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>(_collect, select)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>, T2> SelectManyBox<T2>(Func<T, ICollectionWrapper<T2>> @select)
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>, T2>(
                    new SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>(
                            _collect, new FuncFunctor<T, ICollectionWrapper<T2>>(select)
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T> Where<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T>(
                    new WhereOperator<TCollect, TFilter, T>(_collect, filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> Where(Func<T, bool> filter)
        {
            return new OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(_collect, new FuncFunctor<T, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T>(
                    new WhereIndexingOperator<TCollect, TFilter, T>(_collect, filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndex(Func<ZipPair<T>, bool> filter)
        {
            return new OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(_collect, new FuncFunctor<ZipPair<T>, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeOperator<TCollect, T>, T> Take(uint count)
        {
            return new OperatorWrapper<TakeOperator<TCollect, T>, T>(
                    new TakeOperator<TCollect, T>(_collect, count)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T> TakeWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T>(
                    new TakeWhile<TCollect, TFilter, T>(_collect, filter)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T> TakeWhile(Func<T, bool> filter)
        {
            return new OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new TakeWhile<TCollect, FuncFunctor<T, bool>, T>(_collect, new FuncFunctor<T, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T>(
                    new TakeWhileIndexingOperator<TCollect, TFilter, T>(_collect, filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(_collect, new FuncFunctor<ZipPair<T>, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipOperator<TCollect, T>, T> Skip(uint count)
        {
            return new OperatorWrapper<SkipOperator<TCollect, T>, T>(
                    new SkipOperator<TCollect, T>(_collect, count)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileOperator<TCollect, TFilter, T>, T> SkipWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<SkipWhileOperator<TCollect, TFilter, T>, T>(
                    new SkipWhileOperator<TCollect, TFilter, T>(_collect, filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>, T> SkipWhile(Func<T, bool> filter)
        {
            return new OperatorWrapper<SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>(_collect, new FuncFunctor<T, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileIndexingOperator<TCollect, TFilter, T>, T> SkipWhileIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<SkipWhileIndexingOperator<TCollect, TFilter, T>, T>(
                    new SkipWhileIndexingOperator<TCollect, TFilter, T>(_collect, filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> SkipWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            return new OperatorWrapper<SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(_collect, new FuncFunctor<ZipPair<T>, bool>(filter))
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>> Zip<TOtherCollect, T2>(TOtherCollect collect2) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>>(
                    new ZipOperator<TCollect, T, TOtherCollect, T2>(_collect, collect2)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, TSelect, T2>(TOtherCollect collect2, TSelect select) 
            where TOtherCollect : struct, ICollectionWrapper<T2> 
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>
        {
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>(
                            new ZipOperator<TCollect, T, TOtherCollect, T2>(_collect, collect2), select
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, T2>(TOtherCollect collect2, Func<Pair<T, T2>, T2> @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>(
                            new ZipOperator<TCollect, T, TOtherCollect, T2>(_collect, collect2), new FuncFunctor<Pair<T, T2>, T2>(select)
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, Pair<T, T2>> ZipBox<T2>(ICollectionWrapper<T2> collect2)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            return new OperatorWrapper<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, Pair<T, T2>>(
                new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(_collect, collect2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>, T2> ZipBoxSelect<TSelect, T2>(ICollectionWrapper<T2> collect2, TSelect @select) 
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>(
                            new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(_collect, collect2), select
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2> ZipBoxSelect<T2>(ICollectionWrapper<T2> collect2, Func<Pair<T, T2>, T2> @select)
        {
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>(
                            new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(_collect, collect2), new FuncFunctor<Pair<T, T2>, T2>(select)
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T> Concat<TOther>(TOther other) 
            where TOther : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T>(new ConcatOperator<TCollect, TOther, T>(_collect, other));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty()
        {
            return new OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T>(new DefaultIfEmptyOperator<TCollect, T>(_collect, default(T)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty(T defaultVal)
        {
            return new OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T>(new DefaultIfEmptyOperator<TCollect, T>(_collect, defaultVal));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, TFactory>, T> DefaultIfEmpty<TFactory>(TFactory factory) 
            where TFactory : struct, IFunctor<T>
        {
            return new OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, TFactory>, T>(new LazyDefaultIfEmptyOperator<TCollect, T, TFactory>(_collect, factory));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>, T> DefaultIfEmpty(Func<T> factory)
        {
            return new OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>, T>(new LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>(_collect, new FuncFunctor<T>(factory)));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ReverseOperator<TCollect, T>, T> Reverse()
        {
            return new OperatorWrapper<ReverseOperator<TCollect, T>, T>(new ReverseOperator<TCollect, T>(_collect));
        }
        
        public CollectEnumeratorWrapper<TCollect, T> BuildEnumerator()
        {
            return new CollectEnumeratorWrapper<TCollect, T>(_collect);
        }

        public IEnumerator<T> BuildBoxEnumerator()
        {
            return _collect.HasIndexOverhead
                ? (IEnumerator<T>) new CollectOverheadEnumeratorWrapper<TCollect, T>(_collect)
                : new CollectIndexEnumeratorWrapper<TCollect, T>(_collect);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray(uint? capacity = null)
        {
            return ToArrayOperators.ToMetaArray<TCollect, T>(ref _collect, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> ToList(uint? capacity = null)
        {
            return ToListOperators.ToMetaList<TCollect, T>(ref _collect, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HashSet<T> ToHashSet()
        {
            return ToHashSetOperators.ToMetaHashSetMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HashSet<T> ToHashSet(IEqualityComparer<T> comparer)
        {
            return ToHashSetOperators.ToMetaHashSetMeta<TCollect, T>(ref _collect, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinkedList<T> ToLinkedList()
        {
            return ToLinkedListOperators.ToLinkedListMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Queue<T> ToQueue(uint? capacity = null)
        {
            return ToQueueOperators.ToQueueMeta<TCollect, T>(ref _collect, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Stack<T> ToStack(uint? capacity = null)
        {
            return ToStackOperators.ToStackMeta<TCollect, T>(ref _collect, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> keySelector, uint? capacity = null)
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, FuncFunctor<T, TKey>>(ref _collect,
                new FuncFunctor<T, TKey>(keySelector), capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, T> ToDictionary<TKey>(Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, uint? capacity = null)
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, FuncFunctor<T, TKey>>(ref _collect,
                new FuncFunctor<T, TKey>(keySelector), comparer, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, TElem> ToDictionary<TKey, TElem>(Func<T, TKey> keySelector, Func<T, TElem> elementSelector, uint? capacity = null)
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, TElem, FuncFunctor<T, TKey>, FuncFunctor<T, TElem>>(ref _collect,
                new FuncFunctor<T, TKey>(keySelector), new FuncFunctor<T, TElem>(elementSelector), capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, TElem> ToDictionary<TKey, TElem>(Func<T, TKey> keySelector, Func<T, TElem> elementSelector, IEqualityComparer<TKey> comparer,
            uint? capacity = null)
        {
            return ToDictionaryOperators
                .ToDictionaryMeta<TCollect, T, TKey, TElem, FuncFunctor<T, TKey>, FuncFunctor<T, TElem>>(
                    ref _collect, new FuncFunctor<T, TKey>(keySelector), new FuncFunctor<T, TElem>(elementSelector),
                    comparer, capacity
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, T> ToDictionary<TSelect, TKey>(TSelect keySelector, uint? capacity = null) 
            where TSelect : struct, IFunctor<T, TKey>
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, TSelect>(ref _collect, keySelector, capacity);
        }

        public Dictionary<TKey, T> ToDictionary<TKeySelector, TKey>(TKeySelector keySelector, IEqualityComparer<TKey> comparer, uint? capacity = null) 
            where TKeySelector : struct, IFunctor<T, TKey>
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, TKeySelector>(ref _collect, keySelector, comparer, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, TElem> ToDictionary<TKeySelector, TSelect, TKey, TElem>(TKeySelector keySelector, TSelect elementSelector, uint? capacity = null) 
            where TKeySelector : struct, IFunctor<T, TKey> where TSelect : struct, IFunctor<T, TElem>
        {
            return ToDictionaryOperators.ToDictionaryMeta<TCollect, T, TKey, TElem, TKeySelector, TSelect>(ref _collect,
                keySelector, elementSelector, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<TKey, TElem> ToDictionary<TKeySelector, TElemSelector, TKey, TElem>(TKeySelector keySelector,
            TElemSelector elementSelector, IEqualityComparer<TKey> comparer, uint? capacity = null) 
            where TKeySelector : struct, IFunctor<T, TKey> 
            where TElemSelector : struct, IFunctor<T, TElem>
        {
            return ToDictionaryOperators
                .ToDictionaryMeta<TCollect, T, TKey, TElem, TKeySelector, TElemSelector>(
                    ref _collect, keySelector, elementSelector, comparer, capacity
                );
        }
    }
}