using System;

namespace RandN.Implementation
{
    /// <summary>
    /// Assists in implementing block based RNGs.
    /// </summary>
    public sealed class BlockBuffer32<TBlockRng> : IRng
        where TBlockRng : notnull, IBlockRngCore<UInt32>
    {
        private readonly TBlockRng _rng;
        private readonly UInt32[] _results;

        /// <summary>
        /// Constructs a new instance filled from <paramref name="rng"/>.
        /// </summary>
        public BlockBuffer32(TBlockRng rng)
        {
            _rng = rng;
            _results = new UInt32[rng.BlockLength];
            Index = rng.BlockLength;
        }

        /// <summary>
        /// The length of the block.
        /// </summary>
        public Int32 BlockLength => _rng.BlockLength;

        /// <summary>
        /// The index to the location within the current block.
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// Generates and stores the next block and sets <see cref="Index"/> to <paramref name="index"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="index"/> is greater than or equal to <see cref="Index"/>.</exception>
        public void GenerateAndSet(Int32 index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be positive.");
            if (index >= BlockLength)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be less than {nameof(BlockLength)}.");

            _rng.Generate(_results);
            Index = index;
        }

        /// <inheritdoc />
        public UInt32 NextUInt32()
        {
            if (Index >= _rng.BlockLength)
                GenerateAndSet(0);

            UInt32 value = _results[Index];
            Index += 1;
            return value;
        }

        /// <inheritdoc />
        public UInt64 NextUInt64()
        {
            static UInt64 readUInt64(ReadOnlySpan<UInt32> results) => ((UInt64)results[1] << 32) | results[0];

            Span<UInt32> span = _results;
            Int32 length = _rng.BlockLength;
            if (Index < length - 1)
            {
                // Sufficient words left in the block, so read the first 2.
                Index += 2;
                return readUInt64(span.Slice(Index - 2));
            }

            if (Index >= length)
            {
                // No more words left in the block, so regenerate it, then read 2 from the beginning.
                GenerateAndSet(2);
                return readUInt64(span);
            }

            // One word left, read it, regenerate the block, then read the first word from that.
            UInt64 low = _results[length - 1];
            GenerateAndSet(1);
            UInt64 high = _results[0];
            return (high << 32) | low;
        }

        /// <inheritdoc />
        public void Fill(Span<Byte> dest)
        {
            while (dest.Length > 0)
            {
                if (Index >= _rng.BlockLength)
                    GenerateAndSet(0);

                var (consumedUInt32, filledBytes) = Filler.FillViaUInt32Chunks(_results.AsSpan(Index), dest);
                Index += consumedUInt32;
                dest = dest.Slice(filledBytes);
            }
        }
    }

    /// <summary>
    /// Assists in implementing seekable block based RNGs.
    /// </summary>
    public sealed class BlockBuffer32<TBlockRng, TBlockCounter> : IRng
        where TBlockRng : ISeekableBlockRngCore<UInt32, TBlockCounter>
        where TBlockCounter : IEquatable<TBlockCounter>
    {
        private readonly TBlockRng _rng;
        private readonly UInt32[] _results;

        /// <summary>
        /// Constructs a new instance filled from <paramref name="rng"/>.
        /// </summary>
        public BlockBuffer32(TBlockRng rng)
        {
            _rng = rng;
            _results = new UInt32[rng.BlockLength];
            Index = rng.BlockLength;
            _rng.Regenerate(_results);
        }

        /// <summary>
        /// The current block counter for the RNG.
        /// </summary>
        public TBlockCounter BlockCounter
        {
            get => _rng.BlockCounter;
            set
            {
                if (_rng.BlockCounter.Equals(value))
                    return;

                _rng.BlockCounter = value;
                _rng.Regenerate(_results);
            }
        }

        /// <summary>
        /// The length of the generated block.
        /// </summary>
        public Int32 BlockLength => _rng.BlockLength;

        /// <summary>
        /// The index to the location within the current block.
        /// </summary>
        public Int32 Index { get; set; }

        /// <summary>
        /// Generates and stores the next block and sets <see cref="Index"/> to <paramref name="index"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if <paramref name="index"/> is non-positive,
        /// or is greater than or equal to the block length.
        /// </exception>
        public void GenerateAndSet(Int32 index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be positive.");
            if (index >= BlockLength)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index must be less than {nameof(BlockLength)}.");

            _rng.Generate(_results);
            Index = index;
        }

        /// <inheritdoc />
        public UInt32 NextUInt32()
        {
            if (Index >= _rng.BlockLength)
                GenerateAndSet(0);

            UInt32 value = _results[Index];
            Index += 1;
            return value;
        }

        /// <inheritdoc />
        public UInt64 NextUInt64()
        {
            static UInt64 readUInt64(ReadOnlySpan<UInt32> results) => ((UInt64)results[1] << 32) | results[0];

            Span<UInt32> span = _results;
            Int32 length = _rng.BlockLength;
            if (Index < length - 1)
            {
                // Sufficient words left in the block, so read the first 2.
                Index += 2;
                return readUInt64(span.Slice(Index - 2));
            }

            if (Index >= length)
            {
                // No more words left in the block, so regenerate it, then read 2 from the beginning.
                GenerateAndSet(2);
                return readUInt64(span);
            }

            // One word left, read it, regenerate the block, then read the first word from that.
            UInt64 low = _results[length - 1];
            GenerateAndSet(1);
            UInt64 high = _results[0];
            return (high << 32) | low;
        }

        /// <inheritdoc />
        public void Fill(Span<Byte> dest)
        {
            while (dest.Length > 0)
            {
                if (Index >= _rng.BlockLength)
                    GenerateAndSet(0);

                var (consumedUInt32, filledBytes) = Filler.FillViaUInt32Chunks(_results.AsSpan(Index), dest);
                Index += consumedUInt32;
                dest = dest.Slice(filledBytes);
            }
        }
    }
}
