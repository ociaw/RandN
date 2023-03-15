#if X86_INTRINSICS && !NET7_0_OR_GREATER
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using RandN.Implementation;

namespace RandN.Rngs;

/// <summary>
/// An implementation of ChaCha using SSE2 instructions.
/// </summary>
internal sealed class ChaChaVec128 : ISeekableBlockRngCore<UInt32, UInt64>
{
    private static readonly Vector128<UInt32> Constant;

    static ChaChaVec128()
    {
        Constant = Vector128.Create(
            0x61707865u, // "expa"
            0x3320646Eu, // "nd 3"
            0x79622D32u, // "2-by"
            0x6B206574u  // "te k"
        );
    }

    private readonly Vector128<UInt32> _key1;
    private readonly Vector128<UInt32> _key2;

    private readonly UInt32 _doubleRounds;

    private ChaChaVec128(Vector128<UInt32> key1, Vector128<UInt32> key2, UInt32 doubleRounds)
    {
        _key1 = key1;
        _key2 = key2;
        _doubleRounds = doubleRounds;
    }

    /// <summary>
    /// ChaCha's 64-bit block counter.
    /// </summary>
    public UInt64 BlockCounter { get; set; }

    /// <summary>
    /// ChaCha's 64-bit stream id.
    /// </summary>
    public UInt64 Stream { get; set; }

        /// <inheritdoc />
        public Int32 BlockLength => ChaCha.BufferLength;

    /// <summary>
    /// Creates a new instance of <see cref="ChaChaSoftware"/>.
    /// </summary>
    /// <param name="key">ChaCha20's key. Must have a length of 8.</param>
    /// <param name="counter">ChaCha's 64-bit block counter.</param>
    /// <param name="stream">ChaCha20's 64-bit stream id.</param>
    /// <param name="doubleRoundCount">
    /// The number of double rounds to perform. Half the total number of rounds,
    /// ex. ChaCha20 has 10 double rounds and ChaCha8 has 4 double rounds.
    /// </param>
    public static ChaChaVec128 Create(ReadOnlySpan<UInt32> key, UInt64 counter, UInt64 stream, UInt32 doubleRoundCount)
    {
        Debug.Assert(key.Length == ChaCha.KeyLength);
        Debug.Assert(doubleRoundCount != 0);

        var key1 = Vector128.Create(key[0], key[1], key[2], key[3]);
        var key2 = Vector128.Create(key[4], key[5], key[6], key[7]);

        return new ChaChaVec128(key1, key2, doubleRoundCount) { BlockCounter = counter, Stream = stream };
    }

    /// <inheritdoc />
    public void Generate(Span<UInt32> results)
    {
        // We wrap once we run out of data.
        BlockCounter = unchecked(BlockCounter + 1);
        QuadBlock(results);
    }

    /// <inheritdoc />
    public void Regenerate(Span<UInt32> results) => QuadBlock(results);

    private void QuadBlock(Span<UInt32> results)
    {
        Debug.Assert(results.Length == ChaCha.BufferLength);

        unchecked
        {
            var startCounter = BlockCounter << 2;
            FullBlock(results, startCounter);
            FullBlock(results[ChaCha.WordCount..], startCounter + 1ul);
            FullBlock(results[(ChaCha.WordCount * 2)..], startCounter + 2ul);
            FullBlock(results[(ChaCha.WordCount * 3)..], startCounter + 3ul);
        }
    }

    /// <summary>
    /// Generates a full ChaCha block into <paramref name="destination"/>.
    /// </summary>
    /// <param name="destination">The destination buffer. Must be at least <see cref="ChaCha.WordCount" /> long.</param>
    /// <param name="counter">The counter used in the nonce.</param>
    private void FullBlock(Span<UInt32> destination, UInt64 counter)
    {
        Debug.Assert(destination.Length >= ChaCha.WordCount);
        Debug.Assert(_doubleRounds != 0);

        var input = Vector128.Create(counter, Stream).AsUInt32();

        var b0 = Constant;
        var b1 = _key1;
        var b2 = _key2;
        var b3 = input;

        for (var i = 0; i < _doubleRounds; i++)
            InnerBlock(ref b0, ref b1, ref b2, ref b3);

        var out0 = Sse2.Add(Constant, b0);
        var out1 = Sse2.Add(_key1, b1);
        var out2 = Sse2.Add(_key2, b2);
        var out3 = Sse2.Add(input, b3);

        unsafe
        {
            fixed (UInt32* ptr = destination)
            {
                Sse2.Store(ptr, out0);
                Sse2.Store(ptr + 4, out1);
                Sse2.Store(ptr + 8, out2);
                Sse2.Store(ptr + 12, out3);
            }
        }
    }

    /// <summary>
    /// Performs a quarter round on each column (1 round), then on each diagonal (1 more round).
    /// </summary>
    private static void InnerBlock(ref Vector128<UInt32> b0, ref Vector128<UInt32> b1, ref Vector128<UInt32> b2, ref Vector128<UInt32> b3)
    {
        ColumnRound(ref b0, ref b1, ref b2, ref b3);

        // Rotate diagonals to columns
        Diagonalize(ref b1, ref b2, ref b3);

        ColumnRound(ref b0, ref b1, ref b2, ref b3);

        // Rotate columns back to diagonals
        Undiagonalize(ref b1, ref b2, ref b3);
    }

    /// <summary>
    /// Performs ChaCha's QuarterRound on the parameters.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ColumnRound(ref Vector128<UInt32> a, ref Vector128<UInt32> b, ref Vector128<UInt32> c, ref Vector128<UInt32> d)
    {
        a = Sse2.Add(a, b);
        d = Sse2.Xor(d, a);
        d = RotateBitsLeft(d, 16);

        c = Sse2.Add(c, d);
        b = Sse2.Xor(b, c);
        b = RotateBitsLeft(b, 12);

        a = Sse2.Add(a, b);
        d = Sse2.Xor(d, a);
        d = RotateBitsLeft(d, 8);

        c = Sse2.Add(c, d);
        b = Sse2.Xor(b, c);
        b = RotateBitsLeft(b, 7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<UInt32> RotateBitsLeft(Vector128<UInt32> vector, Byte amount)
    {
        return Sse2.Or(
            Sse2.ShiftLeftLogical(vector, amount),
            Sse2.ShiftRightLogical(vector, (Byte)(32 - amount))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Diagonalize(ref Vector128<UInt32> b, ref Vector128<UInt32> c, ref Vector128<UInt32> d)
    {
        b = Shuffle3012(b);
        c = Shuffle2301(c);
        d = Shuffle1230(d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Undiagonalize(ref Vector128<UInt32> b, ref Vector128<UInt32> c, ref Vector128<UInt32> d)
    {
        b = Shuffle1230(b);
        c = Shuffle2301(c);
        d = Shuffle3012(d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<UInt32> Shuffle2301(Vector128<UInt32> vector) => Sse2.Shuffle(vector, 0b0100_1110);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<UInt32> Shuffle1230(Vector128<UInt32> vector) => Sse2.Shuffle(vector, 0b1001_0011);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<UInt32> Shuffle3012(Vector128<UInt32> vector) => Sse2.Shuffle(vector, 0b0011_1001);
}
#endif
