using System;
using System.Runtime.CompilerServices;
using RandN.Implementation;

namespace RandN.Rngs;

/// <summary>
/// A cryptographically secure random number generator using the ChaCha algorithm.
/// </summary>
public sealed class ChaCha : ISeekableRng<ChaCha.Counter>, ICryptoRng
{
    internal const Int32 ConstantLength = 4;
    internal const Int32 KeyLength = 8;
    internal const Int32 CounterLength = 2;
    internal const Int32 StreamLength = 2;
    internal const Int32 WordCount = ConstantLength + KeyLength + CounterLength + StreamLength;
    internal const Int32 BufferLength = WordCount * 4;

#if X86_INTRINSICS
        private readonly BlockBuffer32<ChaChaIntrinsics, UInt64> _blockBuffer;

        private ChaCha(BlockBuffer32<ChaChaIntrinsics, UInt64> blockBuffer)
#else
    private readonly BlockBuffer32<ChaChaSoftware, UInt64> _blockBuffer;

    private ChaCha(BlockBuffer32<ChaChaSoftware, UInt64> blockBuffer)
#endif
    {
        _blockBuffer = blockBuffer;
    }

    /// <inheritdoc />
    public Counter Position
    {
        // The block buffer actually buffers 4 full blocks, but we still want to let the user
        // address each individual block normally. To accomplish this, we move the lower two
        // bits of the counter to the word index, so that it becomes 6 bits.
        get
        {
            var block = unchecked((_blockBuffer.BlockCounter << 2) + (UInt64)(_blockBuffer.Index >> 4));
            var word = (UInt32)(_blockBuffer.Index & 0b1111);
            return new Counter(block, word);
        }
        set
        {
            _blockBuffer.BlockCounter = value.Block >> 2;
            _blockBuffer.Index = (Int32)(value.Word + ((value.Block & 0b11) << 4));
        }
    }

    /// <summary>
    /// Creates a ChaCha20 rng using the given seed.
    /// </summary>
    /// <param name="seed">A seed containing the key and stream.</param>
    public static ChaCha Create(Seed seed) => Create(seed, 10);

    /// <inheritdoc cref="Factory8.CreateSeed{TSeedingRng}" />
    public static Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng
    {
        Span<UInt32> key = stackalloc UInt32[KeyLength];
        seedingRng.Fill(System.Runtime.InteropServices.MemoryMarshal.AsBytes(key));
        const UInt64 stream = 0;

        return new Seed(key, stream);
    }

    /// <summary>
    /// Creates a ChaCha RNG using the given seed and number of double rounds.
    /// </summary>
    /// <param name="seed">A seed containing the key and stream.</param>
    /// <param name="doubleRounds">
    /// The number of double rounds to perform. Half the total number of rounds,
    /// ex. ChaCha20 has 10 double rounds and ChaCha8 has 4 double rounds.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="doubleRounds"/> is equal to 0.
    /// </exception>
    public static ChaCha Create(Seed seed, UInt32 doubleRounds)
    {
        if (doubleRounds == 0)
            throw new ArgumentOutOfRangeException(nameof(doubleRounds));

        var key = seed.Key.Length != 0 ? seed.Key.Span : stackalloc UInt32[KeyLength];
#if X86_INTRINSICS
            var core = ChaChaIntrinsics.Create(key, UInt64.MaxValue, seed.Stream, doubleRounds);
            var blockBuffer = new BlockBuffer32<ChaChaIntrinsics, UInt64>(core);
#else
        var core = ChaChaSoftware.Create(key, UInt64.MaxValue, seed.Stream, doubleRounds);
        var blockBuffer = new BlockBuffer32<ChaChaSoftware, UInt64>(core);
#endif
        return new ChaCha(blockBuffer);
    }

    /// <summary>
    /// Gets the ChaCha8 factory.
    /// </summary>
    public static Factory8 GetChaCha8Factory() => new();

    /// <summary>
    /// Gets the ChaCha12 factory.
    /// </summary>
    public static Factory12 GetChaCha12Factory() => new();

    /// <summary>
    /// Gets the ChaCha20 factory.
    /// </summary>
    public static Factory20 GetChaCha20Factory() => new();

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt32 NextUInt32() => _blockBuffer.NextUInt32();

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64 NextUInt64() => _blockBuffer.NextUInt64();

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Fill(Span<Byte> buffer) => _blockBuffer.Fill(buffer);

    /// <summary>
    /// Produces ChaCha8 RNGs and seeds.
    /// </summary>
    public readonly struct Factory8 : IReproducibleRngFactory<ChaCha, Seed>
    {
        private const UInt32 DoubleRounds = 4;

        /// <inheritdoc />
        public ChaCha Create(Seed seed) => ChaCha.Create(seed, DoubleRounds);

        /// <inheritdoc />
        public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng =>
            ChaCha.CreateSeed(seedingRng);
    }

    /// <summary>
    /// Produces ChaCha12 RNGs and seeds.
    /// </summary>
    public readonly struct Factory12 : IReproducibleRngFactory<ChaCha, Seed>
    {
        private const UInt32 DoubleRounds = 6;

        /// <inheritdoc />
        public ChaCha Create(Seed seed) => ChaCha.Create(seed, DoubleRounds);

        /// <inheritdoc />
        public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng =>
            ChaCha.CreateSeed(seedingRng);
    }

    /// <summary>
    /// Produces ChaCha20 RNGs and seeds.
    /// </summary>
    public readonly struct Factory20 : IReproducibleRngFactory<ChaCha, Seed>
    {
        private const UInt32 DoubleRounds = 10;

        /// <inheritdoc />
        public ChaCha Create(Seed seed) => ChaCha.Create(seed, DoubleRounds);

        /// <inheritdoc />
        public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng =>
            ChaCha.CreateSeed(seedingRng);
    }

    /// <summary>
    /// The seed for <see cref="ChaCha"/>. Consists of a key and a stream id.
    /// </summary>
    public readonly struct Seed
    {
        /// <summary>
        /// Constructs a new ChaCha seed from a key with a stream id of 0.
        /// </summary>
        /// <param name="key">ChaCha's key. Must have a length of 8.</param>
        public Seed(ReadOnlySpan<UInt32> key) : this(key, 0) { }

        /// <summary>
        /// Constructs a new ChaCha seed from a key and a stream id.
        /// </summary>
        /// <param name="key">ChaCha's key. Must have a length of 8.</param>
        /// <param name="stream">ChaCha's 64-bit stream id.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the length of <paramref name="key"/> is not equal to 8.
        /// </exception>
        public Seed(ReadOnlySpan<UInt32> key, UInt64 stream)
        {
            if (key.Length != KeyLength)
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
        /// <exception cref="ArgumentException">
        /// Thrown when the length of <paramref name="block"/> is not less than or equal to 16.
        /// </exception>
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
