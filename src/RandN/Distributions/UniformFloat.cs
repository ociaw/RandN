




using System;
using System.Diagnostics;
using RandN.Implementation;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    internal static class UniformFloat
    {

        /// <summary>
        /// Creates a <see cref="UniformFloat{Single}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Single, Single)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/> or when
        /// <paramref name="low"/> or <paramref name="high"/> are non-finite or NaN.
        /// </exception>
        public static UniformFloat<Single> Create(Single low, Single high)
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

            const Int32 bitsToDiscard = 9;

            Single maxRand = (UInt32.MaxValue >> bitsToDiscard).IntoFloatWithExponent(0) - 1;
            Single scale = (high - low).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low).ForceStandardPrecision();
                var aboveMax = maxPossible >= high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!Single.IsPositiveInfinity(scale));
            return new UniformFloat<Single>(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="UniformFloat{Single}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Single, Single)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Single> CreateInclusive(Single low, Single high)
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

            const Int32 bitsToDiscard = 9;

            Single maxRand = (UInt32.MaxValue >> bitsToDiscard).IntoFloatWithExponent(0) - 1;
            Single scale = ((high - low) / maxRand).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low);
                var aboveMax = maxPossible > high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!Single.IsPositiveInfinity(scale));
            return new UniformFloat<Single>(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="UniformFloat{Double}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Double, Double)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/> or when
        /// <paramref name="low"/> or <paramref name="high"/> are non-finite or NaN.
        /// </exception>
        public static UniformFloat<Double> Create(Double low, Double high)
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

            const Int32 bitsToDiscard = 12;

            Double maxRand = (UInt64.MaxValue >> bitsToDiscard).IntoFloatWithExponent(0) - 1;
            Double scale = (high - low).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low).ForceStandardPrecision();
                var aboveMax = maxPossible >= high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!Double.IsPositiveInfinity(scale));
            return new UniformFloat<Double>(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="UniformFloat{Double}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Double, Double)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Double> CreateInclusive(Double low, Double high)
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

            const Int32 bitsToDiscard = 12;

            Double maxRand = (UInt64.MaxValue >> bitsToDiscard).IntoFloatWithExponent(0) - 1;
            Double scale = ((high - low) / maxRand).ForceStandardPrecision();
            while (true)
            {
                var maxPossible = (scale * maxRand + low);
                var aboveMax = maxPossible > high;
                if (!aboveMax)
                    break;

                scale = (scale.ToBits() - 1).ToFloat().ForceStandardPrecision();
            }

            Debug.Assert(0.0 <= scale);
            Debug.Assert(!Double.IsPositiveInfinity(scale));
            return new UniformFloat<Double>(low, scale);
        }

    }

    /// <summary>
    /// Implements a Uniform <see cref="IDistribution{TResult}"/> for <see cref="Single" /> and <see cref="Double" />.
    /// Use of any other type results in a runtime exception.
    /// </summary>
    public readonly struct UniformFloat<T> : IDistribution<T>
        // We're extremely restrictive here to discourage people from trying to use unsupported types for T
        where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    {
        private readonly T _low;
        private readonly T _scale;

        internal UniformFloat(T low, T scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out T result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }

        /// <inheritdoc />
        public T Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {

            if (typeof(T) == typeof(Single))
                return (T)(Object)SampleSingle(rng);

            if (typeof(T) == typeof(Double))
                return (T)(Object)SampleDouble(rng);

            throw new NotSupportedException($"Type {typeof(T)} is not supported.");
        }


        private Single SampleSingle<TRng>(TRng rng) where TRng : notnull, IRng
        {
            const Int32 BitsToDiscard = 9;
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt32();
            Single value12 = (sample >> BitsToDiscard).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            Single value01 = value12 - 1;
            Single scale = (Single)(Object)_scale;
            Single low = (Single)(Object)_low;

            return value01 * scale + low;
        }

        private Double SampleDouble<TRng>(TRng rng) where TRng : notnull, IRng
        {
            const Int32 BitsToDiscard = 12;
            // Generate a value in the range [1, 2)
            var sample = rng.NextUInt64();
            Double value12 = (sample >> BitsToDiscard).IntoFloatWithExponent(0);

            // Get a value in the range [0, 1) in order to avoid
            // overflowing into infinity when multiplying with scale
            Double value01 = value12 - 1;
            Double scale = (Double)(Object)_scale;
            Double low = (Double)(Object)_low;

            return value01 * scale + low;
        }

    }
}
