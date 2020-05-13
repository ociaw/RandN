using BenchmarkDotNet.Running;

namespace RandN.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Rngs>();
        }
    }
}
