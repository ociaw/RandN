using System;
using System.Collections.Generic;
using RandN.Distributions;

namespace RandN;

/// <summary>
/// Various extension methods to simplify use of RNGs.
/// </summary>
public static class RngExtensions
{
    /// <summary>
    /// Shuffles a list using the in-place Fisher-Yates shuffling algorithm.
    /// </summary>
    /// <param name="rng">The RNG used to shuffle the list.</param>
    /// <param name="list">The list to be shuffled.</param>
    public static void ShuffleInPlace<TRng, T>(this TRng rng, IList<T> list)
        where TRng : notnull, IRng
    {
        // Fisher-Yates shuffle
        for (Int32 i = list.Count - 1; i >= 1; i--)
        {
            var dist = Uniform.NewInclusive(0, i);
            var swapIndex = dist.Sample(rng);
            T temp = list[swapIndex];
            list[swapIndex] = list[i];
            list[i] = temp;
        }
    }

    /// <summary>
    /// Creates a new <typeparamref name="TRng"/> using a seed created from <paramref name="seedingRng"/>.
    /// </summary>
    /// <param name="factory">The factory creating the new RNG.</param>
    /// <param name="seedingRng">The RNG used to create the seed.</param>
    /// <returns>A new <typeparamref name="TRng"/> instance.</returns>
    public static TRng Create<TRng, TSeed, TSeedingRng>(this IReproducibleRngFactory<TRng, TSeed> factory, TSeedingRng seedingRng)
        where TRng : notnull, IRng
        where TSeedingRng : notnull, IRng
    {
        var seed = factory.CreateSeed(seedingRng);
        return factory.Create(seed);
    }
}
