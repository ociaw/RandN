using System;

namespace RandN;

public sealed class DisposableRng : ICryptoRng, IDisposable
{
    private readonly IRng _wrapped;

    public DisposableRng(IRng wrapped) => _wrapped = wrapped;

    public Boolean Disposed { get; private set; }

    public void Dispose() => Disposed = true;

    public void Fill(Span<Byte> buffer) => _wrapped.Fill(buffer);

    public UInt32 NextUInt32() => _wrapped.NextUInt32();

    public UInt64 NextUInt64() => _wrapped.NextUInt64();
}
