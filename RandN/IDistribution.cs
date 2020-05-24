using System;

namespace RandN
{
    public interface IDistribution<TResult>
    {
        /// <summary>
        /// Samples a value from <paramref name="rng"/>, blocking until a suitable value is returned.
        /// </summary>
        /// <remarks>
        /// Depending on the distribution, this method could block indefinitely if the quality of the RNG is poor or if you're extraordinarily unlucky.
        /// </remarks>
        TResult Sample<TRng>(TRng rng) where TRng : IRng;

        /// <summary>
        /// Attempts to sample a value from <paramref name="rng"/> once, returning false if the value returned
        /// is not suitable.
        /// </summary>
        Boolean TrySample<TRng>(TRng rng, out TResult result) where TRng : IRng;
    }
}
