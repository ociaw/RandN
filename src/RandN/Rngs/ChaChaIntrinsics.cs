#if X86_INTRINSICS
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using RandN.Implementation;

/* References:
 * https://cr.yp.to/chacha/chacha-20080128.pdf
 * http://loup-vaillant.fr/tutorials/chacha20-design
 */

namespace RandN.Rngs
{
    /// <summary>
    /// The core ChaCha algorithm.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    internal readonly struct ChaChaIntrinsics : ISeekableBlockRngCore<UInt32, UInt64>
    {
        [FieldOffset(0)]
        private readonly ChaChaSoftware _software = null!;

        [FieldOffset(0)]
        private readonly ChaChaSse2 _sse2 = null!;

        [FieldOffset(0)]
        private readonly ChaChaAvx2 _avx2 = null!;

        private ChaChaIntrinsics(ChaChaSoftware software) => _software = software;

        private ChaChaIntrinsics(ChaChaSse2 sse2) => _sse2 = sse2;

        private ChaChaIntrinsics(ChaChaAvx2 avx2) => _avx2 = avx2;

        /// <summary>
        /// ChaCha's 64-bit block counter.
        /// </summary>
        public UInt64 BlockCounter
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (Avx2.IsSupported)
                    return _avx2.BlockCounter;
                if (Sse2.IsSupported)
                    return _sse2.BlockCounter;
                
                return _software.BlockCounter;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (Avx2.IsSupported)
                    _avx2.BlockCounter = value;
                else if (Sse2.IsSupported)
                    _sse2.BlockCounter = value;
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
                if (Avx2.IsSupported)
                    return _avx2.Stream;
                if (Sse2.IsSupported)
                    return _sse2.Stream;

                return _software.Stream;
            }
            set
            {
                if (Avx2.IsSupported)
                    _avx2.Stream = value;
                else if (Sse2.IsSupported)
                    _sse2.Stream = value;
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

            if (Avx2.IsSupported)
                return new ChaChaIntrinsics(ChaChaAvx2.Create(key, counter, stream, doubleRoundCount));
            if (Sse2.IsSupported)
                return new ChaChaIntrinsics(ChaChaSse2.Create(key, counter, stream, doubleRoundCount));

            return new ChaChaIntrinsics(ChaChaSoftware.Create(key, counter, stream, doubleRoundCount));
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Generate(Span<UInt32> results)
        {
            if (Avx2.IsSupported)
                _avx2.Generate(results);
            else if (Sse2.IsSupported)
                _sse2.Generate(results);
            else
                _software.Generate(results);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Regenerate(Span<UInt32> results)
        {
            if (Avx2.IsSupported)
                _avx2.Regenerate(results);
            else if (Sse2.IsSupported)
                _sse2.Regenerate(results);
            else
                _software.Regenerate(results);
        }
    }
}
#endif
