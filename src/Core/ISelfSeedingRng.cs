#if NET7_0_OR_GREATER
namespace RandN;

/// <summary>
/// An RNG that seeds itself from entropy.
/// </summary>
public interface ISelfSeedingRng<out TRng>
    where TRng : notnull, IRng, ISelfSeedingRng<TRng>
{
    /// <summary>
    /// Creates a new <typeparamref name="TRng" />.
    /// </summary>
    /// <returns>A new <typeparamref name="TRng" /> instance.</returns>
    static abstract TRng Create();
}
#endif
