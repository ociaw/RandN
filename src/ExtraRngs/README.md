# RandN.ExtraRngs

[![RandN.ExtraRngs on NuGet](https://img.shields.io/nuget/v/RandN.ExtraRngs)](https://www.nuget.org/packages/RandN.ExtraRngs/)

Provides extra RNG algorithms that aren't necessary to the main RandN package, but may be useful
in many scenarios.

RandN is a .NET library for flexible and reproducible random number generation.

Provided RNG algorithms include a variant of Mersenne Twister, and a variant of XorShift. 

## Mersenne Twister
The Mersenne Twister is an RNG developed in 1997 and has very long period. Currently only an implementation of the
MT19937-64 variant is provided.

[More information on the Mersenne Twister](https://en.wikipedia.org/wiki/Mersenne_Twister)

## XorShift
Xorshift is a family of simple fast space-efficient statistically good algorithms for random number generation.

[More information on Xorshift](https://en.wikipedia.org/wiki/Xorshift)