using System;

namespace Rand
{
    /// <summary>
    /// An RNG that is seekable - i.e. it can be fast forwarded to any point in the stream.
    /// </summary>
    public interface ISeekableRng : IRng
    {
        /// <summary>
        /// Gets the current state of the RNG.
        /// </summary>
        /// <remarks>
        /// While this is called state, it doesn't necessarily return the full state of the RNG.
        /// It mearly needs to be enough so that the full current state can be reached.
        /// </remarks>
        ReadOnlySpan<Byte> GetState();
    }
}
