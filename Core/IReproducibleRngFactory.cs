using System;

namespace Rand
{
    /// <summary>
    /// A factory that produces reproducible Random Number Generators.
    /// </summary>
    public interface IReproducibleRngFactory<TRng>
        where TRng : IRng
    {
        /// <summary>
        /// The minimum length of seed in bytes.
        /// </summary>
        Int32 MinimumSeedLength { get; }

        /// <summary>
        /// The maximum length of the seed in bytes.
        /// </summary>
        Int32 MaximumSeedLength { get; }

        /// <summary>
        /// The length of the seed must a multiple of this.
        /// </summary>
        Int32 SeedStride { get; }

        /// <summary>
        /// Creates a new <see cref="IRng"/> using the specified seed.
        /// </summary>
        /// <param name="seed">The seed to create the RNG with.</param>
        /// <returns>A new <see cref="IRng"/> instance.</returns>
        TRng Create(ReadOnlySpan<Byte> seed);
    }
}
