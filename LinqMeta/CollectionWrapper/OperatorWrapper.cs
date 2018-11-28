using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using LinqMeta.CollectionWrapper.EnumeratorWrapper;
using LinqMeta.DataTypes.Groupin;
using LinqMeta.DataTypes.SetMeta;
using LinqMeta.DataTypes.Statistic;
using LinqMeta.Extensions.Converters;
using LinqMeta.Extensions.Operators;
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
            return AverageOperator.Average<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public decimal AverageDec()
        {
            return AverageOperator.AverageDec<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Max<TComparer>(TComparer firstGreat) 
            where TComparer : struct, IFunctor<T, T, bool>
        {
            return MaxOperator.MaxMeta<TCollect, TComparer, T>(ref _collect, ref firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Max(Func<T, T, bool> firstGreat)
        {
            var functor = new FuncFunctor<T, T, bool>(firstGreat);
            return MaxOperator.MaxMeta<TCollect, FuncFunctor<T, T, bool>, T>(ref _collect, ref functor);
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
            return MinOperator.MinMeta<TCollect, TComparer, T>(ref _collect, ref firstGreat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Min(Func<T, T, bool> firstGreat)
        {
            var functor = new FuncFunctor<T, T, bool>(firstGreat);
            return MinOperator.MinMeta<TCollect, FuncFunctor<T, T, bool>, T>(ref _collect, ref functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Min()
        {
            return MinOperator.MinMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MinMaxPair<T>? MaxMin<TMaxComparer, TMinComparer>(TMaxComparer maxComparer, TMinComparer minComparer) 
            where TMaxComparer : struct, IFunctor<T, T, bool> 
            where TMinComparer : struct, IFunctor<T, T, bool>
        {
            return MaxMinMetaOperator.MaxMinMeta<TCollect, TMaxComparer, TMinComparer, T>(
                    ref _collect, ref maxComparer, ref minComparer
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MinMaxPair<T>? MaxMin(Func<T, T, bool> maxComparer, Func<T, T, bool> minComparer)
        {
            var maxComparerWrap = new FuncFunctor<T, T, bool>(maxComparer);
            var minComparerWrap = new FuncFunctor<T, T, bool>(minComparer);
            return MaxMinMetaOperator.MaxMinMeta<TCollect, FuncFunctor<T, T, bool>, FuncFunctor<T, T, bool>, T>(
                ref _collect, ref maxComparerWrap, ref minComparerWrap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MinMaxPair<T>? MaxMin()
        {
            return MaxMinMetaOperator.MaxMinMeta<TCollect, T>(ref _collect);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Aggregate<TFolder, TRes>(TRes init, TFolder folder) 
            where TFolder : struct, IFunctor<TRes, T, TRes>
        {
            return AggregateOperator.AggregateMeta<TCollect, TFolder, T, TRes>(ref _collect, init, ref folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Aggregate<TFolder>(TFolder folder) 
            where TFolder : struct, IFunctor<T, T, T>
        {
            return AggregateOperator.AggregateMeta<TCollect, TFolder, T>(ref _collect, ref folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TRes Aggregate<TRes>(TRes init, Func<TRes, T, TRes> folder)
        {
            return AggregateOperator.AggregateMeta<TCollect, T, TRes>(ref _collect, init, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Aggregate(Func<T, T, T> folder)
        {
            return AggregateOperator.AggregateMeta<TCollect, T>(ref _collect, folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Sum()
        {
            return SumOperator.SumMeta<TCollect, T>(ref _collect);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First()
        {
            return FirstLastOperators.FirstMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First<TFilter>(TFilter predicat) 
            where TFilter : struct, IFunctor<T, bool>
        {
            var where = new WhereOperator<TCollect, TFilter, T>(ref _collect, ref predicat);
            return FirstLastOperators.FirstMeta<WhereOperator<TCollect, TFilter, T>, T>(ref where);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> First(Func<T, bool> predicat)
        {
            var func = new FuncFunctor<T, bool>(predicat);
            var where = new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func);
            return FirstLastOperators.FirstMeta<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(ref where);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T FirstOrDefault()
        {
            return First().GetValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T FirstOrDefault<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return First<TPredicat>(predicat).GetValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T FirstOrDefault(Func<T, bool> predicat)
        {
            return First(predicat).GetValueOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last()
        {
            return FirstLastOperators.LastMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            var where = new WhereOperator<TCollect, TFilter, T>(ref _collect, ref filter);
            return FirstLastOperators.LastMeta<WhereOperator<TCollect, TFilter, T>, T>(ref where);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Last(Func<T, bool> filter)
        {
            var func = new FuncFunctor<T, bool>(filter);
            var where = new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func);
            return FirstLastOperators.LastMeta<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(ref where);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T LastOrDefault()
        {
            return Last().GetValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T LastOrDefault<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return Last(filter).GetValueOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T LastOrDefault(Func<T, bool> filter)
        {
            return Last(filter).GetValueOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> ElementAt(uint index)
        {
            return FirstLastOperators.NthMeta<TCollect, T>(ref _collect, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ElementAtOrDefault(uint index)
        {
            return ElementAt(index).GetValueOrDefault();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return !First().HasValue;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StatisticInfo<T>? GetStatistic(StatisticValue flagsBuff)
        {
            return StatisticOperator.GetStatisticMeta<TCollect, T>(ref _collect, flagsBuff);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any()
        {
            return AnyOperators.AnyMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return AnyOperators.AnyMeta<TCollect, TFilter, T>(ref _collect, ref filter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Any(Func<T, bool> filter)
        {
            var func = new FuncFunctor<T, bool>(filter);
            return AnyOperators.AnyMeta<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return AllOperators.AllMeta<TCollect, TFilter, T>(ref _collect, ref filter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool All(Func<T, bool> filter)
        {
            var func = new FuncFunctor<T, bool>(filter);
            return AllOperators.AllMeta<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T val)
        {
            return ContainsOperator.ContainsMeta<TCollect, T>(ref _collect, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return ContainsOperator.ContainsMeta<TCollect, T, TPredicat>(ref _collect, ref predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Func<T, bool> predicat)
        {
            var functor = new FuncFunctor<T, bool>(predicat);
            return ContainsOperator.ContainsMeta<TCollect, T, FuncFunctor<T, bool>>(ref _collect, ref functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T2, TPredicat>(TPredicat predicat, T2 val) 
            where TPredicat : struct, IFunctor<T, T2, bool>
        {
            return ContainsOperator.ContainsMeta<TCollect, T, TPredicat, T2>(ref _collect, ref predicat, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains<T2>(Func<T, T2, bool> predicat, T2 val)
        {
            var functor = new FuncFunctor<T, T2, bool>(predicat);
            return ContainsOperator.ContainsMeta<TCollect, T, FuncFunctor<T, T2, bool>, T2>(ref _collect, ref functor, val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsEq<T2>(T2 val) 
            where T2 : IEquatable<T>
        {
            return ContainsOperator.ContainsEqMeta<TCollect, T, T2>(ref _collect, val);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count()
        {
            return CountOperator.CountMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return CountOperator.CountMeta<TCollect, T, TPredicat>(ref _collect, ref predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(Func<T, bool> predicat)
        {
            var functor = new FuncFunctor<T, bool>(predicat);
            return CountOperator.CountMeta<TCollect, T, FuncFunctor<T, bool>>(ref _collect, ref functor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount()
        {
            return LongCountOperator.LongCountMeta<TCollect, T>(ref _collect);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount<TPredicat>(TPredicat predicat) 
            where TPredicat : struct, IFunctor<T, bool>
        {
            return LongCountOperator.LongCountMeta<TCollect, T, TPredicat>(ref _collect, ref predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long LongCount(Func<T, bool> predicat)
        {
            var functor = new FuncFunctor<T, bool>(predicat);
            return LongCountOperator.LongCountMeta<TCollect, T, FuncFunctor<T, bool>>(ref _collect, ref functor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqual<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return SequenceEqualOperator.SequenceEqual<TCollect, TSecond, T>(ref _collect, ref second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqual<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return SequenceEqualOperator.SequenceEqual<TCollect, TSecond, T>(ref _collect, ref second, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqual<TSecond, TPredicat>(TSecond second, TPredicat predicat) 
            where TSecond : struct, ICollectionWrapper<T> 
            where TPredicat : struct, IFunctor<T, T, bool>
        {
            return SequenceEqualOperator.SequenceEqual<TCollect, TSecond, TPredicat, T, T>(ref _collect, ref second, ref predicat);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqual<TSecond>(TSecond second, Func<T, T, bool> predicat) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            var func = new FuncFunctor<T, T, bool>(predicat);
            return SequenceEqualOperator.SequenceEqual<TCollect, TSecond, FuncFunctor<T, T, bool>, T, T>(ref _collect, ref second, ref func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqualEq<TSecond, T2>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T2> 
            where T2 : IEquatable<T>
        {
            return SequenceEqualOperator.SequenceEqualEq<TCollect, TSecond, T, T2>(ref _collect, ref second);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SequenceEqualEqBox<T2>(ICollectionWrapper<T2> second) 
            where T2 : IEquatable<T>
        {
            ErrorUtil.NullCheck(second, "second");
            return SequenceEqualOperator.SequenceEqualEq<TCollect, ICollectionWrapper<T2>, T, T2>(ref _collect, ref second);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEach<TAction>(TAction action) 
            where TAction : struct, IFunctor<T, MetaVoid>
        {
            ForEachOperator.ForEachMeta<TCollect, TAction, T>(ref _collect, ref action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEach(Action<T> action)
        {
            var functor = new ActionFunctor<T>(action);
            ForEachOperator.ForEachMeta<TCollect, ActionFunctor<T>, T>(ref _collect, ref functor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>, TNew> Cast<TNew>() 
            where TNew : T
        {
            var caster = default(CastOperator<T, TNew>);
            return new OperatorWrapper<SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>, TNew>(
                    new SelectOperator<TCollect, CastOperator<T, TNew>, T, TNew>(ref _collect, ref caster)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>, TNew> UnsafeCast<TNew>()
        {
            var caster = default(UnsafeCast<T, TNew>);
            return new OperatorWrapper<SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>, TNew>(
                new SelectOperator<TCollect, UnsafeCast<T, TNew>, T, TNew>(ref _collect, ref caster)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>, TNew> OfType<TNew>() 
            where TNew : class
        {
            var typeOfOperator = default(TypeOfOperator<T, TNew>);
            var notNullOperator = default(NotNullOperator<TNew>);
            var selectOperator =
                new SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>(ref _collect, ref typeOfOperator);
            return new OperatorWrapper<WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>, TNew>(
                    new WhereOperator<SelectOperator<TCollect, TypeOfOperator<T, TNew>, T, TNew>, NotNullOperator<TNew>, TNew>(
                            ref selectOperator, ref notNullOperator
                        )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew> Select<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<T, TNew>
        {
            return new OperatorWrapper<SelectOperator<TCollect, TSelector, T, TNew>, TNew>(
                    new SelectOperator<TCollect, TSelector, T, TNew>(ref _collect, ref selector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew> Select<TNew>(Func<T, TNew> selector)
        {
            var func = new FuncFunctor<T, TNew>(selector);
            return new OperatorWrapper<SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>, TNew>(
                    new SelectOperator<TCollect, FuncFunctor<T, TNew>, T, TNew>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew> SelectIndex<TSelector, TNew>(TSelector selector) 
            where TSelector : struct, IFunctor<ZipPair<T>, TNew>
        {
            return new OperatorWrapper<SelectIndexingOperator<TCollect, TSelector, T, TNew>, TNew>(
                    new SelectIndexingOperator<TCollect, TSelector, T, TNew>(ref _collect, ref selector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew> SelectIndex<TNew>(Func<ZipPair<T>, TNew> selector)
        {
            var func = new FuncFunctor<ZipPair<T>, TNew>(selector);
            return new OperatorWrapper<SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>, TNew>(
                    new SelectIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, TNew>, T, TNew>(ref _collect, ref func)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>, T2> SelectMany<TOtherCollect, TSelector, T2>(TSelector @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2> where TSelector : struct, IFunctor<T, TOtherCollect>
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>, T2>(
                    new SelectManyOperator<TCollect, T, TOtherCollect, T2, TSelector>(ref _collect, ref select)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>, T2> SelectMany<TOtherCollect, T2>(Func<T, TOtherCollect> @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            var func = new FuncFunctor<T, TOtherCollect>(select);
            return new OperatorWrapper<SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>, T2>(
                    new SelectManyOperator<TCollect, T, TOtherCollect, T2, FuncFunctor<T, TOtherCollect>>(ref _collect, ref func))
                ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>, T2> SelectManyBox<TSelector, T2>(TSelector @select) 
            where TSelector : struct, IFunctor<T, ICollectionWrapper<T2>>
        {
            return new OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>, T2>(
                    new SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, TSelector>(ref _collect, ref select)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>, T2> SelectManyBox<T2>(Func<T, ICollectionWrapper<T2>> @select)
        {
            var functor = new FuncFunctor<T, ICollectionWrapper<T2>>(select);
            return new OperatorWrapper<SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>, T2>(
                    new SelectManyOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, ICollectionWrapper<T2>>>(
                           ref _collect, ref functor
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T> Where<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<WhereOperator<TCollect, TFilter, T>, T>(
                    new WhereOperator<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T> Where(Func<T, bool> filter)
        {
            var functor = new FuncFunctor<T, bool>(filter);
            return new OperatorWrapper<WhereOperator<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new WhereOperator<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref functor)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T> WhereIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<WhereIndexingOperator<TCollect, TFilter, T>, T>(
                    new WhereIndexingOperator<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> WhereIndex(Func<ZipPair<T>, bool> filter)
        {
            var func = new FuncFunctor<ZipPair<T>, bool>(filter);
            return new OperatorWrapper<WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new WhereIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeOperator<TCollect, T>, T> Take(uint count)
        {
            return new OperatorWrapper<TakeOperator<TCollect, T>, T>(
                    new TakeOperator<TCollect, T>(ref _collect, count)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T> TakeWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<TakeWhile<TCollect, TFilter, T>, T>(
                    new TakeWhile<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T> TakeWhile(Func<T, bool> filter)
        {
            var func = new FuncFunctor<T, bool>(filter);
            return new OperatorWrapper<TakeWhile<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new TakeWhile<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T> TakeWhileIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, TFilter, T>, T>(
                    new TakeWhileIndexingOperator<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> TakeWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            var func = new FuncFunctor<ZipPair<T>, bool>(filter);
            return new OperatorWrapper<TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new TakeWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipOperator<TCollect, T>, T> Skip(uint count)
        {
            return new OperatorWrapper<SkipOperator<TCollect, T>, T>(
                    new SkipOperator<TCollect, T>(ref _collect, count)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileOperator<TCollect, TFilter, T>, T> SkipWhile<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<T, bool>
        {
            return new OperatorWrapper<SkipWhileOperator<TCollect, TFilter, T>, T>(
                    new SkipWhileOperator<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>, T> SkipWhile(Func<T, bool> filter)
        {
            var func = new FuncFunctor<T, bool>(filter);
            return new OperatorWrapper<SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>, T>(
                    new SkipWhileOperator<TCollect, FuncFunctor<T, bool>, T>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileIndexingOperator<TCollect, TFilter, T>, T> SkipWhileIndex<TFilter>(TFilter filter) 
            where TFilter : struct, IFunctor<ZipPair<T>, bool>
        {
            return new OperatorWrapper<SkipWhileIndexingOperator<TCollect, TFilter, T>, T>(
                    new SkipWhileIndexingOperator<TCollect, TFilter, T>(ref _collect, ref filter)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T> SkipWhileIndex(Func<ZipPair<T>, bool> filter)
        {
            var func = new FuncFunctor<ZipPair<T>, bool>(filter);
            return new OperatorWrapper<SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>, T>(
                    new SkipWhileIndexingOperator<TCollect, FuncFunctor<ZipPair<T>, bool>, T>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>> Zip<TOtherCollect, T2>(TOtherCollect collect2) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            return new OperatorWrapper<ZipOperator<TCollect, T, TOtherCollect, T2>, Pair<T, T2>>(
                    new ZipOperator<TCollect, T, TOtherCollect, T2>(ref _collect, ref collect2)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, TSelect, T2>(TOtherCollect collect2, TSelect select) 
            where TOtherCollect : struct, ICollectionWrapper<T2> 
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>
        {
            var zipOperator = new ZipOperator<TCollect, T, TOtherCollect, T2>(ref _collect, ref collect2);
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, TSelect, Pair<T, T2>, T2>(
                            ref zipOperator, ref select
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2> ZipSelect<TOtherCollect, T2>(TOtherCollect collect2, Func<Pair<T, T2>, T2> @select) 
            where TOtherCollect : struct, ICollectionWrapper<T2>
        {
            var zipOperator = new ZipOperator<TCollect, T, TOtherCollect, T2>(ref _collect, ref collect2);
            var functor = new FuncFunctor<Pair<T, T2>, T2>(select);
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, TOtherCollect, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>(
                            ref zipOperator, ref functor
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, Pair<T, T2>> ZipBox<T2>(ICollectionWrapper<T2> collect2)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            return new OperatorWrapper<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, Pair<T, T2>>(
                new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(ref _collect, ref collect2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>, T2> ZipBoxSelect<TSelect, T2>(ICollectionWrapper<T2> collect2, TSelect @select) 
            where TSelect : struct, IFunctor<Pair<T, T2>, T2>
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            var zipOperator = new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(ref _collect, ref collect2);
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, TSelect, Pair<T, T2>, T2>(
                            ref zipOperator, ref select
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2> ZipBoxSelect<T2>(ICollectionWrapper<T2> collect2, Func<Pair<T, T2>, T2> @select)
        {
            var zipOperator = new ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>(ref _collect, ref collect2);
            var func = new FuncFunctor<Pair<T, T2>, T2>(select);
            return new OperatorWrapper<SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>, T2>(
                    new SelectOperator<ZipOperator<TCollect, T, ICollectionWrapper<T2>, T2>, FuncFunctor<Pair<T, T2>, T2>, Pair<T, T2>, T2>(
                           ref zipOperator, ref func
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T> Concat<TOther>(TOther other) 
            where TOther : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<ConcatOperator<TCollect, TOther, T>, T>(
                    new ConcatOperator<TCollect, TOther, T>(ref _collect, ref other)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty()
        {
            return new OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T>(
                    new DefaultIfEmptyOperator<TCollect, T>(ref _collect, default(T))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T> DefaultIfEmpty(T defaultVal)
        {
            return new OperatorWrapper<DefaultIfEmptyOperator<TCollect, T>, T>(
                    new DefaultIfEmptyOperator<TCollect, T>(ref _collect, defaultVal)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, TFactory>, T> DefaultIfEmpty<TFactory>(TFactory factory) 
            where TFactory : struct, IFunctor<T>
        {
            return new OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, TFactory>, T>(
                    new LazyDefaultIfEmptyOperator<TCollect, T, TFactory>(ref _collect, ref factory)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>, T> DefaultIfEmpty(Func<T> factory)
        {
            var func = new FuncFunctor<T>(factory);
            return new OperatorWrapper<LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>, T>(
                    new LazyDefaultIfEmptyOperator<TCollect, T, FuncFunctor<T>>(ref _collect, ref func)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ReverseOperator<TCollect, T>, T> Reverse()
        {
            return new OperatorWrapper<ReverseOperator<TCollect, T>, T>(
                    new ReverseOperator<TCollect, T>(ref _collect)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DistinctOperator<TCollect, T>, T> Distinct()
        {
            return new OperatorWrapper<DistinctOperator<TCollect, T>, T>(
                    new DistinctOperator<TCollect, T>(ref _collect, null)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DistinctOperator<TCollect, T>, T> Distinct(IEqualityComparer<T> comparer)
        {
            return new OperatorWrapper<DistinctOperator<TCollect, T>, T>(
                    new DistinctOperator<TCollect, T>(ref _collect, comparer)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T> DistinctRestartable()
        {
            return new OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T>(
                    new DistinctRestartableOperator<TCollect, T>(ref _collect, EqualityComparer<T>.Default)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T> DistinctRestartable(IEqualityComparer<T> comparer)
        {
            return new OperatorWrapper<DistinctRestartableOperator<TCollect, T>, T>(
                    new DistinctRestartableOperator<TCollect, T>(ref _collect, comparer)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T> Except<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T>(
                    new ExceptOperator<TCollect, TSecond, T>(ref _collect, ref second, EqualityComparer<T>.Default)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T> Except<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<ExceptOperator<TCollect, TSecond, T>, T>(
                    new ExceptOperator<TCollect, TSecond, T>(ref _collect, ref second, comparer)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T> Intersect<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T>(
                    new IntersectOperator<TCollect, TSecond, T>(ref _collect, ref second, EqualityComparer<T>.Default)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T> Intersect<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<IntersectOperator<TCollect, TSecond, T>, T>(
                    new IntersectOperator<TCollect, TSecond, T>(ref _collect, ref second, comparer)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T> IntersectRestartable<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T>(
                    new IntersectRestartableOperator<TCollect, TSecond, T>(ref _collect, ref second, EqualityComparer<T>.Default)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T> IntersectRestartable<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<IntersectRestartableOperator<TCollect, TSecond, T>, T>(
                    new IntersectRestartableOperator<TCollect, TSecond, T>(ref _collect, ref second, comparer)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T> Union<TSecond>(TSecond second) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T>(
                    new UnionOperator<TCollect, TSecond, T>(ref _collect, ref second, EqualityComparer<T>.Default)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T> Union<TSecond>(TSecond second, IEqualityComparer<T> comparer) 
            where TSecond : struct, ICollectionWrapper<T>
        {
            return new OperatorWrapper<UnionOperator<TCollect, TSecond, T>, T>(
                    new UnionOperator<TCollect, TSecond, T>(ref _collect, ref second, comparer)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> Join<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T2> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T2>, TRes> 
            where TComparer : IEqualityComparer<TKey>
        {
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> Join<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes) 
            where TCollect2 : ICollectionWrapper<T2> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T2>, TRes>
        {
            var hashComparer = default(CompareHashSetHelper<TKey>);
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> JoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T>, TRes> 
            where TComparer : IEqualityComparer<TKey>
        {
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> JoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes) 
            where TCollect2 : ICollectionWrapper<T> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, T>, TRes>
        {
            var hashComparer = default(CompareHashSetHelper<TKey>);
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes,
            IEqualityComparer<TKey> comparer) 
            where TCollect2 : ICollectionWrapper<T2>
        {
            ErrorUtil.NullCheck(comparer, "comparer");
            var funSelector = new FuncFunctor<T, TKey>(selector);
            var funcSelector2 = new FuncFunctor<T2, TKey>(selector2);
            var selectorResFunctor = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref funSelector, ref funcSelector2, ref selectorResFunctor, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TComparer, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TComparer, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T2> 
            where TComparer : IEqualityComparer<TKey>
        {
            var selectorFun = new FuncFunctor<T, TKey>(selector);
            var selectorFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectorResFun = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TComparer, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selectorFun, ref selectorFun2, ref selectorResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            Join<TCollect2, T2, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes) 
            where TCollect2 : ICollectionWrapper<T2>
        {
            var funSelector = new FuncFunctor<T, TKey>(selector);
            var funSelector2 = new FuncFunctor<T2, TKey>(selector2);
            var funResSelector = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            var hashComparer = default(CompareHashSetHelper<TKey>);
            
            return new OperatorWrapper<JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref funSelector, ref funSelector2, ref funResSelector, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> 
            JoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, IEqualityComparer<TKey> comparer)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            ErrorUtil.NullCheck(comparer, "comparer");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            
            return new OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, IEqualityComparer<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TCompare, TKey, TRes>, TRes> 
            JoinBox<T2, TCompare, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes, TCompare comparer) 
            where TCompare : IEqualityComparer<TKey>
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            
            return new OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TCompare, TKey, TRes>, TRes>(
                    new JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, TCompare, TKey, TRes>(
                            ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            JoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2, Func<Pair<T, T2>, TRes> selectorRes)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, T2>, TRes>(selectorRes);
            var comparer = default(CompareHashSetHelper<TKey>);
            
            return new OperatorWrapper<JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                new JoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, T2>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>(
                    ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> GroupJoin<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T2> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, GroupingArray<T2>>, TRes> 
            where TComparer : IEqualityComparer<TKey>
        {
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> GroupJoin<TCollect2, T2, TSelector, TSelector2, TSelectorRes, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes) 
            where TCollect2 : ICollectionWrapper<T2> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T2, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, GroupingArray<T2>>, TRes>
        {
            var hashComparer = default(CompareHashSetHelper<TKey>);
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T2, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes> GroupJoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(
            TCollect2 collect2, TSelector selector, TSelector2 selector2, TSelectorRes selectorRes, TComparer comparer) 
            where TCollect2 : ICollectionWrapper<T> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, GroupingArray<T>>, TRes> 
            where TComparer : IEqualityComparer<TKey>
        {
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> GroupJoinSameType<TCollect2, TSelector, TSelector2, TSelectorRes, TKey, TRes>(TCollect2 collect2,
            TSelector selector, TSelector2 selector2, TSelectorRes selectorRes) 
            where TCollect2 : ICollectionWrapper<T> 
            where TSelector : struct, IFunctor<T, TKey> 
            where TSelector2 : struct, IFunctor<T, TKey> 
            where TSelectorRes : struct, IFunctor<Pair<T, GroupingArray<T>>, TRes>
        {
            var hashComparer = default(CompareHashSetHelper<TKey>);
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T, TSelector, TSelector2, TSelectorRes, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selector, ref selector2, ref selectorRes, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> 
            GroupJoin<TCollect2, T2, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes, IEqualityComparer<TKey> comparer) 
            where TCollect2 : ICollectionWrapper<T2>
        {
            ErrorUtil.NullCheck(comparer, "comparer");
            var funSelector = new FuncFunctor<T, TKey>(selector);
            var funcSelector2 = new FuncFunctor<T2, TKey>(selector2);
            var selectorResFunctor = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref funSelector, ref funcSelector2, ref selectorResFunctor, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TComparer, TKey, TRes>, TRes> GroupJoin<TCollect2, T2, TComparer, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes, TComparer comparer) where TCollect2 : ICollectionWrapper<T2> where TComparer : IEqualityComparer<TKey>
        {
            var selectorFun = new FuncFunctor<T, TKey>(selector);
            var selectorFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectorResFun = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);
            
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TComparer, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TComparer, TKey, TRes>(
                            ref _collect, ref collect2, ref selectorFun, ref selectorFun2, ref selectorResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> 
            GroupJoin<TCollect2, T2, TKey, TRes>(TCollect2 collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes) where TCollect2 : ICollectionWrapper<T2>
        {
            var funSelector = new FuncFunctor<T, TKey>(selector);
            var funSelector2 = new FuncFunctor<T2, TKey>(selector2);
            var funResSelector = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);
            var hashComparer = default(CompareHashSetHelper<TKey>);

            return new OperatorWrapper<GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, TCollect2, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref funSelector, ref funSelector2, ref funResSelector, ref hashComparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes> GroupJoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes, IEqualityComparer<TKey> comparer)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            ErrorUtil.NullCheck(comparer, "comparer");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);
            
            return new OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, IEqualityComparer<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TCompare, TKey, TRes>, TRes> GroupJoinBox<T2, TCompare, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes, TCompare comparer) where TCompare : IEqualityComparer<TKey>
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);

            return new OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TCompare, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, TCompare, TKey, TRes>(
                            ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                        )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes> GroupJoinBox<T2, TKey, TRes>(ICollectionWrapper<T2> collect2, Func<T, TKey> selector, Func<T2, TKey> selector2,
            Func<Pair<T, GroupingArray<T2>>, TRes> selectorRes)
        {
            ErrorUtil.NullCheck(collect2, "collect2");
            var selectFun = new FuncFunctor<T, TKey>(selector);
            var selectFun2 = new FuncFunctor<T2, TKey>(selector2);
            var selectResFun = new FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>(selectorRes);
            var comparer = default(CompareHashSetHelper<TKey>);

            return new OperatorWrapper<GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>, TRes>(
                    new GroupJoinOperator<TCollect, T, ICollectionWrapper<T2>, T2, FuncFunctor<T, TKey>, FuncFunctor<T2, TKey>, FuncFunctor<Pair<T, GroupingArray<T2>>, TRes>, CompareHashSetHelper<TKey>, TKey, TRes>(
                            ref _collect, ref collect2, ref selectFun, ref selectFun2, ref selectResFun, ref comparer
                        )
                );
        }
        
        public CollectEnumeratorWrapper<TCollect, T> GetEnumerator()
        {
            return new CollectEnumeratorWrapper<TCollect, T>(_collect);
        }

        public IEnumerator<T> BuildBoxEnumerator()
        {
            return _collect.HasIndexOverhead
                ? (IEnumerator<T>) new CollectOverheadEnumeratorWrapper<TCollect, T>(_collect)
                : new CollectIndexEnumeratorWrapper<TCollect, T>(_collect);
        }

        #region To Another collect

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

        #endregion
    }
}