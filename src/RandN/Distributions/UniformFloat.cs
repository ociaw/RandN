




using System;
using System.Diagnostics;
using RandN.Implementation;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{

    /// <summary>
    /// A uniform distribution of type <see cref="Single" />.
    /// </summary>
    public sealed class UniformSingle : IDistribution<Single>
    {
        private const Int32 BITS_TO_DISCARD = 9;

        private readonly Single _low;
        private readonly Single _scale;

        private UniformSingle(Single low, Single scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <summary>
        /// Creates a <see cref="UniformSingle" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Single, Single)" />.
        /// </summary>
        public static UniformSingle Create(Single low, Single high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");
            if (Single.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (Single.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (Single.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (Single.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            Single maxRand = (UInt32.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1;
            Single scale = high - low;
            while (true)
            {
                var mask = (scale * maxRand + low) >= high;
                if (!mask)
                    break;

                scale = (scale.ToBits() - 1).ToFloat();
            }

            Debug.Assert(0.0 <= scale);
            return new UniformSingle(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="UniformSingle" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Single, Single)" />.
        /// </summary>
        public static UniformSingle CreateInclusive(Single low, Single high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            if (Single.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (Single.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (Single.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (Single.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            Single maxRand = (UInt32.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1;
            Single scale = (high - low) / maxRand;
            while (true)
            {
                var mask = (scale * maxRand + low) > high;
                if (!mask)
                    break;

                scale = (scale.ToBits() - 1).ToFloat();
            }

            Debug.Assert(0.0 <= scale);
            return new UniformSingle(low, scale);
        }

        /// <inheritdoc />
        public Single Sample<TRng>(TRng rng) where TRng : IRng
        {
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt32();
            Single value12 = (sample >> BITS_TO_DISCARD).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            Single value01 = value12 - 1;

            return value01 * _scale + _low;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
        {
            result = Sample(rng);
            return true;
        }
    }


    /// <summary>
    /// A uniform distribution of type <see cref="Double" />.
    /// </summary>
    public sealed class UniformDouble : IDistribution<Double>
    {
        private const Int32 BITS_TO_DISCARD = 12;

        private readonly Double _low;
        private readonly Double _scale;

        private UniformDouble(Double low, Double scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <summary>
        /// Creates a <see cref="UniformDouble" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Double, Double)" />.
        /// </summary>
        public static UniformDouble Create(Double low, Double high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");
            if (Double.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (Double.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (Double.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (Double.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            Double maxRand = (UInt64.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1;
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

        /// <summary>
        /// Creates a <see cref="UniformDouble" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Double, Double)" />.
        /// </summary>
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

            Double maxRand = (UInt64.MaxValue >> BITS_TO_DISCARD).IntoFloatWithExponent(0) - 1;
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
            Double value01 = value12 - 1;

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
