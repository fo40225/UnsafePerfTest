using System;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace UnsafePerfTest
{
    public unsafe class UnsafeTest
    {
        static Random rnd = new Random();
        static Stopwatch sw = new Stopwatch();
        static int[] x = new int[1048576];

        [Params(10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = rnd.Next();
            }
        }

        [Benchmark]
        public void ManagedAccessArray()
        {
            var y = new int[1048576];
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[i];
            }
        }

        [Benchmark]
        public void UnsafeAccessArrayOffset()
        {
            var y = new int[1048576];
            fixed (int* ptrX = x, ptrY = y)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    ptrY[i] = ptrX[i];
                }
            }
        }

        [Benchmark]
        public void UnsafeAccessArrayOffsetWorkround()
        {
            var y = new int[1048576];
            fixed (int* ptrX = x, ptrY = y)
            {
                int* tx = ptrX;
                int* ty = ptrY;
                for (int i = 0; i < x.Length; i++)
                {
                    ty[i] = tx[i];
                }
            }
        }

        [Benchmark]
        public void UnsafeAccessArrayRawPointer()
        {
            var y = new int[1048576];
            fixed (int* ptrX = x, ptrY = y)
            {
                int* tx = ptrX;
                int* ty = ptrY;
                for (int i = 0; i < x.Length; i++, tx++, ty++)
                {
                    *ty = *tx;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var summary = BenchmarkRunner.Run<UnsafeTest>();
        }
    }
}
