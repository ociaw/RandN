Title: RandN - Better random number generation for .NET
---

RandN provides an alternative random number generation API to .NET's `Random`, which has
numerous problems, including low performance, poor statistical quality, and limiting and inflexible
 API design. RandN solves all of these issues by providing modern RNG algorithms encapsulated
 within a carefully designed API that was inspired by the Rust crate,
[Rand](https://github.com/rust-random/rand). Think of RandN like [NodaTime](https://nodatime.org/),
but for random numbers.

## Why not just use Random?

While Random can work for simple use cases, it has inherit design problems that have not been
fixed. A common error is instantiating `Random` instances in a loop - they'll almost certainly
produce identical sequences since they're seeded from the current time. It's also very slow
compared to other RNGs. Even more subtle is the statisical bias in the algorithm used. Performance
and statistical quality can be improved by using other algorithms, but the underlying problem
remains - the API is a footgun.

The problems with `Random` are elaborated on in
[this blog post](https://ociaw.com/posts/pitfalls-of-system-random), and there's plenty of
other literature pointing out [statistical bias](https://fuglede.dk/en/blog/bias-in-net-rng/) and
[design issues](https://ericlippert.com/2019/01/31/fixing-random-part-1/). Suffice it to say, while
`Random` works *okay* for simple use cases (and if you know how to avoid the shooting yourself in
the foot) there's a lot of room for improvement in all areas.

# Quick Start

Install the [NuGet package](https://www.nuget.org/packages/RandN/), then you're ready to go.

``` csharp
using RandN;
using RandN.Distributions;

StandardRng rng = StandardRng.Create();

Bernoulli weightedCoin = Bernoulli.FromRatio(8, 10); // 8/10 chance
bool probablyTrue = weightedCoin.Sample(rng);

UniformInt32 d6 = Uniform.NewInclusive(1, 6);
int roll = d6.Sample(rng);

// Drop in replacement of Random is possible through a shim
Random random = RandN.Compat.RandomShim.Create(rng);
random.Next(42);
```

[View on .NET Fiddle](https://dotnetfiddle.net/wVE76H)

## RNG Algorithms

The following random number generators are included with RandN:
* [Standard](/docs/rngs#standard) (cryptographically secure)
* [Small](/docs/rngs#small)
* [ThreadLocalRng](/docs/rngs#threadlocalrng) (cryptographically secure)
* [ChaCha8, ChaCha12, ChaCha20](/docs/rngs#chacha) (cryptographically secure)
* [CryptoServiceProvider](/docs/rngs#os-rng-cryptoserviceprovider) (cryptographically secure)
* [Mersenne Twister](/docs/rngs#mersenne-twister) (MT19937-64 variant)
* [PCG 32](/docs/rngs#pcg-32)
* [Xorshift](/docs/rngs#xorshift)

## Distributions

These distributions are included with RandN:
* [Bernoulli](/docs/distributions#bernoulli)
* [Uniform](/docs/distributions#uniform) (for integers, floats, `decimal`, and `TimeSpan`)
* [Unit Interval](/docs/distributions#unit-interval) (for floats and `decimal`)
