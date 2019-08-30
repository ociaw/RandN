using System;

namespace Cuhogaus
{
    /// <summary>
    /// A factory that produces seekable Random Number Generators.
    /// </summary>
    public interface ISeekableRngFactory : IRngFactory
    {
        /// <summary>
        /// Creates an <see cref="ISeekableRng"/> using the given state.
        /// </summary>
        /// <param name="state">The current state of the RNG.</param>
        ISeekableRng CreateWithState(ReadOnlySpan<Byte> state);
    }
}
