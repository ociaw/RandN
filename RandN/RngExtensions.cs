using System;

namespace RandN
{
    public static class RngExtensions
    {
        public static TResult Sample<TRng, TDistribution, TResult>(this TRng rng, TDistribution distribution)
            where TRng : IRng
            where TDistribution : IDistribution<TResult>
        {
            return distribution.Sample(rng);
        }

        public static TResult Sample<TRng, TResult>(this TRng rng, IDistribution<TResult> distribution)
            where TRng : IRng
        {
            return distribution.Sample(rng);
        }

        /// <summary>
        /// Creates a new <see cref="TRng"/> using a seed created from <paramref name="seedingRng"/>.
        /// </summary>
        /// <param name="seedingRng">The RNG used to create the seed.</param>
        /// <returns>A new <see cref="TRng"/> instance.</returns>
        public static TRng Create<TRng, TSeed, TSeedingRng>(this IReproducibleRngFactory<TRng, TSeed> factory, TSeedingRng seedingRng)
            where TRng : IRng
            where TSeedingRng : IRng
        {
            var seed = factory.CreateSeed(seedingRng);
            return factory.Create(seed);
        }
    }
}
