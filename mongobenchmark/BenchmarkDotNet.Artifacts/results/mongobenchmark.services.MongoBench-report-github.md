```

BenchmarkDotNet v0.14.0, Ubuntu 20.04.6 LTS (Focal Fossa) (container)
Intel Xeon Platinum 8370C CPU 2.80GHz, 1 CPU, 2 logical cores and 1 physical core
.NET SDK 8.0.407
  [Host]     : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                        | Mean     | Error   | StdDev   | Median   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------------------------------ |---------:|--------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| MongoDriver_GetByObjectId     | 375.2 μs | 7.50 μs | 18.96 μs | 368.5 μs |  1.00 |    0.07 | 0.9766 |  26.36 KB |        1.00 |
| MongoDBEntities_GetByObjectId |       NA |      NA |       NA |       NA |     ? |       ? |     NA |        NA |           ? |
| MongoFramework_GetByObjectId  |       NA |      NA |       NA |       NA |     ? |       ? |     NA |        NA |           ? |

Benchmarks with issues:
  MongoBench.MongoDBEntities_GetByObjectId: DefaultJob
  MongoBench.MongoFramework_GetByObjectId: DefaultJob
