using System;
using RandN.Rngs;

namespace RandN;

/// <summary>
/// A non-cryptographically secure RNG with good performance and output quality, while requiring limited memory.
/// </summary>
#if NET7_0_OR_GREATER
public sealed class SmallRng : IRng, ISelfSeedingRng<SmallRng>
#else
public sealed class SmallRng : IRng
#endif
{
    private readonly Pcg32 _wrapped;

    private SmallRng(Pcg32 wrapped) => _wrapped = wrapped;

    /// <summary>
    /// Creates a <see cref="StandardRng"/>.
    /// </summary>
    public static SmallRng Create()
    {
        var rng = Pcg32.GetFactory().Create(ThreadLocalRng.Instance);
        return new SmallRng(rng);
    }

    /// <summary>
    /// Gets the <see cref="StandardRng"/> factory.
    /// </summary>
    public static Factory GetFactory() => new();

    /// <inheritdoc />
    public void Fill(Span<Byte> buffer) => _wrapped.Fill(buffer);

    /// <inheritdoc />
    public UInt32 NextUInt32() => _wrapped.NextUInt32();

    /// <inheritdoc />
    public UInt64 NextUInt64() => _wrapped.NextUInt64();

    /// <inheritdoc cref="IRngFactory{StandardRng}" />
    public readonly struct Factory : IRngFactory<SmallRng>
    {
        /// <inheritdoc />
        public SmallRng Create() => SmallRng.Create();
    }
}
