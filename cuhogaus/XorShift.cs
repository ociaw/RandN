using System;
using System.Buffers.Binary;

namespace Cuhogaus
{
    /// <summary>
    /// XOR Shift algorithm for generating random numbers. Based off of the algorithm used in Rust's rand crate.
    /// </summary>
    public sealed class XorShift : ISeekableRng
    {
        private uint _x;
        private uint _y;
        private uint _z;
        private uint _w;

        private XorShift(uint x, uint y, uint z, uint w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public uint NextUInt32()
        {
            uint t = _x ^ (_x << 11);
            _x = _y;
            _y = _z;
            _z = _w;
            _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
            return _w;
        }

        public ulong NextUInt64() => UInt32Filler.ToUInt64(NextUInt32(), NextUInt32());

        public void Fill(Span<Byte> buffer) => UInt32Filler.Fill(buffer, NextUInt32);

        public ReadOnlySpan<byte> GetState()
        {
            Span<byte> state = new byte[4 * sizeof(uint)];
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(uint) * 0), _x);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(uint) * 1), _y);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(uint) * 2), _z);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(uint) * 3), _w);
            return state;
        }

        public sealed class Factory : IReproducibleRngFactory<XorShift>, ISeekableRngFactory<XorShift>
        {
            public static Factory Instance { get; } = new Factory();

            public Int32 MinimumSeedLength => sizeof(uint) * 4;

            public Int32 MaximumSeedLength => sizeof(uint) * 4;

            public Int32 SeedStride => sizeof(uint) * 4;

            public Int32 StateLength => sizeof(byte) * 4;

            public XorShift Create(ReadOnlySpan<byte> seed) => CreateWithState(seed); // For XorShift, the seed is equivalent to the state.

            public XorShift CreateWithState(ReadOnlySpan<byte> state)
            {
                BinaryPrimitives.TryReadUInt32LittleEndian(state.Slice(sizeof(uint) * 0), out uint x);
                BinaryPrimitives.TryReadUInt32LittleEndian(state.Slice(sizeof(uint) * 1), out uint y);
                BinaryPrimitives.TryReadUInt32LittleEndian(state.Slice(sizeof(uint) * 2), out uint z);
                BinaryPrimitives.TryReadUInt32LittleEndian(state.Slice(sizeof(uint) * 3), out uint w);

                return new XorShift(x, y, z, w);
            }
        }
    }
}
