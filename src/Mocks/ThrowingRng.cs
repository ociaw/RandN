using System;

namespace RandN;

/// <summary>
/// An RNG that throws an exception every time the sequence is enumerated.
/// </summary>
public class ThrowingRng : ICryptoRng
{
    public UInt32 NextUInt32() => throw new UnexpectedRngEnumerationException();

    public UInt64 NextUInt64() => throw new UnexpectedRngEnumerationException();

    public void Fill(Span<Byte> buffer) => throw new UnexpectedRngEnumerationException();

    private class UnexpectedRngEnumerationException : InvalidOperationException
    {
    }
}