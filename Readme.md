# LinqMeta

LinqMeta has: Sum, Max, Min, Aggregate. Work in progress. 

Benchmark compare: Linq, LinqMeta, LinqFaster
#### Aggregate performance list size 100_000:

Method |        Mean |      Error |     StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
--------------------------------------------- |------------:|-----------:|-----------:|------------:|------------:|------------:|--------------------:|
SumLinq |   659.84 us |  3.8764 us |  3.4363 us |           - |           - |           - |                48 B |
SumLinqMeta |   105.22 us |  0.3351 us |  0.3134 us |           - |           - |           - |                   - |
SumLinqFaster |    89.76 us |  0.6498 us |  0.6078 us |           - |           - |           - |                   - |
MinLinq |   655.10 us |  8.3769 us |  7.8358 us |           - |           - |           - |                48 B |
MinLinqMeta |   125.14 us |  0.8147 us |  0.7620 us |           - |           - |           - |                   - |
MinLinqFaster |    90.19 us |  0.8311 us |  0.7774 us |           - |           - |           - |                   - |
AggregateLinq |   921.80 us |  0.7994 us |  0.7087 us |           - |           - |           - |                48 B |
AggregateLinqMeta |   328.51 us |  3.7235 us |  3.4830 us |           - |           - |           - |                   - |
AggregateLinqMetaStructFunctor |    89.78 us |  0.9132 us |  0.8542 us |           - |           - |           - |                   - |
AggregateLinqFaster |   240.63 us |  2.9102 us |  2.7222 us |           - |           - |           - |                   - |
SelectSumLinq | 1,240.44 us |  6.7700 us |  6.3326 us |           - |           - |           - |                96 B |
SelectSumLinqMeta |   635.29 us |  5.6441 us |  5.0033 us |           - |           - |           - |                   - |
SelectSumLinqFaster |   212.15 us |  2.1601 us |  1.8038 us |           - |           - |           - |                   - |
SelectWhereSumLinq | 1,724.61 us | 20.0574 us | 16.7489 us |           - |           - |           - |               144 B |
SelectWhereSumLinqMeta | 1,120.72 us |  6.6346 us |  6.2060 us |           - |           - |           - |                   - |
SelectWhereSumStructFunctorLinqMeta |   851.96 us |  8.9757 us |  8.3959 us |           - |           - |           - |                   - |
SelectWhereSumLinqFaster | 1,345.08 us | 10.4522 us |  9.7770 us |    248.0469 |    248.0469 |    248.0469 |            802072 B |
SelectWhereIndexTakeSumLinq |    43.24 us |  0.4492 us |  0.4202 us |      0.0610 |           - |           - |               224 B |
SelectWhereIndexTakeSumLinqMeta |    23.51 us |  0.2081 us |  0.1946 us |           - |           - |           - |                   - |
SelectWhereIndexTakeSumStructFunctorLinqMeta |    17.50 us |  0.0933 us |  0.0872 us |           - |           - |           - |                   - |
SelectWhereIndexTakeSumLinqFaster |   914.04 us |  4.9388 us |  4.6198 us |    350.5859 |    300.7813 |    293.9453 |           1344552 B |

#### Implement methods:

Operation, like XXXDefault may replace. because XXX operators return special type Option, then may return default, value or exception

| Method          | Linq | LinqMeta |
|-----------------|------|----------|
| Select          | +    | +        |
| SelectMany      | +    | -        |
| Where      | +    | +        |
| GroupJoin      | +    | -        |
| Join      | +    | -        |
| All      | +    | +        |
| Any      | +    | +        |
| Contains      | +    | -        |
| Concat      | +    | +        |
| DefaultIfEmpty      | +    | -        |
| Distinct      | +    | -        |
| Except      | +    | -        |
| Intersect      | +    | -        |
| Union      | +    | -        |
| OrderBy      | +    | -        |
| OrderByDescending	     | +    | -        |
| ThenBy      | +    | -        |
| ThenByDescending      | +    | -        |
| Reverse     | +    | -        |
| GroupBy      | +    | -        |
| Aggregate	      | +    | +        |
| Max	      | +    | +        |
| Min	      | +    | +        |
| Sum	      | +    | +        |
| MaxMin	      | -    | +        |
| Cast	      | +    | +        |
| OfType	      | +    | +        |
| UnsafeCast	      | -    | +        |
| Average      | +    | -        |
| Count      | +    | -        |
| LongCount	      | +    | -        |
| Statistic      | -    | +        |
| ElementAt      | +    | +       |
| ElementAtOrDefault	      | +    | -        |
| First      | +    | +        |
| FirstOrDefault      | +    | -        |
| Last      | +    | -        |
| LastOrDefault      | +    | -        |
| Skip      | +    | +        |
| SkipWhile      | +    | +        |
| Take      | +    | +        |
| TakeWhile      | +    | +        |
| Zip      | +    | +        |
| ToArray      | +    | +        |
| ToList      | +    | +        |
| ToDictionary      | +    | -        |
| ToHashSet      | +    | -        |