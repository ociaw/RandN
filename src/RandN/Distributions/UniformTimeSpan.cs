using System;

namespace RandN.Distributions
{
    /// <summary>
    /// A uniform distribution of type <see cref="TimeSpan" />.
    /// </summary>
    public readonly struct UniformTimeSpan : IPortableDistribution<TimeSpan>
    {
        private readonly UniformInt<Int64> _backing;

        private UniformTimeSpan(UniformInt<Int64> backing) => _backing = backing;

        /// <summary>
        /// Creates a <see cref="UniformTimeSpan" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(TimeSpan, TimeSpan)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformTimeSpan Create(TimeSpan low, TimeSpan high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, high - TimeSpan.FromTicks(1));
        }

        /// <summary>
        /// Creates a <see cref="UniformTimeSpan" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(TimeSpan, TimeSpan)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformTimeSpan CreateInclusive(TimeSpan low, TimeSpan high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            UniformInt<Int64> backing = UniformInt.CreateInclusive(low.Ticks, high.Ticks);
            return new UniformTimeSpan(backing);
        }

        /// <inheritdoc />
        public TimeSpan Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var ticks = _backing.Sample(rng);
            return TimeSpan.FromTicks(ticks);
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out TimeSpan result) where TRng : notnull, IRng
        {
            if (_backing.TrySample(rng, out var ticks))
            {
                result = TimeSpan.FromTicks(ticks);
                return true;
            }

            result = TimeSpan.Zero;
            return false;
        }
    }
}
