# LinqMeta

LinqMeta has: Sum, Max, Min, Aggregate. Work in progress. 

Benchmark compare: Linq, LinqMeta, LinqFaster
#### Aggregate performance list size 100_000:

 Method |      Mean |      Error |     StdDev |    Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
------------------------------- |----------:|-----------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
                  AggregateLinq | 848.19 us | 15.4095 us | 14.4141 us | 840.83 us |           - |           - |           - |                48 B |
              AggregateLinqMeta | 290.26 us |  0.6335 us |  0.5616 us | 290.03 us |           - |           - |           - |                   - |
 AggregateLinqMetaStructFunctor |  85.11 us |  1.6928 us |  2.5851 us |  87.06 us |           - |           - |           - |                   - |
            AggregateLinqFaster | 228.74 us |  4.4476 us |  6.7920 us | 232.95 us |           - |           - |           - |                   - |
