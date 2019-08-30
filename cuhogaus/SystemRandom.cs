using System;

namespace Cuhogaus
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

        public void Fill(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            _rng.NextBytes(buffer);
        }

        public void Fill(Span<byte> buffer) => _rng.NextBytes(buffer);

        public sealed class Factory : IRngFactory
        {
            public static Factory Instance { get; } = new Factory();

            public Int32 SeedLength => 4;

            public IRng Create(ReadOnlySpan<byte> seed)
            {
                int num = 0;
                for (int i = 0; i < Math.Min(sizeof(int), seed.Length); i++)
                    num |= seed[i] << (8 * i);

                var rng = new Random(num);
                return new SystemRandom(rng);
            }
        }
    }
}
