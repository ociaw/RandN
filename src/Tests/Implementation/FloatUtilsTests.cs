using System;
using Xunit;

namespace RandN.Implementation;

public sealed class FloatUtilsTests
{
    [Fact]
    public void DecrementSmallestPositiveDecimal()
    {
        Decimal num = new(1, 0, 0, false, 28);
        Decimal result = num.DecrementMantissa();
        Assert.Equal(0m, result);
    }

    [Fact]
    public void DecrementZeroDecimals()
    {
        for (Byte i = 0; i <= 28; i++)
        {
            Decimal num = new(0, 0, 0, false, i);
            Assert.Throws<ArgumentOutOfRangeException>(() => num.DecrementMantissa());
        }
    }

    [Fact]
    public void DecrementDecimalPastScaleThreshold()
    {
        Decimal num = new(1, 0, 0, false, 0);
        Decimal result = num.DecrementMantissa();
        Assert.True(result < num);
        Assert.True(result > 0);
        Assert.Equal(.9m, result);
    }

    [Fact]
    public void DecrementDecimalLowBits()
    {
        Decimal num = new(2, 0, 0, false, 0);
        Decimal result = num.DecrementMantissa();
        Assert.True(result < num);
        Assert.Equal(1m, result);
    }

    [Fact]
    public void DecrementDecimalMidBits()
    {
        Decimal num = new(0, 1, 0, false, 0);
        Decimal result = num.DecrementMantissa();
        Assert.True(result < num);
        Assert.Equal(UInt32.MaxValue, result);
    }

    [Fact]
    public void DecrementDecimalHiBits()
    {
        Decimal num = new(0, 0, 1, false, 0);
        Decimal result = num.DecrementMantissa();
        Assert.True(result < num);
        Assert.Equal(UInt64.MaxValue, result);
    }
}