using System;
using System.Buffers.Binary;
using RandN.RngHelpers;

namespace RandN.Rngs
{
    /// <summary>
    /// XOR Shift algorithm for generating random numbers. Based off of the algorithm used in Rust's rand crate.
    /// </summary>
    public sealed class XorShift : IRng
    {
        private static readonly Factory _factory = new Factory();

        private UInt32 _x;
        private UInt32 _y;
        private UInt32 _z;
        private UInt32 _w;

        private XorShift(UInt32 x, UInt32 y, UInt32 z, UInt32 w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public static XorShift Create(UInt32 x, UInt32 y, UInt32 z, UInt32 w)
        {
            return new XorShift(x, y, z, w);
        }

        public static Factory GetFactory() => _factory;

        public UInt32 NextUInt32()
        {
            UInt32 t = _x ^ (_x << 11);
            _x = _y;
            _y = _z;
            _z = _w;
            _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
            return _w;
        }

        public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);

        public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

        public ReadOnlySpan<Byte> GetState()
        {
            Span<Byte> state = new Byte[4 * sizeof(UInt32)];
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(UInt32) * 0), _x);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(UInt32) * 1), _y);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(UInt32) * 2), _z);
            BinaryPrimitives.WriteUInt32LittleEndian(state.Slice(sizeof(UInt32) * 3), _w);
            return state;
        }

        public sealed class Factory : IReproducibleRngFactory<XorShift, Seed>
        {
            internal Factory() { }

            public Int32 SeedLength => sizeof(UInt32) * 4;

            public XorShift Create(Seed seed) => XorShift.Create(seed.X, seed.Y, seed.Z, seed.W);

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
            public Seed(UInt32 x, UInt32 y, UInt32 z, UInt32 w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public UInt32 X { get; }

            public UInt32 Y { get; }

            public UInt32 Z { get; }

            public UInt32 W { get; }
        }
    }
}
