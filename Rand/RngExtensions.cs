using System;

namespace Rand
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
    }
}
