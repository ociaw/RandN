using System;
using System.Diagnostics;
using RandN.Distributions.UnitInterval;

namespace RandN.Examples;

public sealed class Program
{
    private static void Main()
    {
        var rng = StandardRng.Create();

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

        Double error = 1 / Math.Sqrt(iterations) * 4;
        return ((Double)insideQuadrant / iterations * 4.0, error);
    }
}
