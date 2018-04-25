# UnsafePerfTest

https://github.com/dotnet/coreclr/issues/2594

https://github.com/dotnet/coreclr/issues/2480

This repo contains code that can reproduce the issue of csharp's unsafe pointer performene issue.

.NET Core

    BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1029 (1703/CreatorsUpdate/Redstone2)
    Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
    Frequency=3222683 Hz, Resolution=310.3005 ns, Timer=TSC
    .NET Core SDK=2.1.300-rc1-008662
      [Host]    : .NET Core 2.1.0-rc1-26423-06 (CoreCLR 4.6.26423.02, CoreFX 4.6.26423.06), 64bit RyuJIT
      Clr       : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2558.0
      Core      : .NET Core 2.1.0-rc1-26423-06 (CoreCLR 4.6.26423.02, CoreFX 4.6.26423.06), 64bit RyuJIT
      Mono      : Mono 5.10.1 (Visual Studio), 64bit
    
    Platform=X64
    
                               Method |       Job | Runtime |     N |     Mean |     Error |    StdDev |
    --------------------------------- |---------- |-------- |------ |---------:|----------:|----------:|
                   ManagedAccessArray |       Clr |     Clr | 10000 | 2.308 ms | 0.0282 ms | 0.0220 ms |
              UnsafeAccessArrayOffset |       Clr |     Clr | 10000 | 2.263 ms | 0.0435 ms | 0.0406 ms |
     UnsafeAccessArrayOffsetWorkround |       Clr |     Clr | 10000 | 1.999 ms | 0.0233 ms | 0.0218 ms |
          UnsafeAccessArrayRawPointer |       Clr |     Clr | 10000 | 1.990 ms | 0.0360 ms | 0.0337 ms |
                   ManagedAccessArray |      Core |    Core | 10000 | 1.531 ms | 0.0230 ms | 0.0215 ms |
              UnsafeAccessArrayOffset |      Core |    Core | 10000 | 1.468 ms | 0.0395 ms | 0.0369 ms |
     UnsafeAccessArrayOffsetWorkround |      Core |    Core | 10000 | 1.168 ms | 0.0228 ms | 0.0263 ms |
          UnsafeAccessArrayRawPointer |      Core |    Core | 10000 | 1.170 ms | 0.0233 ms | 0.0249 ms |
                   ManagedAccessArray |      Mono |    Mono | 10000 | 2.933 ms | 0.0312 ms | 0.0291 ms |
              UnsafeAccessArrayOffset |      Mono |    Mono | 10000 | 2.866 ms | 0.0326 ms | 0.0305 ms |
     UnsafeAccessArrayOffsetWorkround |      Mono |    Mono | 10000 | 2.838 ms | 0.0160 ms | 0.0133 ms |
          UnsafeAccessArrayRawPointer |      Mono |    Mono | 10000 | 2.264 ms | 0.0150 ms | 0.0133 ms |

.NET Framework

    BenchmarkDotNet=v0.10.14, OS=Windows 10.0.15063.1029 (1703/CreatorsUpdate/Redstone2)
    Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
    Frequency=3222683 Hz, Resolution=310.3005 ns, Timer=TSC
      [Host]       : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2558.0
      Clr          : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.2558.0
      LegacyJitX64 : .NET Framework 4.7.1 (CLR 4.0.30319.42000), 64bit LegacyJIT/clrjit-v4.7.2558.0;compatjit-v4.7.2558.0
      Mono         : Mono 5.10.1 (Visual Studio), 64bit
    
    Platform=X64
    
                               Method |          Job |       Jit | Runtime |     N |     Mean |     Error |    StdDev |
    --------------------------------- |------------- |---------- |-------- |------ |---------:|----------:|----------:|
                   ManagedAccessArray |          Clr |    RyuJit |     Clr | 10000 | 2.375 ms | 0.0337 ms | 0.0315 ms |
              UnsafeAccessArrayOffset |          Clr |    RyuJit |     Clr | 10000 | 2.244 ms | 0.0353 ms | 0.0295 ms |
     UnsafeAccessArrayOffsetWorkround |          Clr |    RyuJit |     Clr | 10000 | 2.020 ms | 0.0312 ms | 0.0277 ms |
          UnsafeAccessArrayRawPointer |          Clr |    RyuJit |     Clr | 10000 | 1.985 ms | 0.0370 ms | 0.0363 ms |
                   ManagedAccessArray | LegacyJitX64 | LegacyJit |     Clr | 10000 | 2.228 ms | 0.0418 ms | 0.0370 ms |
              UnsafeAccessArrayOffset | LegacyJitX64 | LegacyJit |     Clr | 10000 | 2.325 ms | 0.0421 ms | 0.0394 ms |
     UnsafeAccessArrayOffsetWorkround | LegacyJitX64 | LegacyJit |     Clr | 10000 | 2.222 ms | 0.0433 ms | 0.0405 ms |
          UnsafeAccessArrayRawPointer | LegacyJitX64 | LegacyJit |     Clr | 10000 | 2.335 ms | 0.0465 ms | 0.0929 ms |
                   ManagedAccessArray |         Mono |    RyuJit |    Mono | 10000 | 2.682 ms | 0.0527 ms | 0.0586 ms |
              UnsafeAccessArrayOffset |         Mono |    RyuJit |    Mono | 10000 | 2.824 ms | 0.0547 ms | 0.0749 ms |
     UnsafeAccessArrayOffsetWorkround |         Mono |    RyuJit |    Mono | 10000 | 2.888 ms | 0.0572 ms | 0.0763 ms |
          UnsafeAccessArrayRawPointer |         Mono |    RyuJit |    Mono | 10000 | 2.357 ms | 0.0456 ms | 0.0448 ms |
