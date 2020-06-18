using System;
using System.Diagnostics;
using RandN.Implementation;

namespace RandN.Distributions
{
    /// <summary>
    /// A uniform distribution of type <see cref="Decimal" />.
    /// </summary>
    public readonly struct UniformDecimal : IDistribution<Decimal>
    {
        private readonly Decimal _low;
        private readonly Decimal _scale;

        private UniformDecimal(Decimal low, Decimal scale)
        {
            _low = low;
            _scale = scale;
        }

        /// <summary>
        /// Creates a <see cref="UniformDecimal" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Decimal, Decimal)" />.
        /// </summary>
        public static UniformDecimal Create(Decimal low, Decimal high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            const Decimal maxRand = 7.9228162514264337593543950335m; // Equal to new Decimal(-1, -1, -1, false, 28);

            Decimal range;
            try
            {
                range = high - low;
            }
            catch (OverflowException)
            {
                // Not ideal, but the sort of thing occurs for Doubles and Singles
                range = Decimal.MaxValue;
            }
            Decimal scale = range / maxRand;
            while (true)
            {
                try
                {
                    if (scale * maxRand + low < high)
                        break;
                }
                catch (OverflowException)
                {
                    // We don't do anything different for overflows
                }

                scale = scale.DecrementMantissa();
            }

            Debug.Assert(0 <= scale);
            return new UniformDecimal(low, scale);
        }

        /// <summary>
        /// Creates a <see cref="UniformDecimal" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Decimal, Decimal)" />.
        /// </summary>
        public static UniformDecimal CreateInclusive(Decimal low, Decimal high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            const Decimal maxRand = 7.9228162514264337593543950335m; // Equal to new Decimal(-1, -1, -1, false, 28);

            Decimal range;
            try
            {
                range = high - low;
            }
            catch (OverflowException)
            {
                // Not ideal, but the sort of thing occurs for Doubles and Singles
                range = Decimal.MaxValue;
            }
            Decimal scale = range / maxRand;
            while (true)
            {
                try
                {
                    if (scale * maxRand + low <= high)
                        break;
                }
                catch (OverflowException)
                {
                    // We don't do anything different for overflows
                }

                scale = scale.DecrementMantissa();
            }

            Debug.Assert(0 <= scale);
            return new UniformDecimal(low, scale);
        }

        /// <inheritdoc />
        public Decimal Sample<TRng>(TRng rng) where TRng : IRng
        {
            // 96 bits of precision - The range generated is approximately [0-7.92].
            const Int32 decimalScale = 28;
            Int32 lo = unchecked((Int32)rng.NextUInt32());
            Int32 mid = unchecked((Int32)rng.NextUInt32());
            Int32 hi = unchecked((Int32)rng.NextUInt32());
            var value = new Decimal(lo, mid, hi, isNegative: false, decimalScale);
            return value * _scale + _low;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Decimal result) where TRng : IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
