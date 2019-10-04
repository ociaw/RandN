namespace Cuhogaus
{
    /// <summary>
    /// A factory that produces Random Number Generators.
    /// </summary>
    public interface IRngFactory
    {
        /// <summary>
        /// Creates a new <see cref="IRng"/>.
        /// </summary>
        /// <returns>A new <see cref="IRng"/> instance.</returns>
        IRng Create();
    }
}
