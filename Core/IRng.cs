using System;

namespace RandN
{
    /// <summary>
    /// A Random Number Generator (RNG)
    /// </summary>
    public interface IRng
    {
        /// <summary>
        /// Returns the next 32 bits in the sequence as a UInt32.
        /// </summary>
        UInt32 NextUInt32();

        /// <summary>
        /// Returns the next 64 bits in the sequence as a UInt64.
        /// </summary>
        UInt64 NextUInt64();

        /// <summary>
        /// Completely fills the span with random bytes.
        /// </summary>
        /// <param name="buffer">The span to fill.</param>
        void Fill(Span<Byte> buffer);
    }
}
