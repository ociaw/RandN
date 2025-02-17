using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        if (list is T[] array)
        {
            ShuffleInPlace(rng, array.AsSpan());
            return;
        }
#if NET6_0_OR_GREATER
        if (list is List<T> concrete)
        {
            ShuffleInPlace(rng, CollectionsMarshal.AsSpan(concrete));
            return;
        }
#endif
        // Fisher-Yates shuffle
        for (Int32 i = list.Count - 1; i >= 1; i--)
        {
            var dist = Uniform.NewInclusive(0, i);
            var swapIndex = dist.Sample(rng);
            (list[swapIndex], list[i]) = (list[i], list[swapIndex]);
        }
    }

    /// <summary>
    /// Shuffles a span using the in-place Fisher-Yates shuffling algorithm.
    /// </summary>
    /// <param name="rng">The RNG used to shuffle the list.</param>
    /// <param name="span">The span to be shuffled.</param>
    public static void ShuffleInPlace<TRng, T>(this TRng rng, Span<T> span)
        where TRng : notnull, IRng
    {
        ref var first = ref MemoryMarshal.GetReference(span);
        // Fisher-Yates shuffle
        for (Int32 i = span.Length - 1; i >= 1; i--)
        {
            var dist = Uniform.NewInclusive(0, i);
            var swapIndex = dist.Sample(rng);
            ref var right = ref Unsafe.Add(ref first, i);
            ref var left = ref Unsafe.Add(ref first, swapIndex);
            (right, left) = (left, right);
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
