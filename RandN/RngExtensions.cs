using System;

namespace RandN
{
    /// <summary>
    /// Various extension methods to simplify use of RNGs.
    /// </summary>
    public static class RngExtensions
    {
        /// <summary>
        /// Samples the distribution from <paramref name="rng"/>.
        /// </summary>
        /// <remarks>
        /// The is equivalent to calling <see cref="IDistribution{TResult}.Sample{TRng}(TRng)"/>.
        /// </remarks>
        public static TResult Sample<TRng, TDistribution, TResult>(this TRng rng, TDistribution distribution)
            where TRng : IRng
            where TDistribution : IDistribution<TResult>
        {
            return distribution.Sample(rng);
        }

        /// <summary>
        /// Samples the distribution from <paramref name="rng"/>.
        /// </summary>
        /// <remarks>
        /// The is equivalent to calling <see cref="IDistribution{TResult}.Sample{TRng}(TRng)"/>.
        /// </remarks>
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
