using System;

namespace Rand
{
    /// <summary>
    /// Creates instances of an <see cref="IReproducibleRngFactory"/> using automatically generated seeds.
    /// </summary>
    public sealed class AutoSeedingRngFactory<TRng, TSeedingRng, TSeed> : IRngFactory<TRng>
        where TRng : IRng
        where TSeedingRng : IRng
    {
        private readonly TSeedingRng _seedSource;

        private readonly IReproducibleRngFactory<TRng, TSeed> _rngFactory;

        internal AutoSeedingRngFactory(IReproducibleRngFactory<TRng, TSeed> rngFactory, TSeedingRng seedSource)
        {
            _rngFactory = rngFactory;
            _seedSource = seedSource;
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="TRng"/>.
        /// </summary>
        /// <returns>A seeded instance of <typeparamref name="TRng"/>.</returns>
        public TRng Create()
        {
            var seed = _rngFactory.CreateSeed(_seedSource);
            return _rngFactory.Create(seed);
        }
    }

    public static class AutoSeedingRngFactory
    {
        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG factory and a Cryptographically Secure Seed Source.
        /// </summary>
        public static AutoSeedingRngFactory<TRng, CryptoServiceProvider, TSeed> Create<TRng, TSeed>(IReproducibleRngFactory<TRng, TSeed> rngFactory)
            where TRng : IRng
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));

            var seedSource = CryptoServiceProvider.Factory.Instance.Create();
            return new AutoSeedingRngFactory<TRng, CryptoServiceProvider, TSeed>(rngFactory, seedSource);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG Factory and Seed Source.
        /// </summary>
        public static AutoSeedingRngFactory<TRng, TSeedingRng, TSeed> Create<TRng, TSeedingRng, TSeed>(IReproducibleRngFactory<TRng, TSeed> rngFactory, TSeedingRng seedSource)
            where TRng : IRng
            where TSeedingRng : IRng
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));
            if (seedSource == null)
                throw new ArgumentNullException(nameof(seedSource));

            return new AutoSeedingRngFactory<TRng, TSeedingRng, TSeed>(rngFactory, seedSource);
        }
    }
}
