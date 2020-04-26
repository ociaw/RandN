using System;

namespace Rand
{
    /// <summary>
    /// Creates instances of an <see cref="IReproducibleRngFactory"/> using automatically generated seeds.
    /// </summary>
    public sealed class AutoSeedingFactory<TRng> : IRngFactory<TRng>
        where TRng : IRng
    {
        private readonly IRng _seedSource;

        private readonly IReproducibleRngFactory<TRng> _rngFactory;

        private AutoSeedingFactory(IReproducibleRngFactory<TRng> rngFactory, IRng seedSource)
        {
            _rngFactory = rngFactory;
            _seedSource = seedSource;
        }

        public TRng Create()
        {
            int length = _rngFactory.MinimumSeedLength;
            Span<byte> seed = stackalloc byte[length];
            _seedSource.Fill(seed);
            return _rngFactory.Create(seed);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG factory and a Cryptographically Secure Seed Source.
        /// </summary>
        public static AutoSeedingFactory<TRng> Create(IReproducibleRngFactory<TRng> rngFactory)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));

            var seedSource = CryptoServiceProvider.Factory.Instance.Create();
            return new AutoSeedingFactory<TRng>(rngFactory, seedSource);
        }

        /// <summary>
        /// Creates an auto seeding RNG factory with the given RNG Factory and Seed Source.
        /// </summary>
        public static AutoSeedingFactory<TRng> Create(IReproducibleRngFactory<TRng> rngFactory, TRng seedSource)
        {
            if (rngFactory == null)
                throw new ArgumentNullException(nameof(rngFactory));
            if (seedSource == null)
                throw new ArgumentNullException(nameof(seedSource));

            return new AutoSeedingFactory<TRng>(rngFactory, seedSource);
        }
    }
}
