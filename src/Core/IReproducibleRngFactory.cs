namespace RandN
{
    /// <summary>
    /// A factory that produces reproducible Random Number Generators.
    /// </summary>
    public interface IReproducibleRngFactory<TRng, TSeed>
        where TRng : notnull, IRng
    {
        /// <summary>
        /// Creates a new <typeparamref name="TRng" /> using the specified seed.
        /// </summary>
        /// <param name="seed">The seed to create the RNG with.</param>
        /// <returns>A new <typeparamref name="TRng" /> instance.</returns>
        TRng Create(TSeed seed);

        /// <summary>
        /// Creates a seed of type <typeparamref name="TSeed"/>, that can then be used in the <see cref="Create(TSeed)"/>
        /// method to create a <typeparamref name="TRng"/>.
        /// </summary>
        /// <typeparam name="TSeedingRng">The type of the <see cref="IRng"/> used to create the seed.</typeparam>
        /// <param name="seedingRng">The <typeparamref name="TSeedingRng"/> used to create the seed.</param>
        /// <returns>A new <typeparamref name="TSeed"/> suitable for use in the <see cref="Create(TSeed)"/> method.</returns>
        TSeed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng;
    }
}
