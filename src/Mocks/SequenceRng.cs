using System;
using System.Collections.Generic;
using System.Linq;
using RandN.Implementation;

namespace RandN;

/// <summary>
/// An RNG repeating a fixed sequence;
/// </summary>
public sealed class SequenceRng(UInt32[] sequence) : ICryptoRng
{
    public SequenceRng(IEnumerable<UInt64> sequence) : this(sequence.SelectMany(SplitUInt64).ToArray())
    { }

    public UInt32[] Sequence { get; } = sequence;

    public Int32 Index { get; private set; }

    public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

    public UInt32 NextUInt32() => Sequence[Index++ % Sequence.Length];

    public UInt64 NextUInt64() => Filler.NextUInt64ViaUInt32(this);

    private static IEnumerable<UInt32> SplitUInt64(UInt64 ul)
    {
        yield return ul.IsolateLow();
        yield return ul.IsolateHigh();
    }
}
