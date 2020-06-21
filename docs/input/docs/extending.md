Title: Extending RandN
---

It's easy to extend RandN with both new RNG algorithms and new distributions. RandN includes many
utility methods to make creating new RNGs and distributions easy under the `RandN.Implementation`
namespace.

# Implementing an RNG

To add a new RNG algorithm, you just need to implement `IRng`.
For example, here is the code for `StepRng`, which is used in tests:

``` csharp
using RandN;
using RandN.Implementation;

public sealed class StepRng : IRng
{
    public StepRng(UInt64 state) => State = state;

    public UInt64 State { get; set; }

    public UInt64 Increment { get; set; } = 1;

    public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

    public UInt32 NextUInt32() => (UInt32)NextUInt64();

    public UInt64 NextUInt64()
    {
        var value = State;
        State = unchecked(State + Increment);
        return value;
    }
}
```

If the algorithm is cryptographically secure, be sure to implement the marker interface
`ICryptoRng` as well.

You'll probably want to implement an RNG factory as well - either `IReproducibleRngFactory` or
`IRngFactory`, depending on whether or not you want a reproducible RNG.

## Reproducible RNG Factory

Implement `IReproducibleRngFactory<TRng, TSeed>`, where `TSeed` is the type the factory uses for
RNG seeding. It has two methods - `CreateSeed` to create a seed from another RNG, and `Create`,
which uses that seed to create an RNG. Mersenne Twister can use a 64-bit integer as a seed, as
demonstrated below:

``` csharp
public readonly struct MersenneTwisterFactory : IReproducibleRngFactory<Mt1993764, UInt64>
{
    public Mt1993764 Create(UInt64 seed)
    {
        return Mt1993764.Create(seed);
    }

    public UInt64 CreateSeed<TSeedingRng>(TSeedingRng seedingRng)
    {
        return seedingRng.NextUInt64();
    }
}
```

## Non-Reproducible RNG Factory

Implement `IRngFactory<TRng>` - this is very simple since it just has one parameter-less method,
`Create()`. If the underlying RNG needs to be seeded, the best source is usually `ThreadLocalRng`,
which is guaranteed to be cryptographically secure and thread safe.

For example, here's a simplified implementation of `StandardRng`'s factory, which uses ChaCha8
internally:

``` csharp
public readonly struct StandardRngFactory : IRngFactory<ChaCha>
{
    public ChaCha Create()
    {
        return ChaCha.GetChaCha8Factory().Create(ThreadLocalRng.Instance);
    }
}
```

# Implementing a Distribution

Implement `IDistribution<TResult>`, where `TResult` is the type that the distribution returns
from sampling. There are two methods to implement `Sample`, and `TrySample`. How these methods are
implemented depends on whether or not an attempt to sample can fail. For example, uniform `Int32`
distributions can fail if the range doesn't divide cleanly into a 32 bit number, and the RNG may
need to be called multiple times until it returns a value in range. This may not be acceptable in
some scenarios, so we provide `TrySample` to avoid blocking too long the RNG.

Here's an example of a distribution that rejects all positive `Int32`s; note how `Sample` blocks
until it gets an acceptable number:

``` csharp
public readonly struct NoPositiveInt32 : IDistribution<Int32>
{
    public Boolean TrySample<TRng>(TRng rng, out Int32 result) where TRng : IRng
    {
        result = (Int32)rng.NextUInt32();
        return result <= 0;
    }

    public Int32 Sample<TRng>(TRng rng) where TRng : IRng
    {
        while (true)
        {
            if (TrySample(rng, out Int32 result))
                return result;
        }
    }
}
```

On the other hand, the algorithm used for floating point numbers such as `Single` can scale
any result to the target range. For example here's the implementation for a uniform distribution
over the interval [0, 1). This time `TrySample` simply calls `Sample`, which doesn't block:

``` csharp
public readonly struct ClosedOpenSingle : IDistribution<Single>
{
    public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
    {
        result = Sample(rng);
        return true;
    }

    public Single Sample<TRng>(TRng rng) where TRng : IRng
    {
        const Single scale = 1.0f / (1u << 24);
        var random = rng.NextUInt32();
        var value = random >> 8;
        return scale * value;
    }
}
```
