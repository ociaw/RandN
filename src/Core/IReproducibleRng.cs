#if NET7_0_OR_GREATER
namespace RandN;

/// <summary>
/// An RNG that must be provided a seed to initialize itself. 
/// </summary>
public interface IReproducibleRng<out TRng, TSeed>
    where TRng : notnull, IRng, IReproducibleRng<TRng, TSeed>
{
    /// <summary>
    /// Creates a new <typeparamref name="TRng" /> using the specified seed.
    /// </summary>
    /// <param name="seed">The seed to create the RNG with.</param>
    /// <returns>A new <typeparamref name="TRng" /> instance.</returns>
    static abstract TRng Create(TSeed seed);

    /// <summary>
    /// Creates a seed of type <typeparamref name="TSeed"/>, that can then be used in the <see cref="Create(TSeed)"/>
    /// method to create a <typeparamref name="TRng"/>.
    /// </summary>
    /// <typeparam name="TSeedingRng">The type of the <see cref="IRng"/> used to create the seed.</typeparam>
    /// <param name="seedingRng">The <typeparamref name="TSeedingRng"/> used to create the seed.</param>
    /// <returns>A new <typeparamref name="TSeed"/> suitable for use in the <see cref="Create(TSeed)"/> method.</returns>
    static abstract TSeed CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : notnull, IRng;

    /// <summary>
    /// Creates a new <typeparamref name="TRng"/> using a seed created from <paramref name="seedingRng"/>.
    /// </summary>
    /// <param name="seedingRng">The RNG used to create the seed.</param>
    /// <returns>A new <typeparamref name="TRng"/> instance.</returns>
    static TRng Create<TSeedingRng>(TSeedingRng seedingRng)
        where TSeedingRng : notnull, IRng
    {
        var seed = TRng.CreateSeed(seedingRng);
        return TRng.Create(seed);
    }
}
#endif
