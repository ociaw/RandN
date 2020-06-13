using System;

namespace RandN.RngHelpers
{
    /// <summary>
    /// Represents a block RNG with a result type of <typeparamref name="TItem"/>.
    /// </summary>
    public interface IBlockRngCore<TItem>
    {
        /// <summary>
        /// The length of the generated block.
        /// </summary>
        public Int32 BlockLength { get; }

        /// <summary>
        /// Generates the next block and fills <paramref name="results"/> with the values.
        /// </summary>
        /// <param name="results">The destination to fill with random values. Must be at least <see cref="BlockLength"/> long.</param>
        public void Generate(Span<TItem> results);
    }
}
