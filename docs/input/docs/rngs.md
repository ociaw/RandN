Title: RNGs
---

# General RNGs

These RNGs are good all-rounders, and should be the first to be considered. They are not
reproducible and cannot be seeded, but are automatically seeded with a cryptographic RNG upon
creation.

## Standard

`StandardRng` is the go-to RNG that works well for anything that doesn't require reproducibility.
It's cryptographically secure, so it's safe to use for any application, has good statistical
properties, and performs fairly well.

The underlying algorithm is currently ChaCha8, but is subject to change and cannot be relied upon.

``` csharp
var rng = StandardRng.Create();
```

## Small

If you're limited in memory or need a faster RNG than `StandardRng`, but don't need cryptographic
security, `SmallRng` is the RNG to use. It uses an algorithm that requires a small amount of state
while maintaining good statistical properties and high performance.

The underlying algorithm is currently PCG32, but is subject to change and cannot be relied upon.

``` csharp
var rng = SmallRng.Create();
```

## ThreadLocalRng

`ThreadLocalRng` is thread-safe, cryptographically secure, has good statistical properties, and
performs fairly well. It stores the underlying algorithm in thread-local storage to be thread-safe.

The underlying algorithm is currently ChaCha8, but is subject to change and cannot be relied upon.

``` csharp
var rng = ThreadLocalRng.Instance;
```

# Reproducible RNGs

If reproducibile sequences are needed, then a specific algorithm must be selected. The user is
responsible for seeding these RNGs correctly.

## ChaCha

ChaCha is a seekable, cryptographically secure\* RNG.

``` csharp
var seed = new ChaCha.Seed(new UInt32[8]); // All zero seed
var rng = ChaCha.Create(seed);

rng.Position = new ChaCha.Counter(9001, 0); // Seek to an arbitrary position
```

[*ChaCha* on *Wikipedia*](https://en.wikipedia.org/wiki/Salsa20#ChaCha_variant)

\* 8 rounds and above are unbroken.

## Mersenne Twister

Mersenne Twister doesn't offer any noticable advantages over PCG32, and may later be moved out of
the main `RandN` package.

``` csharp
var seed1 = 5489ul;
var rng1 = Mt1993764.Create(seed1);

// Mersenne Twister can be seeded with an array as well
var seed2 = new UInt64[16]; // All zero seed
var rng2 = Mt1993764.Create(seed2);
```

[Mersenne Twister on *Wikipedia*](https://en.wikipedia.org/wiki/Mersenne_Twister)

## PCG 32

A `Permuted Congruential Generator` with 64-bit state and 32-bit output. The seed is 127 bits, as
the highest bit of the stream id is ignored.

``` csharp
var seed = new Pcg32.Seed(578437695752307201ul, 1157159078456920585ul);
var rng = Pcg32.Create(seed);
```

[PCG on *Wikipedia*](https://en.wikipedia.org/wiki/Permuted_congruential_generator)

[PCG paper](https://www.pcg-random.org/paper.html)

## Xorshift

Xorshift doesn't offer any noticable advantages over PCG32, and may later be moved out of the main
`RandN` package.

``` csharp
var seed = (1u, 2u, 3u, 4u);
var rng = XorShift.Create(seed);
```

# OS RNG - CryptoServiceProvider

CryptoServiceProvider uses `System.Security.Cryptography.RNGCryptoServiceProvider`, which is the
closest we get to an OS level RNG through .NET. It's cryptographically secure, but not
reproducbile. The underlying algorithm is provided by the cryptographic service provider.

``` csharp
using var rng = CryptoServiceProvider.Create();
```

[RNGCryptoServiceProvider on *Microsoft Docs*](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rngcryptoserviceprovider)

</article>
