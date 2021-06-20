Title: New Release - Version 0.3.0
Published: 2021-06-20
Author: ociaw
Category: Releases
---

RandN v0.3.0 has been released! It's available at
[NuGet](https://www.nuget.org/packages/RandN/0.3.0) and
[GitHub](https://github.com/ociaw/RandN/releases/tag/v0.3.0). It's a small one, but has a couple
potentially breaking changes.

## Full Changelog

- Moved the IDistribution and IPortableDistribution interfaces to RandN.Core
- Removed NullGuard.Fody from public dependencies - this was only needed for build and was being
unnecessarily included in all dependents.
- Fixed tests failing to run on Mono.

### What's next?

More distributions and more RNG algorithms - whether or not these will be included alongside
`RandN`'s primary NuGet package or on another is still TBD.

More efficient one-off range distribution - right now, an entire `Uniform` object must be built
before sampling a range; this could make some use cases, such as the `RandomShim`, much more
efficient.

Improvements to website versioning - only the Docs and API need to be versioned, not the blog or
the index page.
