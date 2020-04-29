using System;

namespace Rand
{
    /// <summary>
    /// Creates instances of an <see cref="IReproducibleRngFactory"/> using automatically generated seeds.
    /// </summary>
    public sealed class AutoUInt64SeedingFactory<TRng> : IRngFactory<TRng>
        where TRng : IRng
    {
        private readonly IRng _seedSource;

        private readonly IReproducibleRngFactory<TRng, UInt64> _rngFactory;

        private AutoUInt64SeedingFactory(IReproducibleRngFactory<TRng, UInt64> rngFactory, IRng seedSource)
        {
            _rngFactory = rngFactory;
            _seedSource = seedSource;
        }

        public TRng Create()
        {
            var seed = _seedSource.NextUInt64();
            return _rngFactory.Create(seed);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG factory and a Cryptographically Secure Seed Source.
        /// </summary>
        public static AutoUInt64SeedingFactory<TRng> Create(IReproducibleRngFactory<TRng, UInt64> rngFactory)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));

            var seedSource = CryptoServiceProvider.Factory.Instance.Create();
            return new AutoUInt64SeedingFactory<TRng>(rngFactory, seedSource);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG Factory and Seed Source.
        /// </summary>
        public static AutoUInt64SeedingFactory<TRng> Create(IReproducibleRngFactory<TRng, UInt64> rngFactory, TRng seedSource)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));
            if (seedSource == null)
                throw new ArgumentNullException(nameof(seedSource));

            return new AutoUInt64SeedingFactory<TRng>(rngFactory, seedSource);
        }
    }
}
