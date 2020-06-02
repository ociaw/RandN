using System;
using RandN.Rngs;

namespace RandN
{
    /// <summary>
    /// A non-cryptographically secure RNG with good performance and output quality, while requiring limited memory.
    /// </summary>
    public sealed class SmallRng : IRng
    {
        private static readonly Factory _factory = new Factory();
        private readonly Pcg32 _wrapped;

        private SmallRng(Pcg32 wrapped) => _wrapped = wrapped;

        /// <summary>
        /// Creates a <see cref="StandardRng"/>.
        /// </summary>
        public static SmallRng Create()
        {
            var rng = Pcg32.GetFactory().Create(ThreadLocalRng.Instance);
            return new SmallRng(rng);
        }

        /// <summary>
        /// Gets the <see cref="StandardRng"/> factory.
        /// </summary>
        public static Factory GetFactory() => _factory;

        /// <inheritdoc />
        public void Fill(Span<Byte> buffer) => _wrapped.Fill(buffer);

        /// <inheritdoc />
        public UInt32 NextUInt32() => _wrapped.NextUInt32();

        /// <inheritdoc />
        public UInt64 NextUInt64() => _wrapped.NextUInt64();

        /// <inheritdoc cref="IRngFactory{StandardRng}" />
        public sealed class Factory : IRngFactory<SmallRng>
        {
            internal Factory() { }

            /// <inheritdoc />
            public SmallRng Create() => SmallRng.Create();
        }
    }
}
