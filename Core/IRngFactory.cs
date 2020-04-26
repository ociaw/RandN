namespace Rand
{
    /// <summary>
    /// A factory that produces Random Number Generators.
    /// </summary>
    public interface IRngFactory<TRng>
    {
        /// <summary>
        /// Creates a new <see cref="TRng"/>.
        /// </summary>
        /// <returns>A new <see cref="TRng"/> instance.</returns>
        TRng Create();
    }
}
