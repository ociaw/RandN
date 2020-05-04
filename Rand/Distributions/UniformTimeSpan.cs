using System;

namespace Rand.Distributions
{
    public sealed class UniformTimeSpan : IDistribution<TimeSpan>
    {
        private readonly UniformInt64 _backing;

        private UniformTimeSpan(UniformInt64 backing) => _backing = backing;

        public static UniformTimeSpan Create(TimeSpan low, TimeSpan high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, high - TimeSpan.FromTicks(1));
        }

        public static UniformTimeSpan CreateInclusive(TimeSpan low, TimeSpan high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
            
            UniformInt64 backing = UniformInt64.CreateInclusive(low.Ticks, high.Ticks);
            return new UniformTimeSpan(backing);
        }
            
        public Boolean TrySample<TRng>(TRng rng, out TimeSpan result) where TRng : IRng
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
