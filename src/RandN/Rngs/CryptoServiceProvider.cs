using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using RandN.Implementation;

namespace RandN.Rngs
{
    /// <summary>
    /// A cryptographically secure random number generator wrapping <see cref="RNGCryptoServiceProvider"/>.
    /// </summary>
    public sealed class CryptoServiceProvider : IRng, ICryptoRng, IDisposable
    {
        private readonly BlockBuffer32<BlockCore> _buffer;
        private readonly RNGCryptoServiceProvider _rng;

        private CryptoServiceProvider(RNGCryptoServiceProvider rng)
        {
            _buffer = new BlockBuffer32<BlockCore>(new BlockCore(rng));
            _rng = rng;
        }

        /// <summary>
        /// Creates a new instance of <see cref="CryptoServiceProvider"/>.
        /// </summary>
        public static CryptoServiceProvider Create() => new CryptoServiceProvider(new RNGCryptoServiceProvider());

        /// <summary>
        /// Returns a singleton instance of <see cref="CryptoServiceProvider.Factory"/>.
        /// </summary>
        public static Factory GetFactory() => new Factory();

        /// <inheritdoc />
        public UInt32 NextUInt32() => _buffer.NextUInt32();

        /// <inheritdoc />
        public UInt64 NextUInt64() => _buffer.NextUInt64();

        /// <inheritdoc />
        public void Fill(Span<Byte> buffer)
        {
            // Only use the block buffer if it's longer than the destination.
            // Otherwise, it's more efficient to fill the destination directly.
            if (buffer.Length < _buffer.BlockLength)
            {
                _buffer.Fill(buffer);
                return;
            }

#if NETSTANDARD2_0
            var tmp = new Byte[buffer.Length];
            _rng.GetBytes(tmp);
            tmp.CopyTo(buffer);
#else
            _rng.GetBytes(buffer);
#endif
        }

        /// <inheritdoc />
        public void Dispose() => _rng.Dispose();

        /// <inheritdoc cref="IRngFactory{CryptoServiceProvider}" />
        public readonly struct Factory : IRngFactory<CryptoServiceProvider>
        {
            /// <inheritdoc />
            public CryptoServiceProvider Create() => CryptoServiceProvider.Create();
        }

        private sealed class BlockCore : IBlockRngCore<UInt32>
        {
            private readonly RNGCryptoServiceProvider _rng;

            public BlockCore(RNGCryptoServiceProvider rng) => _rng = rng;

            /// <remarks>
            /// Block length is flexible - the larger the block, the less often the buffer needs to
            /// be refilled, so we get higher throughput. This has diminishing returns though, and
            /// the larger the block, the more memory usage. 128 (512 bytes) is chosen as a decent
            /// balance - about twice as fast as 32, but after 256, it starts dropping off
            /// significantly.
            /// </remarks>
            public Int32 BlockLength => 128;

            public void Generate(Span<UInt32> results)
            {
                Span<Byte> span = MemoryMarshal.AsBytes(results);
#if NETSTANDARD2_0
                var tmp = new Byte[span.Length];
                _rng.GetBytes(tmp);
                tmp.CopyTo(span);
#else
                _rng.GetBytes(span);
#endif
            }
        }
    }
}
