using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
#if BITWISE_ROTATION
using System.Numerics;
#endif

namespace RandN.Implementation;

/// <summary>
/// Bitwise extensions to unsigned integer types.
/// </summary>
public static class BitwiseExtensions
{
    /// <summary>
    /// Rotates <paramref name="original"/> <paramref name="amount"/> to the left.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt32 RotateLeft(this UInt32 original, Int32 amount)
    {
#if BITWISE_ROTATION
        return BitOperations.RotateLeft(original, amount);
#else
        return (original << amount) | (original >> (32 - amount));
#endif
    }

    /// <summary>
    /// Rotates <paramref name="original"/> <paramref name="amount"/> to the right.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt32 RotateRight(this UInt32 original, Int32 amount)
    {
#if BITWISE_ROTATION
        return BitOperations.RotateRight(original, amount);
#else
        return (original >> amount) | (original << (32 - amount));
#endif
    }

    /// <summary>
    /// Isolates and returns the 32 high bits of <paramref name="original"/>.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt32 IsolateHigh(this UInt64 original) => (UInt32)(original >> 32);

    /// <summary>
    /// Isolates and returns the 32 low bits of <paramref name="original"/>.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt32 IsolateLow(this UInt64 original) => unchecked((UInt32)original);

    /// <summary>
    /// Combines <paramref name="high"/> and <paramref name="low"/> into a <see cref="UInt64"/>.
    /// </summary>
    /// <param name="high">The 32 high bits.</param>
    /// <param name="low">The 32 low bits.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64 CombineWithLow(this UInt32 high, UInt32 low) => ((UInt64)high << 32) | low;

#if NET8_0_OR_GREATER
    /// <summary>
    /// Isolates and returns the 64 high bits of <paramref name="original"/>.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64 IsolateHigh(this UInt128 original) => (UInt64)(original >> 64);

    /// <summary>
    /// Isolates and returns the 64 low bits of <paramref name="original"/>.
    /// </summary>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt64 IsolateLow(this UInt128 original) => unchecked((UInt64)original);

    /// <summary>
    /// Combines <paramref name="high"/> and <paramref name="low"/> into a <see cref="UInt128"/>.
    /// </summary>
    /// <param name="high">The 64 high bits.</param>
    /// <param name="low">The 64 low bits.</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 CombineWithLow(this UInt64 high, UInt64 low) => new(high, low);
#endif
}
