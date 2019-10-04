using System;

namespace Cuhogaus
{
    /// <summary>
    /// A factory that produces reproducible Random Number Generators.
    /// </summary>
    public interface IReproducibleRngFactory
    {
        /// <summary>
        /// The length of seed the factory needs in bytes.
        /// </summary>
        Int32 SeedLength { get; }

        /// <summary>
        /// Creates a new <see cref="IRng"/> using the specified seed.
        /// </summary>
        /// <param name="seed">The seed to create the RNG with.</param>
        /// <returns>A new <see cref="IRng"/> instance.</returns>
        /// <remarks>
        /// Extra bytes will be truncated, while missing bytes will be zero-padded.
        /// </remarks>
        IRng Create(ReadOnlySpan<Byte> seed);
    }
}
