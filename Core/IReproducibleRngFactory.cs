namespace Rand
{
    /// <summary>
    /// A factory that produces reproducible Random Number Generators.
    /// </summary>
    public interface IReproducibleRngFactory<TRng, TSeed>
        where TRng : IRng
    {
        /// <summary>
        /// Creates a new <see cref="IRng"/> using the specified seed.
        /// </summary>
        /// <param name="seed">The seed to create the RNG with.</param>
        /// <returns>A new <see cref="IRng"/> instance.</returns>
        TRng Create(TSeed seed);
    }
}
