using System;
using BenchmarkDotNet.Running;

namespace RandN.Benchmarks
{
    class Program
    {
        static void Main(String[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
