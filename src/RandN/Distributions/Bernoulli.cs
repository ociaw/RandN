using System;

namespace RandN.Distributions
{
    /// <summary>
    /// A Bernoulli distribution, where each sample returns either <see langword="true" /> or <see langword="false" />.
    /// </summary>
    public readonly struct Bernoulli : IPortableDistribution<Boolean>
    {
        /// <summary>
        /// Probability represented as _p / 2^64.
        /// </summary>
        private readonly UInt64 _p;

        /// <summary>
        /// Indicates whether or not this distribution always samples true.
        /// </summary>
        private readonly Boolean _alwaysTrue;

        /// <summary>
        /// 2^64
        /// </summary>
        private const Double Scale = 2.0 * (1ul << 63);

        private Bernoulli(UInt64 p, Boolean alwaysTrue)
        {
            _p = p;
            _alwaysTrue = alwaysTrue;
        }

        /// <summary>
        /// Creates a new Bernoulli distribution with a probability of p.
        /// </summary>
        /// <param name="p">The probability of success. 0 &lt;= p &lt;= 1</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="p"/> is greater than 1 or less than 0.</exception>
        public static Bernoulli FromP(Double p)
        {
            if (p < 0.0)
                throw new ArgumentOutOfRangeException(nameof(p), p, $"{nameof(p)} must be greater than or equal to 0.");
            if (p > 1.0)
                throw new ArgumentOutOfRangeException(nameof(p), p, $"{nameof(p)} must be less than or equal to 1.");
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (p == 1.0)
                return new Bernoulli(0, true);

            return new Bernoulli((UInt64)(p * Scale), false);
        }

        /// <summary>
        /// Creates a new Bernoulli distribution, with a probability of <paramref name="numerator"/> / <paramref name="denominator"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="numerator"/> is greater than <paramref name="denominator"/>.</exception>
        public static Bernoulli FromRatio(UInt32 numerator, UInt32 denominator)
        {
            if (numerator > denominator)
                throw new ArgumentOutOfRangeException(nameof(numerator), numerator, $"{nameof(numerator)} must be less than or equal to {nameof(denominator)}.");
            if (numerator == denominator)
                return new Bernoulli(0, true);

            var p = (Double)numerator / denominator * Scale;
            return new Bernoulli((UInt64)p, false);
        }

        /// <summary>
        /// Creates a new Bernoulli distribution, with a probability of <paramref name="numerator"/> / 2^64.
        /// </summary>
        public static Bernoulli FromInverse(UInt64 numerator) => new(numerator, false);

        /// <inheritdoc />
        public Boolean Sample<TRng>(TRng rng) where TRng : notnull, IRng => _alwaysTrue || rng.NextUInt64() < _p;

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Boolean result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
