# RandN

[![RandN on NuGet](https://img.shields.io/nuget/v/RandN)](https://www.nuget.org/packages/RandN/)

RandN is a .NET library for random number generation. It aims to rectify [deficiencies in
`System.Random`](https://ociaw.com/posts/pitfalls-of-system-random) with adaptability and
extensibility in mind. RandN is heavily inspired by the design of the Rust crate
[rand](https://github.com/rust-random/rand), and aims to maintain some level of
compatibility with it.

### What's wrong with System.Random?

In short, the algorithm it uses is [slow](https://github.com/ociaw/RandomBenchmarks) and
[biased](https://fuglede.dk/en/blog/bias-in-net-rng/). The API is very rigid and inflexible, and as
a result is unsuited for many purposes.

RandN provides a clear and obvious API that is difficult to use incorrectly, unlike the API of
`System.Random`. This is accomplished by clearly separating two concepts; generating randomness
with an `IRng`, and turning that data into something useful with an `IDistribution`.

## Docs

The full documentation is available [here](https://ociaw.com/randn).

# Usage

Install the RandN package from [Nuget](https://www.nuget.org/packages/RandN/) for most use cases.
If you just want to implement an random number generator (ex. you're publishing a package with a new
RNG), instead depend on [RandN.Core](https://www.nuget.org/packages/RandN.Core/).

## Examples

### Creating an RNG

``` csharp
using RandN;

// Creates a cryptographically secure RNG
var rng = StandardRng.Create();

// Creates a non-cryptographically secure RNG that's fast and uses less memory
var insecureRng = SmallRng.Create();
```

A reproducible RNG can also be created by using an algorithm directly:

``` csharp
using RandN.Rngs;

// Use ThreadLocalRng to seed the RNG - this uses a cryptographically secure
// algorithm, so tight loops won't result in similar seeds
var seeder = ThreadLocalRng.Instance;

// Create the seed (Seeds can also be created manually)
var factory = ChaCha.GetChaCha8Factory();
var seed = factory.CreateSeed(seeder);

// Create the RNG from the seed
var rng = factory.Create(seed);
```

### Getting random numbers

Once you have an RNG, you can either use it directly,

``` csharp
var num = rng.NextUInt32();
var bigNum = rng.NextUInt64();
var buffer = new Byte[1000];
rng.Fill(buffer);
```

or you can use it to sample a distribution:

``` csharp
UniformInt32 distribution = Uniform.NewInclusive(42, 54); // [42 - 54]
int answer = distribution.Sample(rng);

Bernoulli weightedCoin = Bernoulli.FromRatio(8, 10); // 80% chance of true
bool probablyHeads = weightedCoin.Sample(rng);
```

Shuffling a list is also easy:

``` csharp
var list = new List<Int32>() { 1, 2, 3, 4, 5, 6 };
rng.ShuffleInPlace(list);
```

Any type implementing `IRng` can be wrapped with `RandomShim`, which can be used as a drop-in
replacement for `Random`.

``` csharp
using RandN.Compat;
Random random = RandomShim.Create(rng);
random.Next(2);
```
