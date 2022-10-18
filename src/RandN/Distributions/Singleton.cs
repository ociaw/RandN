using System;

namespace RandN.Distributions;

/// <summary>
/// A distribution containing a single value.
/// </summary>
public static class Singleton
{
    /// <summary>
    /// Creates a singleton distribution containing a single value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value.</param>
    public static Singleton<T> New<T>(T value) => new Singleton<T>(value);
}

/// <summary>
/// A distribution containing a single value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public sealed class Singleton<T> : IPortableDistribution<T>
{
    private readonly T _value;

    internal Singleton(T value)
    {
        _value = value;
    }

    /// <inheritdoc />
    public T Sample<TRng>(TRng rng) where TRng : notnull, IRng => _value;

    /// <inheritdoc />
    public Boolean TrySample<TRng>(TRng rng, out T result) where TRng : notnull, IRng
    {
        result = _value;
        return true;
    }
}