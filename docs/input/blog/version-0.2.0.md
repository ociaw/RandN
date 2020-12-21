Title: New Release - Version 0.2.0
Published: 2020-12-20
Author: ociaw
Category: Releases
---

RandN v0.2.0 has been released! It's available at
[NuGet](https://www.nuget.org/packages/RandN/0.2.0) and
[GitHub](https://github.com/ociaw/RandN/releases/tag/v0.2.0). Highlights of this release include:

- `ChaCha` is now SIMD accerated and has between 6x-11x higher throughput on CPUs supporting AVX2
on .NET Core 3.1 and 5+ compared to .NET Framework 4.7.2.
- The `RandN.Distributions` namespace has been reworked significantly to make discovery and usage
easier.
- Comprehensive unit test coverage.
- Many bugs found and fixed thanks to improved testing.
- This fancy documentation website!

A full list of changes, including breaking changes from v0.1 to v0.2, is included further below.

### What's next?

More distributions and more RNG algorithms - whether or not these will be included alongside
`RandN`'s primary NuGet package or on another is still TBD.

More efficient one-off range distribution - right now, an entire `Uniform` object must be built
before sampling a range; this could make some use cases, such as the `RandomShim`, much more
efficient.

Improvements to website versioning - only the Docs and API need to be versioned, not the blog or
the index page.

## Full Changelog

### New APIs
- The marker-interface [IPortableDistribution<T>](/api/RandN/IPortableDistribution_1) has been
created to mark distributions that provide portable and repeatable results across all platforms.
Currently, the Bernoulli, the uniform `Decimal`, the uniform `TimeSpan`, and all the uniform
integer distributions are portable.
- Added [BlockBuffer32<TBlockRng, TBlockCounter>.BlockLength](/api/RandN.Implementation/BlockBuffer32_2/6839F0CC)
 for consistency with [BlockBuffer32<TBlockRng>.BlockLength](/api/RandN.Implementation/BlockBuffer32_1/A6553A71).

### Updated APIs
- Uniform distributions now live have been grouped into nested classes under the
[Uniform](/api/RandN.Distributions/Uniform/) static class. For example, `UniformInt32` has become
[Uniform.Int32](/api/RandN.Distributions/Uniform.Int32/).
- Unit interval distributions now live in the
[Rand.Distributions.UnitInterval](/api/RandN.Distributions.UnitInterval/) namespace and have been grouped
into nested classes. For example, `UnitInterval.OpenDouble` has become
[UnitInterval.Open.Double](/api/RandN.Distributions.UnitInterval/Open.Double/).
- Method arguments now have null-checks injected by
[NullGuard.Fody](https://github.com/Fody/NullGuard) instead of relying on the honor system.
- The `TRng` type parameter of
[IRngFactory<TRng>](/api/RandN/IRngFactory_1/) and
[IReproducibleRngFactory<TRng, TSeed>](/api/RandN/IRngFactory_2/) is
now covariant (`IRngFactory<out TRng>`).
- `BlockBuffer32<TBlockRng>.Length` has been renamed to `BlockLength`.

### Removed APIs
- Removed the `Sample` extension methods - these were questionably useful, and could be up to four
times slower than their non-extension counterpart,
[IDistribution.Sample](/api/RandN/IDistribution_1/746236D3).

### Bug fixes
- Distributions returning floating point numbers would sometimes sample a number outside the
desired range on .NET Framework x86, which can use 80-bit floating point calculations, instead of
64-bit. Results are now forced to 64-bit precision before returning.
- [`BitwiseExtensions.RotateRight`](/api/RandN.Implementation/BitwiseExtensions.RotateRight) now
rotates bits right instead of left on .NET Core 3.1+.
- All places where overflow is expected is now wrapped with `unchecked`.

### Miscellaneous
- Full test coverage, excluding portions that can't be tested without a big-endian processor.
- Pure methods and properties in `RandN.Core` have been marked with the [Pure] attribute.
- Use of C# 9 features across the board where possible.
- Fixed a bunch of things suggested by ReSharper.
- More benchmarks have been added.
