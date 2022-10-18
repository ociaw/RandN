// Copyright 2018 Developers of the RandN project.
// Copyright 2017 Paul Dicker.
// Copyright 2014-2017 Melissa O'Neill and PCG Project contributors
//
// Licensed under the MIT license
// <LICENSE-MIT or https://opensource.org/licenses/MIT>.
// This file may not be copied, modified, or distributed
// except according to those terms.

// Read more about PCG here: https://www.pcg-random.org/paper.html
// And read the paper here: https://www.pcg-random.org/pdf/hmc-cs-2014-0905.pdf

using System;
using RandN.Implementation;

namespace RandN.Rngs;

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
/// comprising 64 bits of state and a 64 bit stream selector. These are both set
/// by <see cref="Factory"/>, using a 127-bit seed.
/// </remarks>
public sealed class Pcg32 : IRng
{
    private const UInt64 Multiplier = 6364136223846793005;

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

    /// <summary>
    /// Gets the <see cref="XorShift" /> factory.
    /// </summary>
    public static Factory GetFactory() => new();

    /// <inheritdoc />
    public UInt32 NextUInt32()
    {
        var state = _state;
        Step();

        // Output function XSH RR: xorshift high (bits), followed by a random rotate
        // Constants are for 64-bit state, 32-bit output
        const Int32 rotate = 59; // 64 - 5
        const Int32 xShift = 18; // (5 + 32) / 2
        const Int32 spare = 27; // 64 - 32 - 5

        var rotation = unchecked((Int32)(state >> rotate));
        var xs = unchecked((UInt32)(((state >> xShift) ^ state) >> spare));
        // Rotate Right
        return (xs >> rotation) | (xs << (32 - rotation));
    }

    /// <inheritdoc />
    public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);

    /// <inheritdoc />
    public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

    private void Step() => _state = unchecked(_state * Multiplier + _increment);

    /// <summary>
    /// Produces Pcg32 RNGs and seeds.
    /// </summary>
    public readonly struct Factory : IReproducibleRngFactory<Pcg32, Seed>
    {
        /// <inheritdoc />
        public Pcg32 Create(Seed seed) => Pcg32.Create(seed.State, seed.Stream);

        /// <inheritdoc />
        public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng => new(seedingRng.NextUInt64(), seedingRng.NextUInt64());
    }

    /// <summary>
    /// A 127-bit seed to initialize the state and select a stream.
    /// </summary>
    public readonly struct Seed
    {
        /// <summary>
        /// Creates a new Pcg32 seed with the given state and stream.
        /// </summary>
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
        /// Our implementation is consistent with the reference PCG implementation, where the
        /// highest bit is discarded. However, the some implementations discard the lowest bit instead
        /// (<see href="https://github.com/rust-random/rand/blob/bf8b5a98ac95da19953f0b3b0f6da6ec8eda940b/rand_pcg/src/pcg64.rs#L87">such as Rust's Rand</see>)
        /// which results in surprising behavior for someone using incremental stream ids. A
        /// stream id such as this can be right shifted once if identical results are desired.
        /// </remarks>
        public UInt64 Stream { get; }
    }
}