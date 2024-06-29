#if NET6_0_OR_GREATER

using System;

namespace RandN.Distributions;

public static partial class Uniform
{
    /// <summary>
    /// A uniform distribution of type <see cref="System.Numerics.BigInteger" />.
    /// </summary>
    public readonly struct BigInteger : IPortableDistribution<System.Numerics.BigInteger>
    {
        private readonly System.Numerics.BigInteger _low;
        private readonly System.Numerics.BigInteger _zone;
        private readonly System.Int32 _bytesToGenerate;
        private readonly System.Byte _bitmask;

        private BigInteger(System.Numerics.BigInteger low, System.Numerics.BigInteger zone,
            System.Int32 bytesToGenerate, System.Byte bitmask)
        {
            _low = low;
            _zone = zone;
            _bytesToGenerate = bytesToGenerate;
            _bitmask = bitmask;
        }

        /// <summary>
        /// Creates a <see cref="BigInteger" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Numerics.BigInteger, System.Numerics.BigInteger)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static BigInteger Create(System.Numerics.BigInteger low, System.Numerics.BigInteger high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high),
                    $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, high - 1);
        }

        /// <summary>
        /// Creates a <see cref="BigInteger" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Numerics.BigInteger, System.Numerics.BigInteger)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static BigInteger CreateInclusive(System.Numerics.BigInteger low, System.Numerics.BigInteger high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high),
                    $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var zone = high - low;
            var bitLength = zone.GetBitLength();
            var bytesToGenerate = (System.Int32)((bitLength + 7) / 8);
            var setBitCount = (System.Int32)(bitLength - bitLength / 8 * 8);
            if (setBitCount == 0)
                setBitCount = 8;

            var mask = (System.Byte)(System.Byte.MaxValue >> (8 - setBitCount));
            return new BigInteger(low, zone, bytesToGenerate, mask);
        }

        /// <inheritdoc />
        public System.Numerics.BigInteger Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var candidate = NextCandidate(rng);

            while (true)
            {
                if (candidate <= _zone)
                    return candidate + _low;

                candidate = NextCandidate(rng);
            }
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Numerics.BigInteger result) where TRng : notnull, IRng
        {
            var candidate = NextCandidate(rng);
            if (candidate > _zone)
            {
                result = System.Numerics.BigInteger.Zero;
                return false;
            }

            result = candidate + _low;
            return true;
        }

        private System.Numerics.BigInteger NextCandidate<TRng>(TRng rng) where TRng : notnull, IRng
        {
            if (_bytesToGenerate == 0)
                return System.Numerics.BigInteger.Zero;

            Span<System.Byte> span = _bytesToGenerate > 1024 ? new System.Byte[_bytesToGenerate] : stackalloc System.Byte[_bytesToGenerate];
            rng.Fill(span);
            span[^1] &= _bitmask;

            return new System.Numerics.BigInteger(span, true);
        }
    }
}
#endif
