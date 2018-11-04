using System;
using System.Collections.Generic;
using BenchmarkDotNet.Running;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using JM.LinqFaster;
using LinqMeta.CollectionWrapper;
using LinqMeta.Extensions;
using LinqMeta.Extensions.Operators.Aggregate;
using LinqMeta.Extensions.Operators.Min;
using LinqMeta.Extensions.Operators.Sum;
using LinqMeta.Functors;
using LinqMeta.Operators.Numbers;

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
                _list.Add(rnd.Next(2));
            }
        }
/*
        #region Sum

        [Benchmark]
        public int SumLinq() => _list.Sum();

        [Benchmark]
        public int SumLinqMeta() => _list.SumMeta();
        
        [Benchmark]
        public int SumLinqFaster() => _list.SumF();

        #endregion
        */
/*
        #region Min

        [Benchmark]
        public int MinLinq() => arr.Min();
        
        [Benchmark]
        public int MinLinqMeta() => arr.MinMeta();
        
        [Benchmark]
        public int MinLinqFaster() => arr.MinF();

        #endregion
*/
        #region Aggregate

        [Benchmark]
        public int AggregateLinq() => _list.Aggregate((i, i1) => i * i1);
        
        [Benchmark]
        public int AggregateLinqMeta() => _list.AggregateMeta((i, i1) => i * i1);
        
        [Benchmark]
        public int AggregateLinqMetaStructFunctor() => _list.AggregateMeta(default(To2Functor));
          
        [Benchmark]
        public int AggregateLinqFaster() => _list.AggregateF((i, i1) => i * i1);

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

    public struct To2Functor : IFunctor<int, int, int>
    {
        public int Invoke(int param, int param2)
        {
            return param * param2;
        }
    }
}