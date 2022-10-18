using System;

namespace RandN;

public class MockDistribution<T> : IDistribution<T>
{
    public MockDistribution(T item, Boolean success = true)
    {
        Item = item;
        Success = success;
    }

    public T Item { get; }

    public Boolean Success { get; }

    public T Sample<TRng>(TRng rng) where TRng : notnull, IRng
    {
        if (!Success)
        {
            throw new InvalidOperationException(
                "You cannot sample from a distribution that has been set up to fail.");
        }

        return Item;
    }

    public Boolean TrySample<TRng>(TRng rng, out T result) where TRng : notnull, IRng
    {
        result = Item;
        return Success;
    }
}