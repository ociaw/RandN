using System;
using System.Security.Cryptography;

namespace RandN.Compat
{
    /// <summary>
    /// A shim able to wrap any <see cref="IRng"/> as a <see cref="RandomNumberGenerator"/>.
    /// </summary>
    public static class RandomNumberGeneratorShim
    {
        /// <summary>
        /// Creates a new <see cref="RandomNumberGenerator"/> wrapper over <paramref name="rng"/>.
        /// </summary>
        /// <param name="rng">A cryptographically secure RNG.</param>
        public static RandomNumberGeneratorShim<TRng> Create<TRng>(TRng rng)
            where TRng : ICryptoRng
        {
            return new RandomNumberGeneratorShim<TRng>(rng);
        }
    }

    /// <summary>
    /// A shim able to wrap any <see cref="IRng"/> as a <see cref="RandomNumberGenerator"/>.
    /// </summary>
    public sealed class RandomNumberGeneratorShim<TRng> : RandomNumberGenerator, IDisposable
        where TRng : ICryptoRng
    {
        private readonly TRng _rng;

        /// <summary>
        /// Constructs a new <see cref="RandomNumberGenerator"/> wrapper over <paramref name="rng"/>.
        /// </summary>
        /// /// <param name="rng">A cryptographically secure RNG.</param>
        public RandomNumberGeneratorShim(TRng rng) => _rng = rng;

        /// <inheritdoc />
        public override void GetBytes(Byte[] data)
        {
            // We normally rely on C# 8's nullable reference types, but this class is intended
            // to be used for backwards compatibility, so we should null check arguments here.
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _rng.Fill(data);
        }

        /// <inheritdoc />
        public override void GetBytes(Byte[] data, Int32 offset, Int32 count)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Must be non-negative.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Must be non-negative.");
            if (count > data.Length - offset)
                throw new ArgumentException("offset + count must be less or equal to than data.Length.");

            _rng.Fill(data.AsSpan().Slice(offset, count));
        }

#if !NETSTANDARD2_0
        /// <inheritdoc />
        public override void GetBytes(Span<Byte> data) => _rng.Fill(data);

        /// <inheritdoc />
        public override void GetNonZeroBytes(Span<Byte> data)
        {
            while (data.Length > 0)
            {
                Span<Byte> random = data;
                _rng.Fill(random);
                Int32 zeroCount = 0;
                for (Int32 i = 0; i < random.Length; i++)
                {
                    if (random[i] != 0)
                        continue;

                    var src = random.Slice(i + 1);
                    var dst = random.Slice(i, src.Length);
                    src.CopyTo(dst);

                    random = random.Slice(i, src.Length);
                    zeroCount += 1;
                    i = -1;
                }

                // The rest of this needs to be refilled, since these bytes were moved down.
                data = data.Slice(data.Length - zeroCount);
            }
        }

        /// <inheritdoc />
        public override void GetNonZeroBytes(Byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            GetNonZeroBytes(data.AsSpan());
        }
#else
        /// <inheritdoc />
        public override void GetNonZeroBytes(Byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var span = data.AsSpan();
            while (span.Length > 0)
            {
                Span<Byte> random = span;
                _rng.Fill(random);
                Int32 zeroCount = 0;
                for (Int32 i = 0; i < random.Length; i++)
                {
                    if (random[i] != 0)
                        continue;

                    var src = random.Slice(i + 1);
                    var dst = random.Slice(i, src.Length);
                    src.CopyTo(dst);

                    random = random.Slice(i, src.Length);
                    zeroCount += 1;
                    i = -1;
                }

                // The rest of this needs to be refilled, since these bytes were moved down.
                span = span.Slice(span.Length - zeroCount);
            }
        }
#endif

        /// <inheritdoc />
        protected override void Dispose(Boolean disposing)
        {
            if (_rng is IDisposable disposable)
                disposable.Dispose();
        }
    }
}
