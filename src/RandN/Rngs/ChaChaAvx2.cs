#if X86_INTRINSICS
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace RandN.Rngs;
internal sealed class ChaChaAvx2
{
    private static readonly Vector256<UInt32> Constant;

    static ChaChaAvx2()
    {
            Constant = Vector256.Create(
            0x61707865u, // "expa"
            0x3320646Eu, // "nd 3"
            0x79622D32u, // "2-by"
            0x6B206574u,  // "te k"

            0x61707865u, // "expa"
            0x3320646Eu, // "nd 3"
            0x79622D32u, // "2-by"
            0x6B206574u  // "te k"
        );
    }

    private readonly Vector256<UInt32> _key1;
    private readonly Vector256<UInt32> _key2;

    private readonly UInt32 _doubleRounds;

    private ChaChaAvx2(Vector256<UInt32> key1, Vector256<UInt32> key2, UInt32 doubleRounds)
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
    public static ChaChaAvx2 Create(ReadOnlySpan<UInt32> key, UInt64 counter, UInt64 stream, UInt32 doubleRoundCount)
    {
        Debug.Assert(key.Length == ChaCha.KeyLength);
        Debug.Assert(doubleRoundCount != 0);

        var key1 = Vector256.Create(key[0], key[1], key[2], key[3], key[0], key[1], key[2], key[3]);
        var key2 = Vector256.Create(key[4], key[5], key[6], key[7], key[4], key[5], key[6], key[7]);

        return new ChaChaAvx2(key1, key2, doubleRoundCount) { BlockCounter = counter, Stream = stream };
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
            DoubleBlock(results, startCounter);
            DoubleBlock(results[(ChaCha.WordCount * 2)..], startCounter + 2ul);
        }
    }

    /// <summary>
    /// Generates two full ChaCha blocks into <paramref name="destination"/>.
    /// </summary>
    /// <param name="destination">The destination buffer. Must be at least 2 * <see cref="ChaCha.WordCount" /> long.</param>
    /// <param name="counter">The counter used in the nonce.</param>
    private void DoubleBlock(Span<UInt32> destination, UInt64 counter)
    {
        Debug.Assert(_doubleRounds != 0);
        Debug.Assert(destination.Length >= ChaCha.WordCount * 2);

        var input = Vector256.Create(counter, Stream, counter + 1, Stream).AsUInt32();

        var b0 = Constant;
        var b1 = _key1;
        var b2 = _key2;
        var b3 = input;

        for (var i = 0; i < _doubleRounds; i++)
            InnerBlock(ref b0, ref b1, ref b2, ref b3);

        var out0 = Avx2.Add(Constant, b0);
        var out1 = Avx2.Add(_key1, b1);
        var out2 = Avx2.Add(_key2, b2);
        var out3 = Avx2.Add(input, b3);

        unsafe
        {
            fixed (UInt32* ptr = destination)
            {
                Sse2.Store(ptr + 0, out0.GetLower());
                Sse2.Store(ptr + 4, out1.GetLower());
                Sse2.Store(ptr + 8, out2.GetLower());
                Sse2.Store(ptr + 12, out3.GetLower());

                Sse2.Store(ptr + 16, out0.GetUpper());
                Sse2.Store(ptr + 20, out1.GetUpper());
                Sse2.Store(ptr + 24, out2.GetUpper());
                Sse2.Store(ptr + 28, out3.GetUpper());
            }
        }
    }

    /// <summary>
    /// Performs a quarter round on each column (1 round), then on each diagonal (1 more round).
    /// </summary>
    private static void InnerBlock(ref Vector256<UInt32> b0, ref Vector256<UInt32> b1, ref Vector256<UInt32> b2, ref Vector256<UInt32> b3)
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
    private static void ColumnRound(ref Vector256<UInt32> a, ref Vector256<UInt32> b, ref Vector256<UInt32> c, ref Vector256<UInt32> d)
    {
        a = Avx2.Add(a, b);
        d = Avx2.Xor(d, a);
        d = RotateBitsLeft(d, 16);

        c = Avx2.Add(c, d);
        b = Avx2.Xor(b, c);
        b = RotateBitsLeft(b, 12);

        a = Avx2.Add(a, b);
        d = Avx2.Xor(d, a);
        d = RotateBitsLeft(d, 8);

        c = Avx2.Add(c, d);
        b = Avx2.Xor(b, c);
        b = RotateBitsLeft(b, 7);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<UInt32> RotateBitsLeft(Vector256<UInt32> vector, Byte amount)
    {
        return Avx2.Or(
            Avx2.ShiftLeftLogical(vector, amount),
            Avx2.ShiftRightLogical(vector, (Byte)(32 - amount))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Diagonalize(ref Vector256<UInt32> b, ref Vector256<UInt32> c, ref Vector256<UInt32> d)
    {
        b = Shuffle3012(b);
        c = Shuffle2301(c);
        d = Shuffle1230(d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Undiagonalize(ref Vector256<UInt32> b, ref Vector256<UInt32> c, ref Vector256<UInt32> d)
    {
        b = Shuffle1230(b);
        c = Shuffle2301(c);
        d = Shuffle3012(d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<UInt32> Shuffle2301(Vector256<UInt32> vector) => Avx2.Shuffle(vector, 0b0100_1110);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<UInt32> Shuffle1230(Vector256<UInt32> vector) => Avx2.Shuffle(vector, 0b1001_0011);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<UInt32> Shuffle3012(Vector256<UInt32> vector) => Avx2.Shuffle(vector, 0b0011_1001);
}
#endif
