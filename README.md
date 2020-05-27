# RandN
RandN is a .NET library for random number generation. It aims to rectify deficiencies in
`System.Random` with adaptability and extensibility in mind. RandN is heavily inspired by the
design of the Rust crate [rand](https://github.com/rust-random/rand), and aims to maintain some
level of compatibility with it.

# What's wrong with System.Random?

In short, the algorithm it uses is [slow](https://github.com/ociaw/RandomBenchmarks) and
[biased](https://fuglede.dk/en/blog/bias-in-net-rng/). The API is very rigid and inflexible, and as
a result is unsuited for many purposes.

# Examples

### Creating a reproducible RNG
```
using RandN.Rngs;

// Use the RNGCryptoServiceProvider to seed the RNG
var seeder = CryptoServiceProvider.Create();

// Create the seed (Seeds can also be created manually)
var factory = ChaCha.GetChaCha20Factory();
var seed = factory.CreateSeed(seeder);

// Create the RNG from the seed
var rng = factory.Create(rng);
```

Once you have an RNG, you can either use it directly,

```
var num = rng.NextUInt32();
var bigNum = rng.NextUInt64();
var buffer = new Byte[1000];
rng.Fill(buffer);
```

or you can use it to sample a distribution:

```
var distribution = Uniform.NewInclusive(42, 54);
var answer = distribution.sample(rng);
```

Any type implementing `IRng` can be wrapped with `RandomWrapper`, which can be used as a drop-in
replacement for `Random`.

```
Random random = RandomWrapper.Create(rng);
```

# Compatibility
* RandN is written with the assumption that C# 8 Nullable Reference types are enabled and therefore
does not null check arguments.
