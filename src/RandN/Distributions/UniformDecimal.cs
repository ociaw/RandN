using System;
using System.Diagnostics;
using RandN.Implementation;

namespace RandN.Distributions
{
    public static partial class Uniform
    {
        /// <summary>
        /// A uniform distribution of type <see cref="System.Decimal" />.
        /// </summary>
        public readonly struct Decimal : IPortableDistribution<System.Decimal>
        {
            private readonly System.Decimal _low;
            private readonly System.Decimal _scale;

            private Decimal(System.Decimal low, System.Decimal scale)
            {
                _low = low;
                _scale = scale;
            }

            /// <summary>
            /// Creates a <see cref="Decimal" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.Decimal, System.Decimal)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Decimal Create(System.Decimal low, System.Decimal high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                const System.Decimal maxRand = 7.9228162514264337593543950335m; // Equal to new Decimal(-1, -1, -1, false, 28);

                System.Decimal range;
                try
                {
                    range = high - low;
                }
                catch (OverflowException)
                {
                    // Not ideal, but the sort of thing occurs for Doubles and Singles
                    range = System.Decimal.MaxValue;
                }
                System.Decimal scale = range / maxRand;
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
                return new Decimal(low, scale);
            }

            /// <summary>
            /// Creates a <see cref="Decimal" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Decimal, System.Decimal)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Decimal CreateInclusive(System.Decimal low, System.Decimal high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                const System.Decimal maxRand = 7.9228162514264337593543950335m; // Equal to new Decimal(-1, -1, -1, false, 28);

                System.Decimal range;
                try
                {
                    range = high - low;
                }
                catch (OverflowException)
                {
                    // Not ideal, but the sort of thing occurs for Doubles and Singles
                    range = System.Decimal.MaxValue;
                }
                System.Decimal scale = range / maxRand;
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
                return new Decimal(low, scale);
            }

            /// <inheritdoc />
            public System.Decimal Sample<TRng>(TRng rng) where TRng : notnull, IRng
            {
                // 96 bits of precision - The range generated is approximately [0-7.92].
                const System.Int32 decimalScale = 28;
                System.Int32 lo = unchecked((System.Int32)rng.NextUInt32());
                System.Int32 mid = unchecked((System.Int32)rng.NextUInt32());
                System.Int32 hi = unchecked((System.Int32)rng.NextUInt32());
                var value = new System.Decimal(lo, mid, hi, isNegative: false, decimalScale);
                return value * _scale + _low;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.Decimal result) where TRng : notnull, IRng
            {
                result = Sample(rng);
                return true;
            }
        }
    }
}
