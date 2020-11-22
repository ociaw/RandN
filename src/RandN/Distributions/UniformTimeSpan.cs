using System;

namespace RandN.Distributions
{
    public static partial class Uniform
    {
        /// <summary>
        /// A uniform distribution of type <see cref="TimeSpan" />.
        /// </summary>
        public readonly struct TimeSpan : IPortableDistribution<System.TimeSpan>
        {
            private readonly UniformInt<Int64> _backing;

            private TimeSpan(UniformInt<Int64> backing) => _backing = backing;

            /// <summary>
            /// Creates a <see cref="Uniform.TimeSpan" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.TimeSpan, System.TimeSpan)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Uniform.TimeSpan Create(System.TimeSpan low, System.TimeSpan high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, high - System.TimeSpan.FromTicks(1));
            }

            /// <summary>
            /// Creates a <see cref="Uniform.TimeSpan" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.TimeSpan, System.TimeSpan)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Uniform.TimeSpan CreateInclusive(System.TimeSpan low, System.TimeSpan high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                UniformInt<Int64> backing = UniformInt.CreateInclusive(low.Ticks, high.Ticks);
                return new Uniform.TimeSpan(backing);
            }

            /// <inheritdoc />
            public System.TimeSpan Sample<TRng>(TRng rng) where TRng : notnull, IRng
            {
                var ticks = _backing.Sample(rng);
                return System.TimeSpan.FromTicks(ticks);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.TimeSpan result) where TRng : notnull, IRng
            {
                if (_backing.TrySample(rng, out var ticks))
                {
                    result = System.TimeSpan.FromTicks(ticks);
                    return true;
                }

                result = System.TimeSpan.Zero;
                return false;
            }
        }
    }
}
