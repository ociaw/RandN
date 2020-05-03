using System;

namespace Rand.Tests
{
    internal sealed class StaticRng : IRng
    {
        private readonly UInt64 _state;

        public StaticRng(UInt64 state) => _state = state;

        public void Fill(Span<Byte> buffer)
        {
            while (buffer.Length >= sizeof(UInt64))
            {
                System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(buffer, NextUInt64());
                buffer = buffer.Slice(sizeof(UInt64));
            }

            if (buffer.Length == 0)
                return;

            Span<Byte> bytes = stackalloc Byte[8];
            System.Buffers.Binary.BinaryPrimitives.WriteUInt64LittleEndian(bytes, NextUInt64());
            bytes.Slice(0, buffer.Length).CopyTo(buffer);
        }

        public UInt32 NextUInt32() => (UInt32)NextUInt64();

        public UInt64 NextUInt64() => _state;

        public sealed class Factory : IReproducibleRngFactory<StaticRng, UInt64>
        {
            public StaticRng Create(UInt64 seed) => new StaticRng(seed);

            public UInt64 CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng => seedingRng.NextUInt64();
        }
    }
}
