using System;
using System.Diagnostics;
using RandN.Distributions.UnitInterval;
using RandN.Rngs;

namespace RandN.Examples
{
    public sealed class Program
    {
        private static void Main()
        {
            var seed = new ChaCha.Seed(new UInt32[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var rng = ChaCha.Create(seed);

            var stopwatch = Stopwatch.StartNew();
            (Double pi, Double error) = EstimatePi(rng);
            stopwatch.Stop();
            Console.WriteLine($"Pi: {pi}, error: {error}, time (sec): {stopwatch.Elapsed.TotalSeconds}");
        }

        /// <summary>
        /// Estimates Pi using a Monte Carlo simulation.
        /// </summary>
        public static (Double pi, Double error) EstimatePi<TRng>(TRng rng) where TRng : IRng
        {
            const UInt64 iterations = 32_000_000;
            var dist = Closed.Double.Instance;
            UInt64 insideQuadrant = 0;
            for (var i = 0ul; i < iterations; i++)
            {
                var x = dist.Sample(rng);
                var y = dist.Sample(rng);
                var mag2 = x * x + y * y;
                if (mag2 <= 1.0)
                    insideQuadrant += 1;
            }

            Double error = 1 / Math.Sqrt(iterations);
            return ((Double)insideQuadrant / iterations * 4.0, error);
        }
    }
}
