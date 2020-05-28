﻿using System;
using System.Buffers.Binary;
using RandN.RngHelpers;

// Algorithm based off of https://github.com/rust-random/rngs/tree/master/rand_xorshift and
// Marsaglia, George (July 2003). "Xorshift RNGs". Journal of Statistical Software. Vol. 8 (Issue 14).
// https://www.jstatsoft.org/v08/i14/paper

namespace RandN.Rngs
{
    /// <summary>
    /// XOR Shift algorithm for generating random numbers.
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

        /// <summary>
        /// Creates an XorShift RNG using the given seed.
        /// </summary>
        public static XorShift Create(UInt32 x, UInt32 y, UInt32 z, UInt32 w)
        {
            // XorShift can't be seeded with all zeros, but we don't want to throw an exception
            // since it's possible for a random seed to be all zeroes, and it would be inconsistent
            // with other RNGs. Instead, we seed it with a constant.
            if (x == 0 && y == 0 && z == 0 && w == 0)
                return new XorShift(0xBAD_5EED, 0xBAD_5EED, 0xBAD_5EED, 0xBAD_5EED);

            return new XorShift(x, y, z, w);
        }

        /// <summary>
        /// Creates an XorShift RNG using the given seed.
        /// </summary>
        public static XorShift Create(Seed seed) => XorShift.Create(seed.X, seed.Y, seed.Z, seed.W);

        /// <summary>
        /// Gets the XorShift factory.
        /// </summary>
        public static Factory GetFactory() => _factory;

        /// <inheritdoc />
        public UInt32 NextUInt32()
        {
            UInt32 t = _x ^ (_x << 11);
            _x = _y;
            _y = _z;
            _z = _w;
            _w = _w ^ (_w >> 19) ^ (t ^ (t >> 8));
            return _w;
        }

        /// <inheritdoc />
        public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);

        /// <inheritdoc />
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

            /// <inheritdoc />
            public XorShift Create(Seed seed) => XorShift.Create(seed);

            /// <inheritdoc />
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
