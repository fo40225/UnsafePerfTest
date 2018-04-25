# UnsafePerfTest

https://github.com/dotnet/coreclr/issues/2594

https://github.com/dotnet/coreclr/issues/2480

This repo contains code that can reproduce the issue of csharp's unsafe pointer performene issue.

.NET Core

    BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1029 (1703/CreatorsUpdate/Redstone2)
    Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
    Frequency=3222678 Hz, Resolution=310.3009 ns, Timer=TSC
    .NET Core SDK=2.1.300-rc1-008662
      [Host]     : .NET Core 2.1.0-rc1-26423-06 (CoreCLR 4.6.26423.02, CoreFX 4.6.26423.06), 64bit RyuJIT
      DefaultJob : .NET Core 2.1.0-rc1-26423-06 (CoreCLR 4.6.26423.02, CoreFX 4.6.26423.06), 64bit RyuJIT
    
    
                                   Method |     N |     Mean |     Error |    StdDev |
    ------------------------------------- |------ |---------:|----------:|----------:|
                       ManagedAccessArray | 10000 | 1.585 ms | 0.0329 ms | 0.0352 ms |
                  UnsafeAccessArrayOffset | 10000 | 1.428 ms | 0.0277 ms | 0.0296 ms |
         UnsafeAccessArrayOffsetWorkround | 10000 | 1.165 ms | 0.0229 ms | 0.0215 ms |
              UnsafeAccessArrayRawPointer | 10000 | 1.166 ms | 0.0216 ms | 0.0191 ms |

.NET Framework

    BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1029 (1703/CreatorsUpdate/Redstone2)
    Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
    Frequency=3222678 Hz, Resolution=310.3009 ns, Timer=TSC
      [Host]     : .NET Framework 4.6.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2115.0
      DefaultJob : .NET Framework 4.6.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2115.0
    
    
                                   Method |     N |     Mean |     Error |    StdDev |   Median |
    ------------------------------------- |------ |---------:|----------:|----------:|---------:|
                       ManagedAccessArray | 10000 | 2.274 ms | 0.0175 ms | 0.0155 ms | 2.270 ms |
                  UnsafeAccessArrayOffset | 10000 | 2.198 ms | 0.0439 ms | 0.0916 ms | 2.159 ms |
         UnsafeAccessArrayOffsetWorkround | 10000 | 1.872 ms | 0.0253 ms | 0.0236 ms | 1.863 ms |
              UnsafeAccessArrayRawPointer | 10000 | 1.876 ms | 0.0132 ms | 0.0110 ms | 1.878 ms |
