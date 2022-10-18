using System;
using RandN.Implementation;

namespace RandN;

/// <summary>
/// An RNG repeating a fixed sequence;
/// </summary>
public sealed class SequenceRng : ICryptoRng
{
    public SequenceRng(UInt32[] sequence) => Sequence = sequence;

    public UInt32[] Sequence { get; }

    public Int32 Index { get; private set; }

    public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

    public UInt32 NextUInt32() => Sequence[Index++ % Sequence.Length];

    public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);
}