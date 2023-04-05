#if NET6_0_OR_GREATER
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if NET7_0_OR_GREATER
using System.Runtime.Intrinsics;
#else
using System.Runtime.Intrinsics.X86;
#endif
using RandN.Implementation;

/* References:
 * https://cr.yp.to/chacha/chacha-20080128.pdf
 * http://loup-vaillant.fr/tutorials/chacha20-design
 */

namespace RandN.Rngs;
/// <summary>
/// The core ChaCha algorithm.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
internal readonly struct ChaChaIntrinsics : ISeekableBlockRngCore<UInt32, UInt64>
{
    [FieldOffset(0)]
    private readonly ChaChaSoftware _software = null!;

    [FieldOffset(0)]
    private readonly ChaChaVec128 _vec128 = null!;

    [FieldOffset(0)]
    private readonly ChaChaVec256 _vec256 = null!;

    private ChaChaIntrinsics(ChaChaSoftware software) => _software = software;

    private ChaChaIntrinsics(ChaChaVec128 vec128) => _vec128 = vec128;

    private ChaChaIntrinsics(ChaChaVec256 vec256) => _vec256 = vec256;

    /// <summary>
    /// ChaCha's 64-bit block counter.
    /// </summary>
    public UInt64 BlockCounter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
#if NET7_0_OR_GREATER
            if (Vector256.IsHardwareAccelerated)
                return _vec256.BlockCounter;
            if (Vector128.IsHardwareAccelerated)
                return _vec128.BlockCounter;
#else
            if (Avx2.IsSupported)
                return _vec256.BlockCounter;
            if (Sse2.IsSupported)
                return _vec128.BlockCounter;
#endif
            return _software.BlockCounter;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
#if NET7_0_OR_GREATER
            if (Vector256.IsHardwareAccelerated)
                _vec256.BlockCounter = value;
            else if (Vector128.IsHardwareAccelerated)
                _vec128.BlockCounter = value;
#else
            if (Avx2.IsSupported)
                _vec256.BlockCounter = value;
            else if (Sse2.IsSupported)
                _vec128.BlockCounter = value;
#endif
            else
                _software.BlockCounter = value;
        }
    }

    /// <summary>
    /// ChaCha's 64-bit stream id.
    /// </summary>
    public UInt64 Stream
    {
        get
        {
#if NET7_0_OR_GREATER
            if (Vector256.IsHardwareAccelerated)
                return _vec256.Stream;
            if (Vector128.IsHardwareAccelerated)
                return _vec128.Stream;
#else
            if (Avx2.IsSupported)
                return _vec256.Stream;
            if (Sse2.IsSupported)
                return _vec128.Stream;
#endif
            return _software.Stream;
        }
        set
        {
#if NET7_0_OR_GREATER
            if (Vector256.IsHardwareAccelerated)
                _vec256.Stream = value;
            else if (Vector128.IsHardwareAccelerated)
                _vec128.Stream = value;
#else
            if (Avx2.IsSupported)
                _vec256.Stream = value;
            else if (Sse2.IsSupported)
                _vec128.Stream = value;
#endif
            else
                _software.Stream = value;
        }
    }

    /// <inheritdoc />
    public Int32 BlockLength => ChaCha.BufferLength;

    /// <summary>
    /// Creates a new instance of <see cref="ChaChaIntrinsics"/>.
    /// </summary>
    /// <param name="key">ChaCha20's key. Must have a length of 8.</param>
    /// <param name="counter">ChaCha's 64-bit block counter.</param>
    /// <param name="stream">ChaCha20's 64-bit stream id.</param>
    /// <param name="doubleRoundCount">
    /// The number of double rounds to perform. Half the total number of rounds,
    /// ex. ChaCha20 has 10 double rounds and ChaCha8 has 4 double rounds.
    /// </param>
    public static ChaChaIntrinsics Create(ReadOnlySpan<UInt32> key, UInt64 counter, UInt64 stream, UInt32 doubleRoundCount)
    {
        Debug.Assert(key.Length == 8);
        Debug.Assert(doubleRoundCount != 0);

#if NET7_0_OR_GREATER
        if (Vector256.IsHardwareAccelerated)
            return new ChaChaIntrinsics(ChaChaVec256.Create(key, counter, stream, doubleRoundCount));
        if (Vector128.IsHardwareAccelerated)
            return new ChaChaIntrinsics(ChaChaVec128.Create(key, counter, stream, doubleRoundCount));
#else
        if (Avx2.IsSupported)
            return new ChaChaIntrinsics(ChaChaVec256.Create(key, counter, stream, doubleRoundCount));
        if (Sse2.IsSupported)
            return new ChaChaIntrinsics(ChaChaVec128.Create(key, counter, stream, doubleRoundCount));
#endif
        return new ChaChaIntrinsics(ChaChaSoftware.Create(key, counter, stream, doubleRoundCount));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Generate(Span<UInt32> results)
    {
#if NET7_0_OR_GREATER
        if (Vector256.IsHardwareAccelerated)
            _vec256.Generate(results);
        else if (Vector128.IsHardwareAccelerated)
            _vec128.Generate(results);
#else
        if (Avx2.IsSupported)
            _vec256.Generate(results);
        else if (Sse2.IsSupported)
            _vec128.Generate(results);
#endif
        else
            _software.Generate(results);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Regenerate(Span<UInt32> results)
    {
#if NET7_0_OR_GREATER
        if (Vector256.IsHardwareAccelerated)
            _vec256.Regenerate(results);
        else if (Vector128.IsHardwareAccelerated)
            _vec128.Regenerate(results);
#else
        if (Avx2.IsSupported)
            _vec256.Regenerate(results);
        else if (Sse2.IsSupported)
            _vec128.Regenerate(results);
#endif
        else
            _software.Regenerate(results);
    }
}
#endif
