using System;
using System.Threading;
using RandN.Rngs;

namespace RandN;

/// <summary>
/// A cryptographically secure thread local generator. All members in this type are thread-safe.
/// </summary>
public sealed class ThreadLocalRng : ICryptoRng
{
    private static readonly ThreadLocal<ChaCha> ThreadLocal = new(() =>
    {
        using var seeder = SystemCryptoRng.Create();
        return ChaCha.GetChaCha8Factory().Create(seeder);
    });

    /// <summary>
    /// The singleton instance of <see cref="ThreadLocalRng"/>.
    /// </summary>
    public static ThreadLocalRng Instance { get; } = new();

    /// <inheritdoc />
    public void Fill(Span<Byte> buffer) => ThreadLocal.Value!.Fill(buffer);

    /// <inheritdoc />
    public UInt32 NextUInt32() => ThreadLocal.Value!.NextUInt32();

    /// <inheritdoc />
    public UInt64 NextUInt64() => ThreadLocal.Value!.NextUInt64();
}