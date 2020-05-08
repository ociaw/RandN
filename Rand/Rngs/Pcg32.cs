// Copyright 2018 Developers of the Rand project.
// Copyright 2017 Paul Dicker.
// Copyright 2014-2017 Melissa O'Neill and PCG Project contributors
//
// Licensed under the Apache License, Version 2.0 <LICENSE-APACHE or
// https://www.apache.org/licenses/LICENSE-2.0> or the MIT license
// <LICENSE-MIT or https://opensource.org/licenses/MIT>, at your
// option. This file may not be copied, modified, or distributed
// except according to those terms.

using Rand.RngHelpers;
using System;

namespace Rand.Rngs
{
    /// <summary>
    /// A PCG random number generator (XSH RR 64/32 (LCG) variant).
    /// </summary>
    /// <remarks>
    /// Permuted Congruential Generator with 64-bit state, internal Linear
    /// Congruential Generator, and 32-bit output via "xorshift high (bits),
    /// random rotation" output function.
    ///
    /// This is a 64-bit LCG with explicitly chosen stream with the PCG-XSH-RR
    /// output function. This combination is the standard pcg32.
    ///
    /// Despite the name, this implementation uses 16 bytes (128 bit) space
    /// comprising 64 bits of state and 64 bits stream selector. These are both set
    /// by <see cref="Pcg32.Factory"/>, using a 128-bit seed.
    /// </remarks>
    public sealed class Pcg32 : IRng
    {
        private static readonly Factory _factory = new Factory();

        private const UInt64 MULTIPLIER = 6364136223846793005;

        private UInt64 _state;
        private readonly UInt64 _increment;

        private Pcg32(UInt64 state, UInt64 increment)
        {
            _increment = increment;
            _state = unchecked(state + _increment);
            Step();
        }

        /// <summary>
        /// Creates a new <see cref="Pcg32"/> instance, treating <paramref name="state"/> and <paramref name="stream"/>
        /// the same way as the reference implementation of PCG.
        /// </summary>
        /// <param name="state">The 64 bit state.</param>
        /// <param name="stream">The 63 bit stream id - the highest bit is ignored.</param>
        public static Pcg32 Create(UInt64 state, UInt64 stream)
        {
            // The increment must be odd.
            var increment = (stream << 1) | 1;
            return new Pcg32(state, increment);
        }

        public static Factory GetFactory() => _factory;

        public UInt32 NextUInt32()
        {
            var state = _state;
            Step();

            // Output function XSH RR: xorshift high (bits), followed by a random rotate
            // Constants are for 64-bit state, 32-bit output
            const Int32 ROTATE = 59; // 64 - 5
            const Int32 XSHIFT = 18; // (5 + 32) / 2
            const Int32 SPARE = 27; // 64 - 32 - 5

            var rotation = (Int32)(state >> ROTATE);
            var xShift = (UInt32)(((state >> XSHIFT) ^ state) >> SPARE);
            // Rotate Right
            return (xShift >> rotation) | (xShift << (32 - rotation));
        }

        public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);

        public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

        private void Step() => _state = unchecked(_state * MULTIPLIER + _increment);

        public sealed class Factory : IReproducibleRngFactory<Pcg32, Seed>
        {
            internal Factory() { }

            public Pcg32 Create(Seed seed) => Pcg32.Create(seed.State, seed.Stream);

            public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng => new Seed(seedingRng.NextUInt64(), seedingRng.NextUInt64());
        }

        /// <summary>
        /// A 127-bit seed to initialize the state and select a stream.
        /// </summary>
        public readonly struct Seed
        {
            public Seed(UInt64 state, UInt64 stream)
            {
                State = state;
                Stream = stream;
            }

            /// <summary>
            /// The 64-bit seed state.
            /// </summary>
            public UInt64 State { get; }

            /// <summary>
            /// The 63 bit stream id. The highest bit is ignored.
            /// </summary>
            /// <remarks>
            /// Our implementation is consistent with the reference PCG implementation, where the highest bit is discarded.
            /// However, the rust implementation discards the lowest bit
            /// (<see href="https://github.com/rust-random/rand/blob/bf8b5a98ac95da19953f0b3b0f6da6ec8eda940b/rand_pcg/src/pcg64.rs#L87" />)
            /// which results in surprising behavior for someone using incremental stream ids. A rust stream id can be right shifted once if
            /// identical results are desired.
            /// </remarks>
            public UInt64 Stream { get; }
        }
    }
}
