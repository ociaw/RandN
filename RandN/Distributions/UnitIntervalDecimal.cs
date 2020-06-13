using System;

namespace RandN.Distributions
{
    public sealed partial class UnitInterval
    {
        /// <summary>
        /// A distribution over the closed-open interval [0, 1).
        /// </summary>
        public sealed class ClosedOpenDecimal : IDistribution<Decimal>
        {
            private ClosedOpenDecimal() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedOpenDecimal" />.
            /// </summary>
            public static ClosedOpenDecimal Instance { get; } = new ClosedOpenDecimal();

            /// <inheritdoc />
            public Decimal Sample<TRng>(TRng rng) where TRng : IRng
            {
                Decimal result;
                while (!TrySample(rng, out result)) ;
                return result;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Decimal result) where TRng : IRng
            {
                result = GenerateCandidateDecimal(rng);
                return result < 1;
            }
        }

        /// <summary>
        /// A distribution over the open-closed interval (0, 1].
        /// </summary>
        public sealed class OpenClosedDecimal : IDistribution<Decimal>
        {
            private OpenClosedDecimal() { }

            /// <summary>
            /// Gets the instance of <see cref="OpenClosedDecimal" />.
            /// </summary>
            public static OpenClosedDecimal Instance { get; } = new OpenClosedDecimal();

            /// <inheritdoc />
            public Decimal Sample<TRng>(TRng rng) where TRng : IRng
            {
                Decimal result;
                while (!TrySample(rng, out result)) ;
                return result;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Decimal result) where TRng : IRng
            {
                result = GenerateCandidateDecimal(rng);
                return 0 < result && result <= 1;
            }
        }

        /// <summary>
        /// A distribution over the closed interval [0, 1].
        /// </summary>
        public sealed class ClosedDecimal : IDistribution<Decimal>
        {
            private ClosedDecimal() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedDecimal" />.
            /// </summary>
            public static ClosedDecimal Instance { get; } = new ClosedDecimal();

            /// <inheritdoc />
            public Decimal Sample<TRng>(TRng rng) where TRng : IRng
            {
                Decimal result;
                while (!TrySample(rng, out result)) ;
                return result;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Decimal result) where TRng : IRng
            {
                result = GenerateCandidateDecimal(rng);
                return result <= 1;
            }
        }

        /// <summary>
        /// A distribution over the open interval (0, 1).
        /// </summary>
        public sealed class OpenDecimal : IDistribution<Decimal>
        {
            private OpenDecimal() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedDecimal" />.
            /// </summary>
            public static OpenDecimal Instance { get; } = new OpenDecimal();

            /// <inheritdoc />
            public Decimal Sample<TRng>(TRng rng) where TRng : IRng
            {
                Decimal result;
                while (!TrySample(rng, out result)) ;
                return result;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Decimal result) where TRng : IRng
            {
                result = GenerateCandidateDecimal(rng);
                return 0 < result && result < 1;
            }
        }

        private static Decimal GenerateCandidateDecimal<TRng>(TRng rng) where TRng : IRng
        {
            // 93-94 bits of precision - we discard the upper two bits to reduce the average number
            // of sample attempts. The range generated is approximately [0-1.98], so just under 50%
            // of sample attempts will be rejected.
            const Int32 scale = 28;
            const Int32 bitsToDiscard = 2;
            Int32 lo = (Int32)rng.NextUInt32();
            Int32 mid = (Int32)rng.NextUInt32();
            Int32 hi = (Int32)(rng.NextUInt32() >> bitsToDiscard);
            return new Decimal(lo, mid, hi, isNegative: false, scale);
        }
    }
}
