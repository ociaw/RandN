Title: Usage
---

# Creating an RNG

``` csharp
using RandN;

// Creates a cryptographically secure RNG
StandardRng cryptoRng = StandardRng.Create();

// Creates a non-cryptographically secure RNG that's fast and uses less memory
SmallRng rng = SmallRng.Create();
```

A reproducible RNG can also be created by using an algorithm directly:

``` csharp
using RandN.Rngs;

// Use ThreadLocalRng to seed the RNG - this uses a cryptographically secure
// algorithm, so tight loops won't result in similar seeds
ThreadLocalRng seeder = ThreadLocalRng.Instance;

// Create the seed (Seeds can also be created manually)
ChaCha.Factory8 factory = ChaCha.GetChaCha8Factory();
ChaCha.Seed seed = factory.CreateSeed(seeder);

// Create the RNG from the seed
ChaCha rng = factory.Create(seed);

// If you don't care about storing the seed, an RNG can be created directly
rng = factory.Create();
```

# Getting random numbers

Once you have an RNG, you can either use it directly,

``` csharp
var num = rng.NextUInt32();
var bigNum = rng.NextUInt64();
var buffer = new Byte[1000];
rng.Fill(buffer);
```

or you can use it to sample a distribution:

``` csharp
Uniform.Int32 distribution = Uniform.NewInclusive(42, 54); // [42 - 54]
int answer = distribution.Sample(rng);

Bernoulli weightedCoin = Bernoulli.FromRatio(8, 10); // 80% chance of true
bool probablyHeads = weightedCoin.Sample(rng);
```

Shuffling a list is also easy:

``` csharp
var list = new List<Int32>() { 1, 2, 3, 4, 5, 6 };
rng.ShuffleInPlace(list);
```

# Shimming

Any type implementing `IRng` can be wrapped with `RandomShim`, which can be used as a drop-in
replacement for `Random`.

``` csharp
using RandN.Compat;
Random random = RandomShim.Create(rng);
random.Next(2);
```

Similarly, `RandomNumberGeneratorShim` can wrap any `ICryptoRng` and replace
`System.Security.Crypotgraphy.RandomNumberGenerator`.

``` csharp
using RandN.Compat;
RandomNumberGenerator shim = RandomNumberGeneratorShim.Create(rng);
var buffer = new Byte[35];
shim.GetNonZeroBytes(buffer);
```
