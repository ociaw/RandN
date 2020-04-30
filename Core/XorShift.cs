using System;
using System.Buffers.Binary;

namespace Rand
{
    /// <summary>
    /// XOR Shift algorithm for generating random numbers. Based off of the algorithm used in Rust's rand crate.
    /// </summary>
    public sealed class XorShift : IRng
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

        public sealed class Factory : IReproducibleRngFactory<XorShift, Seed>
        {
            public static Factory Instance { get; } = new Factory();

            public Int32 SeedLength => sizeof(uint) * 4;

            public XorShift Create(Seed seed)
            {
                return new XorShift(seed.X, seed.Y, seed.Z, seed.W);
            }

            public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                return new Seed(
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32()
                );
            }
        }

        public readonly struct Seed
        {
            public Seed(uint x, uint y, uint z, uint w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public uint X { get; }

            public uint Y { get; }

            public uint Z { get; }

            public uint W { get; }
        }
    }
}
