using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using RandN.Implementation;

/* References:
 * https://cr.yp.to/chacha/chacha-20080128.pdf
 * http://loup-vaillant.fr/tutorials/chacha20-design
 */

namespace RandN.Rngs;

/// <summary>
/// The core ChaCha algorithm.
/// </summary>
internal sealed class ChaChaSoftware : ISeekableBlockRngCore<UInt32, UInt64>
{
    private static readonly UInt32[] Constant =
    {
        // "expa"
        0x61707865u,
        // "nd 3"
        0x3320646Eu,
        // "2-by"
        0x79622D32u,
        // "te k"
        0x6B206574u,
    };

    private readonly UInt32[] _state;
    private readonly UInt32 _doubleRounds;

    private ChaChaSoftware(UInt32[] state, UInt32 doubleRounds)
    {
        _state = state;
        _doubleRounds = doubleRounds;
    }

    /// <summary>
    /// ChaCha's 64-bit block counter.
    /// </summary>
    public UInt64 BlockCounter { get; set; }

    /// <summary>
    /// ChaCha's 64-bit stream id.
    /// </summary>
    public UInt64 Stream
    {
        get => _state[15].CombineWithLow(_state[14]);
        set
        {
            _state[14] = value.IsolateLow();
            _state[15] = value.IsolateHigh();
        }
    }

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
    public static ChaChaSoftware Create(ReadOnlySpan<UInt32> key, UInt64 counter, UInt64 stream, UInt32 doubleRoundCount)
    {
        Debug.Assert(key.Length == ChaCha.KeyLength);
        Debug.Assert(doubleRoundCount != 0);

        var state = new UInt32[ChaCha.WordCount];
        var stateSpan = state.AsSpan();

        Constant.CopyTo(stateSpan);
        stateSpan = stateSpan.Slice(ChaCha.ConstantLength);
        key.CopyTo(stateSpan);

        return new ChaChaSoftware(state, doubleRoundCount) { BlockCounter = counter, Stream = stream };
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
            FullBlock(results.Slice(ChaCha.WordCount), startCounter + 1ul);
            FullBlock(results.Slice(ChaCha.WordCount * 2), startCounter + 2ul);
            FullBlock(results.Slice(ChaCha.WordCount * 3), startCounter + 3ul);
        }
    }

    /// <summary>
    /// Generates a full ChaCha block into <paramref name="destination"/>.
    /// </summary>
    /// <param name="destination">The destination buffer. Must be at least <see cref="ChaCha.WordCount" /> long.</param>
    /// <param name="counter">The counter used in the nonce.</param>
    private void FullBlock(Span<UInt32> destination, UInt64 counter)
    {
        Debug.Assert(_doubleRounds != 0);
        Debug.Assert(destination.Length >= ChaCha.WordCount);

        _state[12] = counter.IsolateLow();
        _state[13] = counter.IsolateHigh();

        _state.CopyTo(destination);

        destination[12] = counter.IsolateLow();
        destination[13] = counter.IsolateHigh();

        for (var i = 0; i < _doubleRounds; i++)
            InnerBlock(destination);

        unchecked
        {
            for (var i = 0; i < ChaCha.WordCount; i++)
                destination[i] += _state[i];
        }
    }

    /// <summary>
    /// Performs <see cref="QuarterRound" /> on each column (1 round), then on each diagonal (1 more round) in <paramref name="state"/>.
    /// </summary>
    private static void InnerBlock(Span<UInt32> state)
    {
        QuarterRound(ref state[0], ref state[4], ref state[8], ref state[12]);
        QuarterRound(ref state[1], ref state[5], ref state[9], ref state[13]);
        QuarterRound(ref state[2], ref state[6], ref state[10], ref state[14]);
        QuarterRound(ref state[3], ref state[7], ref state[11], ref state[15]);

        // Diagonals
        QuarterRound(ref state[0], ref state[5], ref state[10], ref state[15]);
        QuarterRound(ref state[1], ref state[6], ref state[11], ref state[12]);
        QuarterRound(ref state[2], ref state[7], ref state[8], ref state[13]);
        QuarterRound(ref state[3], ref state[4], ref state[9], ref state[14]);
    }

    /// <summary>
    /// Performs ChaCha's QuarterRound on the parameters.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void QuarterRound(ref UInt32 a, ref UInt32 b, ref UInt32 c, ref UInt32 d)
    {
        unchecked
        {
            a += b;
            d ^= a;
            d = d.RotateLeft(16);

            c += d;
            b ^= c;
            b = b.RotateLeft(12);

            a += b;
            d ^= a;
            d = d.RotateLeft(8);

            c += d;
            b ^= c;
            b = b.RotateLeft(7);
        }
    }
}