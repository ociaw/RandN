using System;

namespace Cuhogaus
{
    /// <summary>
    /// Creates instances of an <see cref="IReproducibleRngFactory"/> using automatically generated seeds.
    /// </summary>
    public sealed class AutoSeedingFactory : IRngFactory
    {
        private readonly IRng _seedSource;

        private readonly IReproducibleRngFactory _rngFactory;

        private AutoSeedingFactory(IReproducibleRngFactory rngFactory, IRng seedSource)
        {
            _rngFactory = rngFactory;
            _seedSource = seedSource;
        }

        public IRng Create()
        {
            int length = _rngFactory.SeedLength;
            Span<byte> seed = stackalloc byte[length];
            return _rngFactory.Create(seed);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG factory and a Cryptographically Secure Seed Source.
        /// </summary>
        public static AutoSeedingFactory Create(IReproducibleRngFactory rngFactory)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));

            var seedSource = CryptoServiceProvider.Factory.Instance.Create();
            return new AutoSeedingFactory(rngFactory, seedSource);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG Factory and Seed Source.
        /// </summary>
        public static AutoSeedingFactory Create(IReproducibleRngFactory rngFactory, IRng seedSource)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));
            if (seedSource == null)
                throw new ArgumentNullException(nameof(seedSource));

            return new AutoSeedingFactory(rngFactory, seedSource);
        }
    }
}
