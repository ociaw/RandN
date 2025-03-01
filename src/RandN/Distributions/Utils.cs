// ReSharper disable twice RedundantUsingDirective
using System;
using System.Numerics;

namespace RandN.Distributions;

/// <summary>
/// Utilities for distributions.
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
#elif !NET6_0_OR_GREATER
    /// <summary>
    /// Gets the number of bits required to represent the positive, unsigned value of this integer.
    /// </summary>
    /// <remarks>GetBitLength isn't available in .NET Standard, so this is a backport of sorts of the method </remarks>
    public static UInt64 GetBitLength(this BigInteger bigInt)
    {
#if NETSTANDARD2_0
        bigInt = BigInteger.Abs(bigInt);
        // Unfortunately we have to allocate a new array for this since we don't have any way to get the underlying byte
        // array without allocating in .NET Standard 2.0
        var bytes = bigInt.ToByteArray();
#endif
#if NETSTANDARD2_1
        var byteCount = bigInt.GetByteCount(true);
        Span<Byte> bytes = byteCount > 1024 ? new Byte[byteCount] : stackalloc Byte[byteCount];
        bigInt.TryWriteBytes(bytes, out _, isUnsigned: true, isBigEndian: false);
#endif
        return (UInt64)bytes.Length * 8 - CountLeadingZeros(bytes[bytes.Length - 1]);
    }
    
    private static UInt64 CountLeadingZeros(Byte input)
    {
        // Adapted from https://stackoverflow.com/a/31377558
        if (input == 0) return 8;

        Byte n = 1;
        if (input >> 4 == 0) { n = (Byte)(n + 4); input = (Byte)(input << 4); }
        if (input >> 6 == 0) { n = (Byte)(n + 2); input = (Byte)(input << 2); }
        n = (Byte)(n - (input >> 7));

        return n;
    }
#endif
}

