using System;
using RandN.Rngs;

namespace RandN;

/// <summary>
/// Creates instances of an <see cref="IRng"/> using automatically generated seeds.
/// </summary>
public sealed class RngSeeder<TRng, TSeedingRng, TSeed> : IRngFactory<TRng>, IDisposable
    where TRng : notnull, IRng
    where TSeedingRng : notnull, IRng
{
    private readonly TSeedingRng _seedSource;

    private readonly IReproducibleRngFactory<TRng, TSeed> _rngFactory;

    internal RngSeeder(IReproducibleRngFactory<TRng, TSeed> rngFactory, TSeedingRng seedSource)
    {
        _rngFactory = rngFactory;
        _seedSource = seedSource;
    }

    /// <summary>
    /// Creates an instance of <typeparamref name="TRng"/>.
    /// </summary>
    /// <returns>A seeded instance of <typeparamref name="TRng"/>.</returns>
    public TRng Create()
    {
        var seed = _rngFactory.CreateSeed(_seedSource);
        return _rngFactory.Create(seed);
    }

    /// <summary>   
    /// Disposes the seeding RNG if it implements <see cref="IDisposable"/>.
    /// </summary>
    public void Dispose()
    {
        if (_seedSource is IDisposable disposable)
            disposable.Dispose();
    }
}

/// <summary>
/// Creates instances of an <see cref="IRng"/> using automatically generated seeds.
/// </summary>
public static class RngSeeder
{
    /// <summary>
    /// Creates an auto seeding RNG factory with the given RNG factory and <see cref="SystemCryptoRng"/> as a seed source.
    /// </summary>
    public static RngSeeder<TRng, SystemCryptoRng, TSeed> Create<TRng, TSeed>(IReproducibleRngFactory<TRng, TSeed> rngFactory)
        where TRng : notnull, IRng
    {
        if (rngFactory == null)
            throw new ArgumentNullException(nameof(rngFactory));

        var seedSource = SystemCryptoRng.GetFactory().Create();
        return new RngSeeder<TRng, SystemCryptoRng, TSeed>(rngFactory, seedSource);
    }

    /// <summary>
    /// Creates an auto seeding RNG factory with the given RNG Factory and seed source.
    /// </summary>
    public static RngSeeder<TRng, TSeedingRng, TSeed> Create<TRng, TSeedingRng, TSeed>(IReproducibleRngFactory<TRng, TSeed> rngFactory, TSeedingRng seedSource)
        where TRng : notnull, IRng
        where TSeedingRng : notnull, IRng
    {
        if (rngFactory == null)
            throw new ArgumentNullException(nameof(rngFactory));
        if (seedSource == null)
            throw new ArgumentNullException(nameof(seedSource));

        return new RngSeeder<TRng, TSeedingRng, TSeed>(rngFactory, seedSource);
    }
}
