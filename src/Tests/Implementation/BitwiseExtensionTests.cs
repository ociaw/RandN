using System;
using Xunit;

namespace RandN.Implementation;

public sealed class BitwiseExtensionTests
{
    [Theory]
    [InlineData(0xFF, 4, 0xF000000F)]
    [InlineData(0xFF000000, 28, 0xF000000F)]
    public void RotationRight(UInt32 original, Int32 amount, UInt32 expected)
    {
        UInt32 rotated = original.RotateRight(amount);
        Assert.Equal(expected, rotated);
    }

    [Theory]
    [InlineData(0xFF, 28, 0xF000000F)]
    [InlineData(0xFF000000, 4, 0xF000000F)]
    public void RotationLeft(UInt32 original, Int32 amount, UInt32 expected)
    {
        UInt32 rotated = original.RotateLeft(amount);
        Assert.Equal(expected, rotated);
    }

    [Theory]
    [InlineData(0xFFFF_FFFF_0000_0000, 0x0000_0000)]
    [InlineData(0x0000_0000_FFFF_FFFF, 0xFFFF_FFFF)]
    [InlineData(0x0000_FFFF_FFFF_0000, 0xFFFF_0000)]
    [InlineData(0xFFFF_0000_0000_FFFF, 0x0000_FFFF)]
    public void LowIsolation(UInt64 original, UInt32 expected) => Assert.Equal(expected, original.IsolateLow());

    [Theory]
    [InlineData(0xFFFF_FFFF_0000_0000, 0xFFFF_FFFF)]
    [InlineData(0x0000_0000_FFFF_FFFF, 0x0000_0000)]
    [InlineData(0x0000_FFFF_FFFF_0000, 0x0000_FFFF)]
    [InlineData(0xFFFF_0000_0000_FFFF, 0xFFFF_0000)]
    public void HighIsolation(UInt64 original, UInt32 expected) => Assert.Equal(expected, original.IsolateHigh());

    [Theory]
    [InlineData(0xFFFF_FFFF, 0x0000_0000, 0xFFFF_FFFF_0000_0000)]
    [InlineData(0x0000_0000, 0xFFFF_FFFF, 0x0000_0000_FFFF_FFFF)]
    [InlineData(0x0000_FFFF, 0xFFFF_0000, 0x0000_FFFF_FFFF_0000)]
    [InlineData(0xFFFF_0000, 0x0000_FFFF, 0xFFFF_0000_0000_FFFF)]
    public void Combination(UInt32 high, UInt32 low, UInt64 expected) => Assert.Equal(expected, high.CombineWithLow(low));
}