using System;

namespace Rand
{
    /// <summary>
    /// Wraps <see cref="Random"/>.
    /// </summary>
    public sealed class SystemRandom : IRng
    {
        private readonly Random _rng;

        private SystemRandom(Random rng)
        {
            _rng = rng;
        }

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
            public static Factory Instance { get; } = new Factory();

            public SystemRandom Create(Int32 seed)
            {
                var rng = new Random(seed);
                return new SystemRandom(rng);
            }

            public Int32 CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                return (Int32)seedingRng.NextUInt32();
            }
        }
    }
}
