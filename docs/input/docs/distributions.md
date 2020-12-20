Title: Distributions
---

# Bernoulli
The Bernoulli distribution can be thought of as a coin flip, returning either `true` or `false`
with a probability of *p*, where *p* lies on the closed interval [0, 1]. When *p == 1*, the
distribution will always return `true`, while when *p == 0*, the distribution will always return
`false`.

``` csharp
var probability = Bernoulli.FromP(0.5);
var ratio = Bernoulli.FromRatio(5, 10);
```

Internally, the probability is represented by `_p`, a `UInt64`. When sampling, a random `UInt64` is
generated and compared to `_p`; if it's less than `_p`, return `true`, otherwise return `false`.

This has the side effect of not allowing distributions that always return `true`. To counteract
this, *p == 1* is a special case where the RNG is not sampled, and simply always returns `true`.

The `FromInverse` method is provided to allow more control over the probability of the
distribution. It takes a `UInt64` and sets `_p` directly.

```csharp
var inverse = Bernoulli.FromInverse(UInt64.MaxValue / 2 + 1);
```

[*Bernoulli Distribution* on Wikipedia](https://en.wikipedia.org/wiki/Bernoulli_distribution)

# Uniform
A uniform distribution over an interval has a *uniform* (or equal) probability of producing any
value within that range. For example, the outcome of rolling a 6 sided die is represented by a
uniform distribution over the interval [1, 6].

``` csharp
Uniform.Int32 d6 = Uniform.NewInclusive(1, 6);
Uniform.Int32 d20 = Uniform.NewInclusive(1, 20);

// Some may argue that there's no such thing as a perfect grade, but this may get you pretty close.
Uniform.Int32 grade = Uniform.New(0.0, 100.0);

// TimeSpans are also supported - try not to burn your popcorn.
Uniform.TimeSpan times = Uniform.NewInclusive(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(3));
```

[*Continuous Uniform Distribution* on Wikipedia](https://en.wikipedia.org/wiki/Uniform_distribution_(continuous))

[*Discrete Uniform Distribution* on Wikipedia](https://en.wikipedia.org/wiki/Discrete_uniform_distribution)

# Unit Interval
Unit interval distributions are a special case of uniform distributions over the unit interval\*,
the interval from 0 to 1. Four distinct distributions are provided, closed-open, open-closed,
closed-closed, and open-open.

``` csharp
using RandN.Distributions.UnitInterval;
var closedOpen = ClosedOpen.Double.Instance;
var open = Open.Double.Instance;
```

[*Unit Interval* on Wikipedia](https://en.wikipedia.org/wiki/Unit_interval)

\* Treating a *unit interval* as any of the four shapes over an interval from 0 to 1:
`[0, 1)`, `(0, 1]`, `[0, 1]`, and `(0, 1)`
