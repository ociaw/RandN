using System;
using BenchmarkDotNet.Running;

namespace RandN.Benchmarks
{
    internal class Program
    {
        private static void Main(String[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
