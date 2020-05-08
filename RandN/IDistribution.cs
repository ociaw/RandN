using System;

namespace RandN
{
    public interface IDistribution<TResult>
    {
        /// <summary>
        /// Samples a value from <paramref name="rng"/>, blocking until a suitable value is returned.
        /// </summary>
        /// <remarks>
        /// This method could block indefinitely if the quality of the RNG is poor or if luck hates you.
        /// </remarks>
        TResult Sample<TRng>(TRng rng)
            where TRng : IRng
        {
            TResult result;
            while (!TrySample(rng, out result))
            { }

            return result;
        }

        /// <summary>
        /// Attempts to sample a value from <paramref name="rng"/> once, returning false if the value returned
        /// is not suitable.
        /// </summary>
        Boolean TrySample<TRng>(TRng rng, out TResult result) where TRng : IRng;
    }
}
