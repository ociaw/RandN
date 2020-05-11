using System;
using System.Diagnostics;
using RandN.RngHelpers;

/* References:
 * https://cr.yp.to/chacha/chacha-20080128.pdf
 * http://loup-vaillant.fr/tutorials/chacha20-design
 */

namespace RandN.Rngs
{
    /// <summary>
    /// The core ChaCha algorithm.
    /// </summary>
    internal sealed class ChaChaCore : ISeekableBlockRngCore<UInt32, UInt64>
    {
        private const Int32 CONSTANT_LENGTH = 4;
        private const Int32 KEY_LENGTH = 8;
        private const Int32 COUNTER_LENGTH = 2;
        private const Int32 STREAM_LENGTH = 2;
        private const Int32 WORD_COUNT = CONSTANT_LENGTH + KEY_LENGTH + COUNTER_LENGTH + STREAM_LENGTH;

        private static readonly UInt32[] _constant = new UInt32[]
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
        private readonly UInt32 DoubleRounds;

        private ChaChaCore(UInt32[] state, UInt32 doubleRounds)
        {
            _state = state;
            DoubleRounds = doubleRounds;
        }

        /// <summary>
        /// ChaCha's 64-bit block counter.
        /// </summary>
        public UInt64 BlockCounter
        {
            get => _state[13].CombineWithLow(_state[12]);
            set
            {
                _state[12] = value.IsolateLow();
                _state[13] = value.IsolateHigh();
            }
        }

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

        /// <inheritdoc />
        public Int32 BlockLength => WORD_COUNT;

        /// <summary>
        /// Creates a new instance of ChaChaCore.
        /// </summary>
        /// <param name="key">ChaCha20's key. Must have a length of 8.</param>
        /// <param name="counter">ChaCha's 64-bit block counter.</param>
        /// <param name="stream">ChaCha20's 64-bit stream id.</param>
        /// <param name="doubleRoundCount"></param>
        /// <returns></returns>
        public static ChaChaCore Create(ReadOnlySpan<UInt32> key, UInt64 counter, UInt64 stream, UInt32 doubleRoundCount)
        {
            Debug.Assert(key.Length == KEY_LENGTH);
            Debug.Assert(doubleRoundCount != 0);

            var state = new UInt32[WORD_COUNT];
            var stateSpan = state.AsSpan();

            _constant.CopyTo(stateSpan);
            stateSpan = stateSpan.Slice(CONSTANT_LENGTH);
            key.CopyTo(stateSpan);
            stateSpan = stateSpan.Slice(KEY_LENGTH);
            stateSpan[0] = counter.IsolateLow();
            stateSpan[1] = counter.IsolateHigh();
            stateSpan = stateSpan.Slice(COUNTER_LENGTH);
            stateSpan[0] = stream.IsolateLow();
            stateSpan[1] = stream.IsolateHigh();

            return new ChaChaCore(state, doubleRoundCount);
        }

        /// <inheritdoc />
        public void Generate(Span<UInt32> results)
        {
            // We wrap once we run out of data.
            BlockCounter = unchecked(BlockCounter + 1);
            FullBlock(results);
        }

        /// <inheritdoc />
        public void Regenerate(Span<UInt32> results) => FullBlock(results);

        /// <summary>
        /// Generates a full ChaCha block into <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The destination buffer. Must be at least <see cref="BlockLength" /> long./></param>
        private void FullBlock(Span<UInt32> destination)
        {
            if (destination.Length < WORD_COUNT)
                throw new ArgumentException($"Destination must have length of {WORD_COUNT}.", nameof(destination));

            Debug.Assert(DoubleRounds != 0);

            _state.CopyTo(destination);

            for (var i = 0; i < DoubleRounds; i++)
                InnerBlock(destination);

            unchecked
            {
                for (var i = 0; i < WORD_COUNT; i++)
                    destination[i] += _state[i];
            }
        }

        /// <summary>
        /// Performs <see cref="QuarterRound" /> on each column (1 round), then on each diagonal (1 more round) in <paramref name="state"/>.
        /// </summary>
        private static void InnerBlock(Span<UInt32> state)
        {
            // Columns
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
}
