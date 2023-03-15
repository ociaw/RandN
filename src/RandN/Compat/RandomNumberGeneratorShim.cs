using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace RandN.Compat;

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
        where TRng : notnull, ICryptoRng
    {
        return new(rng);
    }
}

/// <summary>
/// A shim able to wrap any <see cref="IRng"/> as a <see cref="RandomNumberGenerator"/>.
/// </summary>
public sealed class RandomNumberGeneratorShim<TRng> : RandomNumberGenerator
    where TRng : notnull, ICryptoRng
{
    private readonly TRng _rng;

    /// <summary>
    /// Constructs a new <see cref="RandomNumberGenerator"/> wrapper over <paramref name="rng"/>.
    /// </summary>
    /// /// <param name="rng">A cryptographically secure RNG.</param>
    public RandomNumberGeneratorShim(TRng rng) => _rng = rng;

    /// <inheritdoc />
    public override void GetBytes(Byte[] data) => _rng.Fill(data.AsSpan());

    /// <inheritdoc />
    public override void GetBytes(Byte[] data, Int32 offset, Int32 count)
    {
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
            // Simple algorithm to remove zero bytes, searching for them one at a time. We don't bother with attempting
            // to shift multiple zeros at a time since it's fairly unlikely to occur.
            while (data.Length > 0)
            {
                // Fill the data with random bytes
                Span<Byte> random = data;
                _rng.Fill(random);

                // Search for zero bytes
                Int32 zeroCount = 0;
                for (Int32 i = 0; i < random.Length; i++)
                {
                    if (random[i] != 0)
                        continue;

                    // Now that we've found a zero-byte, overwrite it with all the data after it
                    var src = random.Slice(i + 1);
                    var dst = random.Slice(i, src.Length);
                    src.CopyTo(dst);

                    // Adjust the search window and start from the beginning
                    random = random.Slice(i, src.Length);
                    zeroCount += 1;
                    i = -1;
                }

                // Slice away the data that's definitely non-zero.
                // The rest of the data needs to be regenerated, since those bytes were moved down. We'll end up
                // regenerating zeroCount bytes.
                data = data.Slice(data.Length - zeroCount);
                Debug.Assert(data.Length == zeroCount);
            }
        }

        /// <inheritdoc />
        public override void GetNonZeroBytes(Byte[] data) => GetNonZeroBytes(data.AsSpan());
#else
    /// <inheritdoc />
    public override void GetNonZeroBytes(Byte[] data)
    {
        // Simple algorithm to remove zero bytes, searching for them one at a time. We don't bother with attempting
        // to shift multiple zeros at a time since it's fairly unlikely to occur.
        var span = data.AsSpan();
        while (span.Length > 0)
        {
            // Fill the data with random bytes
            Span<Byte> random = span;
            _rng.Fill(random);

            // Search for zero bytes
            Int32 zeroCount = 0;
            for (Int32 i = 0; i < random.Length; i++)
            {
                if (random[i] != 0)
                    continue;

                // Now that we've found a zero-byte, overwrite it with all the data after it
                var src = random.Slice(i + 1);
                var dst = random.Slice(i, src.Length);
                src.CopyTo(dst);

                // Adjust the search window and start from the beginning
                random = random.Slice(i, src.Length);
                zeroCount += 1;
                i = -1;
            }

            // Slice away the data that's definitely non-zero.
            // The rest of the data needs to be regenerated, since those bytes were moved down. We'll end up
            // regenerating zeroCount bytes.
            span = span.Slice(span.Length - zeroCount);
            Debug.Assert(span.Length == zeroCount);
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
