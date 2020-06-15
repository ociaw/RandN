namespace RandN
{
    /// <summary>
    /// An RNG that is seekable - i.e. it can be fast forwarded to any point in the stream.
    /// </summary>
    public interface ISeekableRng<TCounter> : IRng
    {
        /// <summary>
        /// The current position of the RNG.
        /// </summary>
        TCounter Position { get; set; }
    }
}
