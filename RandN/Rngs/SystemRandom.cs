using System;

namespace RandN.Rngs
{
    /// <summary>
    /// Wraps <see cref="Random"/>.
    /// </summary>
    public sealed class SystemRandom : IRng
    {
        private static readonly Factory _factory = new Factory();

        private readonly Random _rng;

        public SystemRandom(Random rng) => _rng = rng;

        public static SystemRandom Create(Int32 seed) => new SystemRandom(new Random(seed));

        public static Factory GetFactory() => _factory;

        public UInt32 NextUInt32()
        {
            Span<Byte> buffer = stackalloc Byte[4];
            Fill(buffer);
            return BitConverter.ToUInt32(buffer);
        }

        public UInt64 NextUInt64()
        {
            Span<Byte> buffer = stackalloc Byte[8];
            Fill(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        public void Fill(Span<Byte> buffer) => _rng.NextBytes(buffer);

        public sealed class Factory : IReproducibleRngFactory<SystemRandom, Int32>
        {
            internal Factory() { }

            public SystemRandom Create(Int32 seed) => SystemRandom.Create(seed);

            public Int32 CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng => (Int32)seedingRng.NextUInt32();
        }
    }
}
