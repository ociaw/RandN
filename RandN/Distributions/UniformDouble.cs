using System;
using System.Diagnostics;

namespace RandN.Distributions
{
    public sealed class UniformDouble : IDistribution<Double>
    {
        private const Int32 BITS_TO_DISCARD = 64 - 52;

        private readonly Double _low;
        private readonly Double _scale;

        private UniformDouble(Double low, Double scale)
        {
            _low = low;
            _scale = scale;
        }

        public static UniformDouble Create(Double low, Double high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            if (Double.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (Double.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (Double.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (Double.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            Double maxRand = (UInt64.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1.0;
            Double scale = high - low;
            while (true)
            {
                var mask = (scale * maxRand + low) >= high;
                if (!mask)
                    break;

                scale = (scale.ToBits() - 1).ToFloat();
            }

            Debug.Assert(0.0 <= scale);
            return new UniformDouble(low, scale);
        }

        public static UniformDouble CreateInclusive(Double low, Double high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            if (Double.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (Double.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (Double.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (Double.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            Double maxRand = (UInt64.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1.0;
            Double scale = (high - low) / maxRand;
            while (true)
            {
                var mask = (scale * maxRand + low) > high;
                if (!mask)
                    break;

                scale = (scale.ToBits() - 1).ToFloat();
            }

            Debug.Assert(0.0 <= scale);
            return new UniformDouble(low, scale);
        }

        /// <inheritdoc />
        public Double Sample<TRng>(TRng rng) where TRng : IRng
        {
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt64();
            Double value12 = (sample >> BITS_TO_DISCARD).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            Double value01 = value12 - 1.0;

            return value01 * _scale + _low;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Double result) where TRng : IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
