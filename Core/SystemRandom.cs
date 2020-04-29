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

        public uint NextUInt32()
        {
            Span<byte> buffer = stackalloc byte[4];
            Fill(buffer);
            return BitConverter.ToUInt32(buffer);
        }

        public ulong NextUInt64()
        {
            Span<byte> buffer = stackalloc byte[8];
            Fill(buffer);
            return BitConverter.ToUInt64(buffer);
        }

        public void Fill(Span<byte> buffer) => _rng.NextBytes(buffer);

        public sealed class Factory : IReproducibleRngFactory<SystemRandom, Int32>
        {
            public static Factory Instance { get; } = new Factory();

            public SystemRandom Create(Int32 seed)
            {
                var rng = new Random(seed);
                return new SystemRandom(rng);
            }
        }
    }
}
