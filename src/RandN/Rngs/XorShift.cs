using System;
using RandN.RngHelpers;

// Algorithm based off of https://github.com/rust-random/rngs/tree/master/rand_xorshift and
// Marsaglia, George (July 2003). "Xorshift RNGs". Journal of Statistical Software. Vol. 8 (Issue 14).
// https://www.jstatsoft.org/v08/i14/paper

namespace RandN.Rngs
{
    /// <summary>
    /// A random number generator using the XOR Shift algorithm.
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
        public static XorShift Create((UInt32 x, UInt32 y, UInt32 z, UInt32 w) seed) => Create(seed.x, seed.y, seed.z, seed.w);

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

        /// <inheritdoc cref="IRngFactory{CryptoServiceProvider}" />
        public sealed class Factory : IReproducibleRngFactory<XorShift, (UInt32 x, UInt32 y, UInt32 z, UInt32 w)>
        {
            internal Factory() { }

            /// <inheritdoc />
            public XorShift Create((UInt32 x, UInt32 y, UInt32 z, UInt32 w) seed) => XorShift.Create(seed);

            /// <inheritdoc />
            public (UInt32 x, UInt32 y, UInt32 z, UInt32 w) CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng
            {
                return (
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32(),
                    seedingRng.NextUInt32()
                );
            }
        }
    }
}
