using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Running;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using JM.LinqFaster;
using LinqMeta.Extensions;
using LinqMeta.Functors;
using LinqMetaCore;
using LinqMetaCore.Intefaces;

namespace Benchmark
{
    [MemoryDiagnoser]
    public class Operators
    {
        private const int N = 100_000;
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
                _list.Add(rnd.Next(10));
            }
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
  
  /*      
        #region SelectSum

        [Benchmark]
        public long SelectWhereIndexTakeSumLinq() => _list.Select(i => (long)i).TakeWhile((l, i) => i < 20_000).Take(2000).Sum();
        
        [Benchmark]
        public long SelectWhereIndexTakeSumLinqMeta() => _list.MetaOperators().SelectMeta(i => (long)i).WhereIndexMeta(pair => pair.Index < 20_000).TakeMeta(2000).SumMeta();
        
        [Benchmark]
        public long SelectWhereIndexTakeSumStructFunctorLinqMeta() => _list.MetaOperators().SelectMeta<IntToLong, long>(default(IntToLong))
            .WhereIndexMeta(new TakeWhileIndexLessThan(20_000)).TakeMeta(2000).SumMeta();
        
        [Benchmark]
        public long SelectWhereIndexTakeSumLinqFaster() => _list.SelectF(i => (long)i).WhereF((l, i) => i < 20_000).TakeF(2000).SumF();

        #endregion
  */
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