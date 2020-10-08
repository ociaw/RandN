Title: Reproducibility
---

# Reproducibility

Any type implementing `IReproducibleRngFactory` is guaranteed to be reproducible; that is, given a
seed, the resulting RNG *always* will produce the same sequence. This applies across major and
minor versions, even if it turns out the RNG has a bug in it. In that case, the given seed type
and factory will be marked as obsolete, and a corrected version will be added. The obsolete version
may be moved into a separate package for the next version-breaking upgrade.

# Distribution Stability

In .NET, *nothing* regarding implementation of floating-point is guaranteed. Therefore,
distributions involving floating-point numbers (such as those that return `Double` or `Single`) are
**not** portable - the results can vary on different machines, and even on different runtimes.
You *may* be able to consider the distributions to be deterministic, however - the same machine and
runtime should produce the same results (this could still potentially vary on runtime, however).

For more information on this, see issues [#8](https://github.com/ociaw/RandN/issues/8) and
[#9](https://github.com/ociaw/RandN/issues/9).

Other distributions are currently not stable - their implementations may change, but will be
considered to be breaking and will warrant a minor-version bump (or major after 1.0.0).
