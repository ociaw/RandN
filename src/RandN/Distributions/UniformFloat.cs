using System;
using System.Diagnostics;
using RandN.Implementation;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions;

public static partial class Uniform
{
    /// <summary>
    /// A uniform distribution of type <see cref="System.Single" />.
    /// </summary>
    public readonly struct Single : IDistribution<System.Single>
    {
        private const System.Int32 BitsToDiscard = 9;

        private readonly System.Single _low;
        private readonly System.Single _scale;

        private Single(System.Single low, System.Single scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <summary>
        /// Creates a <see cref="Single" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Single, System.Single)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Single Create(System.Single low, System.Single high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");
            if (System.Single.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (System.Single.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (System.Single.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (System.Single.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            System.Single maxRand = (System.UInt32.MaxValue >> BitsToDiscard).IntoFloatWithExponent(0) - 1;
            System.Single scale = (high - low).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low).ForceStandardPrecision();
                var aboveMax = maxPossible >= high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!System.Single.IsPositiveInfinity(scale));
            return new Single(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="Single" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Single, System.Single)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Single CreateInclusive(System.Single low, System.Single high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            if (System.Single.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (System.Single.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (System.Single.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (System.Single.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            System.Single maxRand = (System.UInt32.MaxValue >> BitsToDiscard).IntoFloatWithExponent(0) - 1;
            System.Single scale = ((high - low) / maxRand).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = scale * maxRand + low;
                var aboveMax = maxPossible > high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!System.Single.IsPositiveInfinity(scale));
            return new Single(low, scale);
        }

        /// <inheritdoc />
        public System.Single Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt32();
            System.Single value12 = (sample >> BitsToDiscard).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            System.Single value01 = value12 - 1;

            return value01 * _scale + _low;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Single result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
    /// <summary>
    /// A uniform distribution of type <see cref="System.Double" />.
    /// </summary>
    public readonly struct Double : IDistribution<System.Double>
    {
        private const System.Int32 BitsToDiscard = 12;

        private readonly System.Double _low;
        private readonly System.Double _scale;

        private Double(System.Double low, System.Double scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <summary>
        /// Creates a <see cref="Double" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Double, System.Double)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Double Create(System.Double low, System.Double high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");
            if (System.Double.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (System.Double.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (System.Double.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (System.Double.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            System.Double maxRand = (System.UInt64.MaxValue >> BitsToDiscard).IntoFloatWithExponent(0) - 1;
            System.Double scale = (high - low).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low).ForceStandardPrecision();
                var aboveMax = maxPossible >= high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!System.Double.IsPositiveInfinity(scale));
            return new Double(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="Double" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(System.Double, System.Double)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Double CreateInclusive(System.Double low, System.Double high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            if (System.Double.IsInfinity(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be finite.");
            if (System.Double.IsInfinity(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be finite.");
            if (System.Double.IsNaN(low))
                throw new ArgumentOutOfRangeException(nameof(low), low, "Must be a number.");
            if (System.Double.IsNaN(high))
                throw new ArgumentOutOfRangeException(nameof(high), high, "Must be a number.");

            System.Double maxRand = (System.UInt64.MaxValue >> BitsToDiscard).IntoFloatWithExponent(0) - 1;
            System.Double scale = ((high - low) / maxRand).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = scale * maxRand + low;
                var aboveMax = maxPossible > high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!System.Double.IsPositiveInfinity(scale));
            return new Double(low, scale);
        }

        /// <inheritdoc />
        public System.Double Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt64();
            System.Double value12 = (sample >> BitsToDiscard).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            System.Double value01 = value12 - 1;

            return value01 * _scale + _low;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Double result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
