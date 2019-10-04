using System;

namespace Cuhogaus
{
    /// <summary>
    /// A Random Number Generator (RNG)
    /// </summary>
    public interface IRng
    {
        /// <summary>
        /// Returns the next 32 bits in the sequence as a UInt32.
        /// </summary>
        uint NextUInt32();

        /// <summary>
        /// Returns the next 64 bits in the sequence as a UInt64.
        /// </summary>
        ulong NextUInt64();

        /// <summary>
        /// Completely fills the array with random bytes.
        /// </summary>
        /// <param name="buffer">The array to fill.</param>
        void Fill(Byte[] buffer);

        /// <summary>
        /// Completely fills the span with random bytes.
        /// </summary>
        /// <param name="buffer">The span to fill.</param>
        void Fill(Span<Byte> buffer);
    }
}
