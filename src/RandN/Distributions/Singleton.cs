using System;

namespace RandN.Distributions
{
    /// <summary>
    /// A distribution containing a single value.
    /// </summary>
    public static class Singleton
    {
        /// <summary>
        /// Creates a singleton distribution containing a single value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public static Singleton<T> New<T>(T value) =>
            new Singleton<T>(value);
    }

    /// <summary>
    /// A distribution containing a single value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Singleton<T> : IPortableDistribution<T>
    {
        internal Singleton(T value)
        {
        }

        /// <inheritdoc />
        public T Sample<TRng>(TRng rng) where TRng : notnull, IRng =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out T result) where TRng : notnull, IRng =>
            throw new NotImplementedException();
    }
}
