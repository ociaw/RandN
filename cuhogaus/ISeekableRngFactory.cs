using System;

namespace Cuhogaus
{
    /// <summary>
    /// A factory that produces seekable Random Number Generators.
    /// </summary>
    public interface ISeekableRngFactory<TRng> : IReproducibleRngFactory<TRng>
        where TRng : ISeekableRng
    {
        /// <summary>
        /// The length of the state in bytes.
        /// </summary>
        Int32 StateLength { get; }

        /// <summary>
        /// Creates an <see cref="ISeekableRng"/> using the given state.
        /// </summary>
        /// <param name="state">The current state of the RNG.</param>
        TRng CreateWithState(ReadOnlySpan<Byte> state);
    }
}
