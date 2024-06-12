// Copyright 2023 Developers of the RandN project.
// Copyright 2023 Chris Marc Dailey (nitz) <https://cmd.wtf>
// Copyright 2023 Tony Finch <dot@dotat.at>
// Copyright 2017 Paul Dicker.
// Copyright 2014-2017 Melissa O'Neill and PCG Project contributors
//
// Licensed under the MIT license
// <LICENSE-MIT or https://opensource.org/licenses/MIT>.
// This file may not be copied, modified, or distributed
// except according to those terms.
//
// Work from Tony Finch licensed under 0BSD

// Read more about PCG here: https://www.pcg-random.org/paper.html
// And read the paper here: https://www.pcg-random.org/pdf/hmc-cs-2014-0905.pdf
// And read about the DSXM here: https://dotat.at/@/2023-06-21-pcg64-dxsm.html

using System;

using RandN.Implementation;

namespace RandN.Rngs;

#if NET7_0_OR_GREATER

/// <summary>
/// A PCG random number generator (DSXM 64 variant).
/// </summary>
/// <remarks>
/// Permuted Congruential Generator with 128-bit state, internal Linear
/// Congruential Generator, with a 64-bit output via "double xor shift multiply"
/// output function.
///
/// This is a 128-bit LCG with explicitly chosen stream with the PCG-DXSM
/// output function.
///
/// Despite the name, this implementation uses 32 bytes (256 bit) space
/// comprising 128 bits of state and a 128 bit stream selector. These are both set
/// by <see cref="Factory"/>, using a 255-bit seed.
/// </remarks>
public sealed class Pcg64Dxsm : IRng
{
    private const UInt64 Multiplier = 15750249268501108917UL;

    private UInt128 _state;
    private readonly UInt128 _increment;

    private Pcg64Dxsm(UInt128 state, UInt128 increment)
    {
        _increment = increment;
        _state = unchecked(state + _increment);
        Step();
    }

    /// <summary>
    /// Creates a new <see cref="Pcg64Dxsm"/> instance, treating <paramref name="state"/> and <paramref name="stream"/>
    /// the same way as the reference implementation of PCG.
    /// </summary>
    /// <param name="state">The 128 bit state.</param>
    /// <param name="stream">The 127 bit stream id - the highest bit is ignored.</param>
    public static Pcg64Dxsm Create(UInt128 state, UInt128 stream)
    {
        // The increment must be odd.
        var increment = (stream << 1) | 1;
        return new Pcg64Dxsm(state, increment);
    }

    /// <summary>
    /// Gets the <see cref="Pcg64Dxsm" /> factory.
    /// </summary>
    public static Factory GetFactory() => new();

    /// <inheritdoc />
    public UInt32 NextUInt32() => Filler.NextUInt32ViaUInt64(this);

    /// <inheritdoc />
    public UInt64 NextUInt64()
    {
        var state = _state;
        Step();

        // Output function: DXSM: double xor shift multiply
        // Constants are for 128-bit state, 64-bit output
        UInt64 hi = (UInt64)(state >> 64);
        UInt64 lo = (UInt64)(state | 1);

        unchecked
        {
            hi ^= hi >> 32;
            hi *= Multiplier;
            hi ^= hi >> 48;
            hi *= lo;
        }

        return hi;
    }

    /// <inheritdoc />
    public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

    private void Step() => _state = unchecked(_state * Multiplier + _increment);

    /// <summary>
    /// Produces Pcg64Dxsm RNGs and seeds.
    /// </summary>
    public readonly struct Factory : IReproducibleRngFactory<Pcg64Dxsm, Seed>
    {
        /// <inheritdoc />
        public Pcg64Dxsm Create(Seed seed) => Pcg64Dxsm.Create(seed.State, seed.Stream);

        /// <inheritdoc />
        public Seed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng
            => new(Filler.NextUInt128ViaUInt64(seedingRng), Filler.NextUInt128ViaUInt64(seedingRng));
    }

    /// <summary>
    /// A 127-bit seed to initialize the state and select a stream.
    /// </summary>
    public readonly struct Seed
    {
        /// <summary>
        /// Creates a new Pcg64Dxsm seed with the given state and stream.
        /// </summary>
        public Seed(UInt128 state, UInt128 stream)
        {
            State = state;
            Stream = stream;
        }

        /// <summary>
        /// The 128-bit seed state.
        /// </summary>
        public UInt128 State { get; }

        /// <summary>
        /// The 127 bit stream id. The lowest bit is ignored.
        /// </summary>
        /// <remarks>
        /// Our implementation is consistent with Tony Finch's PCG64 DXSM implementation, where the
        /// highest bit is discarded. However, the some implementations discard the lowest bit instead
        /// (<see href="https://github.com/rust-random/rand/blob/bf8b5a98ac95da19953f0b3b0f6da6ec8eda940b/rand_pcg/src/pcg64.rs#L87">such as Rust's Rand</see>)
        /// which results in surprising behavior for someone using incremental stream ids. A
        /// stream id such as this can be right shifted once if identical results are desired.
        /// </remarks>
        public UInt128 Stream { get; }
    }
}

#endif // NET7_0_OR_GREATER
