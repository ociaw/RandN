Title: RNG Benchmarks (v0.2)
Published: 2021-06-07
Author: ociaw
Category: benchmarks
---

Some quick benchmarks of the RNG algorithms currently implemented for v0.2.

### System Information

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1500 (1909/November2018Update/19H2)
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.201
  [Host]          : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  .NET Framework  : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT
  .NET 5, no AVX2 : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
  .NET 5          : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT
```

### Results

|             RngUInt64 | .NET Framework | .NET 5, no AVX2 |        .NET 5 |
|---------------------- |---------------:|----------------:|--------------:|
|               ChaCha8 |    187.30 MB/s |     970.70 MB/s | 1,285.57 MB/s |
|              ChaCha12 |    147.38 MB/s |     775.92 MB/s | 1,025.21 MB/s |
|              ChaCha20 |    101.88 MB/s |     550.32 MB/s |   817.92 MB/s |
|            mt19937-64 |    736.41 MB/s |     782.77 MB/s |   829.18 MB/s |
|                 PCG32 |  1,224.33 MB/s |   1,412.73 MB/s | 1,411.52 MB/s |
|              XorShift |  1,333.21 MB/s |   1,464.17 MB/s | 1,411.20 MB/s |
| CryptoServiceProvider |    395.25 MB/s |     763.68 MB/s |   800.41 MB/s |
|         System.Random |    218.05 MB/s |     198.68 MB/s |   198.34 MB/s |

|             RngUInt32 | .NET Framework | .NET 5, no AVX2 |        .NET 5 |
|---------------------- |---------------:|----------------:|--------------:|
|               ChaCha8 |    224.02 MB/s |     978.46 MB/s | 1,245.75 MB/s |
|              ChaCha12 |    168.06 MB/s |     780.89 MB/s | 1,046.13 MB/s |
|              ChaCha20 |    111.17 MB/s |     555.50 MB/s |   798.83 MB/s |
|            mt19937-64 |    340.66 MB/s |     332.02 MB/s |   345.08 MB/s |
|                 PCG32 |  2,312.50 MB/s |   2,411.55 MB/s | 2,401.94 MB/s |
|              XorShift |  2,280.48 MB/s |   2,509.52 MB/s | 2,511.10 MB/s |
| CryptoServiceProvider |    626.10 MB/s |     706.68 MB/s |   716.98 MB/s |
|         System.Random |    216.27 MB/s |     192.77 MB/s |   196.86 MB/s |

|               RngFill | .NET Framework | .NET 5, no AVX2 |        .NET 5 |
|---------------------- |---------------:|----------------:|--------------:|
|               ChaCha8 |    260.09 MB/s |   1,780.26 MB/s | 2,850.54 MB/s |
|              ChaCha12 |    183.75 MB/s |   1,210.77 MB/s | 1,991.67 MB/s |
|              ChaCha20 |    118.57 MB/s |     740.51 MB/s | 1,257.29 MB/s |
|            mt19937-64 |    519.36 MB/s |     694.89 MB/s |   696.68 MB/s |
|                 PCG32 |    716.96 MB/s |   1,067.74 MB/s | 1,129.33 MB/s |
|              XorShift |    727.54 MB/s |   1,101.26 MB/s | 1,163.73 MB/s |
| CryptoServiceProvider |  1,652.05 MB/s | 2,310.89\* MB/s | 2,291.79 MB/s |
|         System.Random |    175.45 MB/s |     132.04 MB/s |   137.85 MB/s |

\* Since CryptoServiceProvider relies on an external CSPRNG, disabling AVX2 for .NET likely has no effect.