#if NET6_0_OR_GREATER
using System;

namespace RandN.Distributions;

/// <summary>
/// Utilities for 128 bit distributions.
/// </summary>
internal static class Utils
{
    
#if NET8_0_OR_GREATER
    /// <summary>
    /// Multiplies two <see cref="System.UInt128"/>s and returns the result split into upper and lower bits.
    /// </summary>
    /// <param name="left">The left multiplicand.</param>
    /// <param name="right">The right multiplicand.</param>
    /// <returns>A tuple of the high bits and the low bits of the multiplication result.</returns>
    public static (UInt128 hi, UInt128 lo) WideningMultiply(this UInt128 left, UInt128 right)
    {
        UInt128 lowerMask = new UInt128(0, UInt64.MaxValue);

        UInt128 low = unchecked((left & lowerMask) * (right & lowerMask));
        UInt128 t = low >> 64;
        low &= lowerMask;
        t += unchecked((left >> 64) * (right & lowerMask));
        low += (t & lowerMask) << 64;
        UInt128 high = t >> 64;
        t = low >> 64;
        low &= lowerMask;
        t += unchecked((right >> 64) * (left & lowerMask));
        low += (t & lowerMask) << 64;
        high += t >> 64;
        high += unchecked((left >> 64) * (right >> 64));

        return (high, low);
    }

    /// <summary>
    /// Returns the next 128 bits in the RNG as a <see cref="System.UInt128"/>.
    /// </summary>
    public static UInt128 NextUInt128<TRng>(this TRng rng) where TRng : notnull, IRng
    {
        // Use Little Endian; we explicitly generate one value before the next.
        var x = rng.NextUInt64();
        var y = rng.NextUInt64();
        return new UInt128(y, x);
    }
#endif
}
#endif
