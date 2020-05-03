using System;

namespace Rand.Tests
{
    internal sealed class SequentialRng : IRng
    {
        public SequentialRng(UInt64 state) => State = state;

        public UInt64 State { get; set; }

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

        public UInt64 NextUInt64()
        {
            var value = State;
            State = unchecked(State + 1);
            return value;
        }

        public sealed class Factory : IReproducibleRngFactory<SequentialRng, UInt64>
        {
            public SequentialRng Create(UInt64 seed) => new SequentialRng(seed);

            public UInt64 CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng => seedingRng.NextUInt64();
        }
    }
}
