Title: Reproducibility
---

# Reproducibility

Any type implementing `IReproducibleRngFactory` is guaranteed to be reproducible; that is, given a
seed, the resulting RNG *always* will produce the same sequence. This applies across major and
minor versions, even if it turns out the RNG has a bug in it. In that case, the given seed type
and factory will be marked as obsolete, and a corrected version will be added. The obsolete version
may be moved into a separate package for the next version-breaking upgrade.

# Distribution Stability

Distributions are currently not stable - their implementations may change, but will be considered
to be breaking.
