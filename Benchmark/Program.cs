using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using JM.LinqFaster;
using LinqMeta.DataTypes;
using LinqMeta.DataTypes.Groupin;
using LinqMeta.DataTypes.SetMeta;
using LinqMeta.Extensions;
using LinqMeta.Operators;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class Operators
    {
        private const int N = 10_000;
        //private int[] arr;
        private List<int> _list;
        
        public Operators()
        {   
            var rnd = new Random(1100);
            
           // arr = new int[N];
            _list = new List<int>(N);
            for (var i = 0; i < N; i++)
            {
                //arr[i] = rnd.Next(2);
                var item = rnd.Next(100);
                _list.Add(item);
            }
        }

        private int[] GetArr()
        {
            return new int[N];
        }

/*
        #region Sum

        [Benchmark]
        public int SumLinq() => _list.Sum();

        [Benchmark]
        public int SumLinqMeta() => _list.MetaOperators().SumMeta();
        
        [Benchmark]
        public int SumLinqFaster() => _list.SumF();

        #endregion
   */
        /*
        #region Min

        [Benchmark]
        public int MinLinq() => _list.Min();
        
        [Benchmark]
        public int MinLinqMeta() => _list.MetaOperators().MinMeta();
        
        [Benchmark]
        public int MinLinqFaster() => _list.MinF();

        #endregion
      */
        /*
        #region Aggregate

        [Benchmark]
        public int AggregateLinq() => _list.Aggregate((i, i1) => i * i1);
        
        [Benchmark]
        public int AggregateLinqMeta() => _list.MetaOperators().AggregateMeta((i, i1) => i * i1);
        
        [Benchmark]
        public int AggregateLinqMetaStructFunctor() => _list.MetaOperators().AggregateMeta(default(To2Functor));
          
        [Benchmark]
        public int AggregateLinqFaster() => _list.AggregateF((i, i1) => i * i1);

        #endregion
*/
     /*
        #region SelectSum

        [Benchmark]
        public double SelectSumLinq() => _list.Select(i => (double)i).Aggregate((i, i1) => i * i1);
        
        [Benchmark]
        public double SelectSumLinqMeta() => _list.MetaOperators().SelectMeta(i => (double)i).AggregateMeta((i, i1) => i * i1);
        
        [Benchmark]
        public double SelectSumLinqFaster() => _list.AggregateF((i, i1) => i * i1);

        #endregion
 */
    /*   
        #region SelectWhereSum

        [Benchmark]
        public long SelectWhereSumLinq() => _list.Select(i => (long)i).Where(l => l % 2 == 0).Sum();
        
        [Benchmark]
        public long SelectWhereSumLinqMeta() => _list.MetaOperators().Select(i => (long)i).Where(i => i % 2 == 0).Sum();
        
        [Benchmark]
        public long SelectWhereSumStructFunctorLinqMeta() => _list.MetaOperators().Select<IntToLong, long>(default(IntToLong))
            .Where(default(EvenFilter)).Sum();
        
        [Benchmark]
        public long SelectWhereSumLinqFaster() => _list.SelectF(i => (long)i).WhereSumF(l => l % 2 == 0);

        #endregion
  */

        /*
        #region SelectSum

        [Benchmark]
        public long SelectWhereIndexTakeSumLinq() => _list.Select(i => (long)i).TakeWhile((l, i) => i < 20_000).Sum();
       
        [Benchmark]
        public long SelectWhereIndexTakeSumLinqMeta() => _list.MetaOperators().Select(i => (long)i).WhereIndex(pair => pair.Index < 20_000).Sum();
        
        [Benchmark]
        public long SelectWhereIndexTakeSumStructFunctorLinqMeta() => _list.MetaOperators().Select<IntToLong, long>(default(IntToLong))
            .WhereIndex(new TakeWhileIndexLessThan(20_000)).Sum();
        
        [Benchmark]
        public long SelectWhereIndexTakeSumLinqFaster() => _list.SelectF(i => (long)i).WhereF((l, i) => i < 20_000).SumF();
        
        #endregion
        */

        
        #region GroupBy
        
        [Benchmark]
        public int GroupByMetaStruct()
        {
            var sum = 0;
            foreach (var key in _list.MetaOperators().GroupBy<IdentityOperator<int>, int>(new IdentityOperator<int>())
                .Select<FirstSelector<int, GroupingArray<int>>, int>(new FirstSelector<int, GroupingArray<int>>()))
            {
                sum += key;
            }

            return sum;
        }
        
        [Benchmark]
        public int GroupByMeta()
        {
            var sum = 0;
            foreach (var key in _list.MetaOperators().GroupBy(i => i).Select(pair => pair.First))
            {
                sum += key;
            }

            return sum;
        }
        
        [Benchmark]
        public int GroupByLinq()
        {
            var sum = 0;
            foreach (var key in _list.GroupBy(i => i).Select(ints => ints.Key))
            {
                sum += key;
            }

            return sum;
        }
        
        #endregion
        
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = 
                BenchmarkRunner.Run<Operators>(
                    ManualConfig.Create(DefaultConfig.Instance).With(Job.RyuJitX64)
                );
        }
    }

    internal struct FirstSelector<TF, TS> : IFunctor<Pair<TF, TS>, TF>
    {
        public TF Invoke(Pair<TF, TS> param)
        {
            return param.First;
        }
    }
    
    internal struct To2Functor : IFunctor<int, int, int>
    {
        public int Invoke(int param, int param2)
        {
            return param * param2;
        }
    }

    internal struct IntToDouble : IFunctor<int, double>
    {
        public double Invoke(int param)
        {
            return param;
        }
    }

    internal struct IntToLong : IFunctor<int, long>
    {
        public long Invoke(int param)
        {
            return param;
        }
    }

    internal struct EvenFilter : IFunctor<long, bool>
    {
        public bool Invoke(long param)
        {
            return param % 2 == 0;
        }
    }

    internal struct TakeWhileIndexLessThan : IFunctor<ZipPair<long>, bool>
    {
        private int _end;

        public TakeWhileIndexLessThan(int end)
        {
            _end = end;
        }
        
        public bool Invoke(ZipPair<long> param)
        {
            return param.Index < _end;
        }
    }
}