#if NET8_0_OR_GREATER
using System;

namespace RandN.Distributions;

public static partial class Uniform
{
    /// <summary>
    /// A uniform distribution of type <see cref="System.Int128" />.
    /// </summary>
    public readonly struct Int128 : IPortableDistribution<System.Int128>
    {
        private readonly System.Int128 _low;
        private readonly System.UInt128 _range;
        private readonly System.UInt128 _zone;

        private Int128(System.Int128 low, System.UInt128 range, System.UInt128 zone)
        {
            _low = low;
            _range = range;
            _zone = System.UInt128.MaxValue - zone;
        }

        /// <summary>
        /// Creates a <see cref="Int128" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Int128, System.Int128)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Int128 Create(System.Int128 low, System.Int128 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, high - 1);
        }

        /// <summary>
        /// Creates a <see cref="Int128" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Int128, System.Int128)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Int128 CreateInclusive(System.Int128 low, System.Int128 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            System.UInt128 unsignedMax = System.UInt128.MaxValue;
            var range = unchecked((System.UInt128)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new Int128(low, range, intsToReject);
        }

        /// <inheritdoc />
        public System.Int128 Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var unsigned = rng.NextUInt128();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return unchecked((System.Int128)unsigned);

            var zone = _zone;

            while (true)
            {
                var (hi, lo) = unsigned.WideningMultiply(_range);

                if (lo <= zone)
                    return unchecked((System.Int128)hi + _low);

                unsigned = rng.NextUInt128();
            }
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Int128 result) where TRng : notnull, IRng
        {
            var unsigned = rng.NextUInt128();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = unchecked((System.Int128)unsigned);
                return true;
            }

            var zone = _zone;
            var (hi, lo) = unsigned.WideningMultiply(_range);

            if (lo <= zone)
            {
                result = unchecked((System.Int128)hi + _low);
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="System.UInt128" />.
    /// </summary>
    public readonly struct UInt128 : IPortableDistribution<System.UInt128>
    {
        private readonly System.UInt128 _low;
        private readonly System.UInt128 _range;
        private readonly System.UInt128 _zone;

        private UInt128(System.UInt128 low, System.UInt128 range, System.UInt128 zone)
        {
            _low = low;
            _range = range;
            _zone = System.UInt128.MaxValue - zone;
        }

        /// <summary>
        /// Creates a <see cref="UInt128" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.UInt128, System.UInt128)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UInt128 Create(System.UInt128 low, System.UInt128 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, high - 1);
        }

        /// <summary>
        /// Creates a <see cref="UInt128" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.UInt128, System.UInt128)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UInt128 CreateInclusive(System.UInt128 low, System.UInt128 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            System.UInt128 unsignedMax = System.UInt128.MaxValue;
            var range = unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UInt128(low, range, intsToReject);
        }

        /// <inheritdoc />
        public System.UInt128 Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var unsigned = rng.NextUInt128();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return unsigned;

            var zone = _zone;

            while (true)
            {
                var (hi, lo) = unsigned.WideningMultiply(_range);

                if (lo <= zone)
                    return unchecked(hi + _low);

                unsigned = rng.NextUInt128();
            }
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.UInt128 result) where TRng : notnull, IRng
        {
            var unsigned = rng.NextUInt128();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = unsigned;
                return true;
            }

            var zone = _zone;
            var (hi, lo) = unsigned.WideningMultiply(_range);

            if (lo <= zone)
            {
                result = unchecked(hi + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
}
#endif
