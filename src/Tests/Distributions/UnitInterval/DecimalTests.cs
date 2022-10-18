using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions.UnitInterval;

public class DecimalTests
{
    [Fact]
    public void DecimalRanges()
    {
        var zeroRng = new StepRng(0) { Increment = 0 }; // Generates 0.0
        var openZeroRng = new SequenceRng(new UInt32[] { 1, 0, 0 }); // Generates the lowest non-zero value
        var openOneRng = new SequenceRng(new UInt32[] { 0x1000_0000, 0x3E25_0261, 0x204F_CE5Du << 2 }); // Generates the value just under 1.0
        var oneRng = new SequenceRng(new UInt32[] { 0x1000_0000, 0x3E25_0261, 0x204F_CE5Eu << 2 }); // Generates 1.0
        var invalidRng = new SequenceRng(new UInt32[] { 0x1000_0000, 0x3E25_0261, 0x204F_CE5Fu << 2 }); // Generates the value just over 1.0
        Decimal zero, openZero, openOne, one;

        var closedOpen = ClosedOpen.Decimal.Instance;
        zero = closedOpen.Sample(zeroRng);
        openZero = closedOpen.Sample(openZeroRng);
        openOne = closedOpen.Sample(openOneRng);
        Assert.False(closedOpen.TrySample(oneRng, out _));
        Assert.False(closedOpen.TrySample(invalidRng, out _));

        Assert.Equal(0, zero);
        Assert.True(0 < openZero);
        Assert.True(openZero < openOne);
        Assert.True(openOne < 1);

        var openClosed = OpenClosed.Decimal.Instance;
        Assert.False(openClosed.TrySample(zeroRng, out _));
        openZero = openClosed.Sample(openZeroRng);
        openOne = openClosed.Sample(openOneRng);
        one = openClosed.Sample(oneRng);
        Assert.False(openClosed.TrySample(invalidRng, out _));

        Assert.True(0 < openZero);
        Assert.True(openZero < openOne);
        Assert.True(openOne < 1);
        Assert.Equal(1, one);

        var closed = Closed.Decimal.Instance;
        zero = closed.Sample(zeroRng);
        openZero = closed.Sample(openZeroRng);
        openOne = closed.Sample(openOneRng);
        one = closed.Sample(oneRng);
        Assert.False(closed.TrySample(invalidRng, out _));

        Assert.Equal(0, zero);
        Assert.True(0 < openZero);
        Assert.True(openZero < openOne);
        Assert.True(openOne < 1);
        Assert.Equal(1, one);

        var open = Open.Decimal.Instance;
        Assert.False(open.TrySample(zeroRng, out _));
        openZero = open.Sample(openZeroRng);
        openOne = open.Sample(openOneRng);
        Assert.False(open.TrySample(oneRng, out _));
        Assert.False(open.TrySample(invalidRng, out _));

        Assert.True(0 < openZero);
        Assert.True(openZero < openOne);
        Assert.True(openOne < 1);
    }
    [Fact]
    public void DecimalAverage()
    {
        Average(OpenClosed.Decimal.Instance, 904);
        Average(ClosedOpen.Decimal.Instance, 905);
        Average(Closed.Decimal.Instance, 906);
        Average(Open.Decimal.Instance, 907);
    }

    private static void Average(IDistribution<Decimal> dist, UInt64 seed)
    {
        const Int32 iterations = 10_000;
        var rng = Pcg32.Create(seed, 11634580027462260723ul);

        Decimal mean = 0;
        for (var i = 0; i < iterations; i++)
        {
            var result = dist.Sample(rng);
            var delta = result - mean;
            mean += delta / (i + 1);
            Assert.True(0 <= result);
            Assert.True(result <= 1);
        }

        Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, (Double)mean, iterations));
    }

    [Fact]
    public void NonNullable()
    {
        Assert.Throws<ArgumentNullException>(() => ClosedOpen.Decimal.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => OpenClosed.Decimal.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Closed.Decimal.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Open.Decimal.Instance.Sample<StepRng>(null));
    }
}