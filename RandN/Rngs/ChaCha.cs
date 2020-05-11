using System;
using RandN.RngHelpers;

namespace RandN.Rngs
{
    /// <summary>
    /// The ChaCha algorithm as a random number generator.
    /// </summary>
    public sealed class ChaCha : IRng, ISeekableRng<ChaCha.Counter>, ICryptoRng
    {
        private const Int32 BLOCK_LENGTH = 16;
        private const Int32 KEY_LENGTH = 8;
        private static readonly Factory8 _factory8 = new Factory8();
        private static readonly Factory12 _factory12 = new Factory12();
        private static readonly Factory20 _factory20 = new Factory20();

        private readonly BlockBuffer32<ChaChaCore, UInt64> _blockBuffer;
        private readonly UInt32[] _buffer;

        private ChaCha(BlockBuffer32<ChaChaCore, UInt64> blockBuffer)
        {
            _blockBuffer = blockBuffer;
            _buffer = new UInt32[BLOCK_LENGTH];
        }

        /// <inheritdoc />
        public Counter Position
        {
            get => new Counter(_blockBuffer.BlockCounter, (UInt32)_blockBuffer.Index);
            set
            {
                _blockBuffer.BlockCounter = value.Block;
                _blockBuffer.Index = (Int32)value.Word;
            }
        }

        /// <summary>
        /// Creates a ChaCha20 rng using the given seed.
        /// </summary>
        /// <param name="seed">A seed containing the key and stream.</param>
        public static ChaCha Create(Seed seed) => Create(seed, 10);

        /// <summary>
        /// Creates a ChaCha20 rng using the given seed and number of double rounds.
        /// </summary>
        /// <param name="seed">A seed containing the key and stream.</param>
        /// <param name="doubleRounds">
        /// The number of double rounds to perform. Half the total number of rounds,
        /// ex. ChaCha20 has 10 double rounds and ChaCha8 has 4 double rounds.
        /// </param>
        public static ChaCha Create(Seed seed, UInt32 doubleRounds)
        {
            if (doubleRounds == 0)
                throw new ArgumentOutOfRangeException(nameof(doubleRounds));

            var key = seed.Key.Length != 0 ? seed.Key.Span : stackalloc UInt32[KEY_LENGTH];
            var core = ChaChaCore.Create(key, UInt64.MaxValue, seed.Stream, doubleRounds);
            var blockBuffer = new BlockBuffer32<ChaChaCore, UInt64>(core);
            return new ChaCha(blockBuffer);
        }

        /// <summary>
        /// Gets the ChaCha8 factory.
        /// </summary>
        public static Factory8 GetChaCha8Factory() => _factory8;

        /// <summary>
        /// Gets the ChaCha12 factory.
        /// </summary>
        public static Factory12 GetChaCha12Factory() => _factory12;

        /// <summary>
        /// Gets the ChaCha20 factory.
        /// </summary>
        public static Factory20 GetChaCha20Factory() => _factory20;

        /// <inheritdoc />
        public UInt32 NextUInt32() => _blockBuffer.NextUInt32();

        /// <inheritdoc />
        public UInt64 NextUInt64() => _blockBuffer.NextUInt64();

        /// <inheritdoc />
        public void Fill(Span<Byte> buffer) => _blockBuffer.Fill(buffer);

        /// <summary>
        /// Produces ChaCha8 RNGs and seeds.
        /// </summary>
        public sealed class Factory8 : IReproducibleRngFactory<ChaCha, Seed>
        {
            private const UInt32 DOUBLE_ROUNDS = 4;

            internal Factory8() { }

            /// <inheritdoc />
            public ChaCha Create(Seed seed) => ChaCha.Create(seed, DOUBLE_ROUNDS);

            /// <inheritdoc />
            public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                Span<UInt32> key = stackalloc UInt32[KEY_LENGTH];
                seedingRng.Fill(System.Runtime.InteropServices.MemoryMarshal.AsBytes(key));
                UInt64 stream = 0;

                return new Seed(key, stream);
            }
        }

        /// <summary>
        /// Produces ChaCha12 RNGs and seeds.
        /// </summary>
        public sealed class Factory12 : IReproducibleRngFactory<ChaCha, Seed>
        {
            private const UInt32 DOUBLE_ROUNDS = 6;

            internal Factory12() { }

            /// <inheritdoc />
            public ChaCha Create(Seed seed) => ChaCha.Create(seed, DOUBLE_ROUNDS);

            /// <inheritdoc />
            public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                Span<UInt32> key = stackalloc UInt32[KEY_LENGTH];
                seedingRng.Fill(System.Runtime.InteropServices.MemoryMarshal.AsBytes(key));
                UInt64 stream = 0;

                return new Seed(key, stream);
            }
        }

        /// <summary>
        /// Produces ChaCha20 RNGs and seeds.
        /// </summary>
        public sealed class Factory20 : IReproducibleRngFactory<ChaCha, Seed>
        {
            private const UInt32 DOUBLE_ROUNDS = 10;

            internal Factory20() { }

            /// <inheritdoc />
            public ChaCha Create(Seed seed) => ChaCha.Create(seed, DOUBLE_ROUNDS);

            /// <inheritdoc />
            public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                Span<UInt32> key = stackalloc UInt32[KEY_LENGTH];
                seedingRng.Fill(System.Runtime.InteropServices.MemoryMarshal.AsBytes(key));
                UInt64 stream = 0;

                return new Seed(key, stream);
            }
        }

        /// <summary>
        /// The seed for <see cref="ChaCha"/>. Consists of a key and a stream id.
        /// </summary>
        public readonly struct Seed
        {
            public Seed(ReadOnlySpan<UInt32> key) : this(key, 0) { }

            /// <param name="key">ChaCha's key. Must have a length of 8.</param>
            /// <param name="stream">ChaCha's 64-bit stream id.</param>
            public Seed(ReadOnlySpan<UInt32> key, UInt64 stream)
            {
                if (key.Length != KEY_LENGTH)
                    throw new ArgumentException("Key must have length of 8.", nameof(key));

                Key = key.ToArray();
                Stream = stream;
            }

            /// <summary>
            /// The 32 byte key for ChaCha20.
            /// </summary>
            public ReadOnlyMemory<UInt32> Key { get; }

            /// <summary>
            /// The stream id.
            /// </summary>
            public UInt64 Stream { get; }
        }

        /// <summary>
        /// ChaCha's 64-bit stream id.
        /// </summary>
        public readonly struct Counter
        {
            /// <param name="block">ChaCha's 64-bit block counter.</param>
            /// <param name="word">The individual word in the current block. Must be less than or equal to 16.</param>
            public Counter(UInt64 block, UInt32 word)
            {
                if (word > 16)
                    throw new ArgumentOutOfRangeException(nameof(word), word, "Must be less than or equal to 16.");

                Block = block;
                Word = word;
            }

            /// <summary>
            /// ChaCha's 64-bit block counter.
            /// </summary>
            public UInt64 Block { get; }

            /// <summary>
            /// The individual word in the current block. Must be less than or equal to 16.
            /// </summary>
            public UInt32 Word { get; }
        }
    }
}
