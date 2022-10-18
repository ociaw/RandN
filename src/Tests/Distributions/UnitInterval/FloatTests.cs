using System;
using System.Collections.Generic;
using Xunit;
using RandN.Rngs;

/*** This file is auto generated - any changes made here will be lost. Modify UnitIntervalTests.tt instead. ***/
namespace RandN.Distributions.UnitInterval;

public sealed class FloatTests
{
    [Fact]
    public void SingleRanges()
    {
        var zeroRng = new StepRng(0) { Increment = 0 };
        var maxRng = new StepRng(UInt64.MaxValue) { Increment = 0 };
        Double low, high;

        var closedOpen = ClosedOpen.Single.Instance;
        low = closedOpen.Sample(zeroRng);
        high = closedOpen.Sample(maxRng);

        Assert.Equal(0, low);
        Assert.True(0 < high && high < 1);

        var openClosed = OpenClosed.Single.Instance;
        low = openClosed.Sample(zeroRng);
        high = openClosed.Sample(maxRng);

        Assert.True(0 < low && low < 1);
        Assert.Equal(1, high);

        var closed = Closed.Single.Instance;
        low = closed.Sample(zeroRng);
        high = closed.Sample(maxRng);

        Assert.Equal(0, low);
        Assert.Equal(1, high);

        var open = Open.Single.Instance;
        low = open.Sample(zeroRng);
        high = open.Sample(maxRng);

        Assert.True(0 < low && low < 1);
        Assert.True(0 < high && high < 1);
    }

    public static IEnumerable<Object[]> SingleParams(Int32 seedStart)
    {
        yield return new Object [] { OpenClosed.Single.Instance, seedStart };
        yield return new Object[] { ClosedOpen.Single.Instance, seedStart + 1 };
        yield return new Object[] { Closed.Single.Instance, seedStart + 2};
        yield return new Object[] { Open.Single.Instance, seedStart + 3};
    }

    [Theory]
    [MemberData(nameof(SingleParams), 900)]
    public void SingleAverage(IDistribution<Single> dist, UInt64 seed)
    {
        const Int32 iterations = 10_000;
        var rng = Pcg32.Create(seed, 11634580027462260723ul);

        Double mean = 0;
        for (var i = 0; i < iterations; i++)
        {
            var result = dist.Sample(rng);
            var delta = result - mean;
            mean += delta / (i + 1);
            Assert.True(0 <= result);
            Assert.True(result <= 1);
        }

        Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean, iterations));

        Double mean2 = 0;
        for (var i = 0; i < iterations; i++)
        {
            Assert.True(dist.TrySample(rng, out var result));
            var delta = result - mean2;
            mean2 += delta / (i + 1);
            Assert.True(0 <= result);
            Assert.True(result <= 1);
        }

        Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean2, iterations));
    }

    [Fact]
    public void SingleNonNullable()
    {
        Assert.Throws<ArgumentNullException>(() => ClosedOpen.Single.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => OpenClosed.Single.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Closed.Single.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Open.Single.Instance.Sample<StepRng>(null));
    }
    [Fact]
    public void DoubleRanges()
    {
        var zeroRng = new StepRng(0) { Increment = 0 };
        var maxRng = new StepRng(UInt64.MaxValue) { Increment = 0 };
        Double low, high;

        var closedOpen = ClosedOpen.Double.Instance;
        low = closedOpen.Sample(zeroRng);
        high = closedOpen.Sample(maxRng);

        Assert.Equal(0, low);
        Assert.True(0 < high && high < 1);

        var openClosed = OpenClosed.Double.Instance;
        low = openClosed.Sample(zeroRng);
        high = openClosed.Sample(maxRng);

        Assert.True(0 < low && low < 1);
        Assert.Equal(1, high);

        var closed = Closed.Double.Instance;
        low = closed.Sample(zeroRng);
        high = closed.Sample(maxRng);

        Assert.Equal(0, low);
        Assert.Equal(1, high);

        var open = Open.Double.Instance;
        low = open.Sample(zeroRng);
        high = open.Sample(maxRng);

        Assert.True(0 < low && low < 1);
        Assert.True(0 < high && high < 1);
    }

    public static IEnumerable<Object[]> DoubleParams(Int32 seedStart)
    {
        yield return new Object [] { OpenClosed.Double.Instance, seedStart };
        yield return new Object[] { ClosedOpen.Double.Instance, seedStart + 1 };
        yield return new Object[] { Closed.Double.Instance, seedStart + 2};
        yield return new Object[] { Open.Double.Instance, seedStart + 3};
    }

    [Theory]
    [MemberData(nameof(DoubleParams), 900)]
    public void DoubleAverage(IDistribution<Double> dist, UInt64 seed)
    {
        const Int32 iterations = 10_000;
        var rng = Pcg32.Create(seed, 11634580027462260723ul);

        Double mean = 0;
        for (var i = 0; i < iterations; i++)
        {
            var result = dist.Sample(rng);
            var delta = result - mean;
            mean += delta / (i + 1);
            Assert.True(0 <= result);
            Assert.True(result <= 1);
        }

        Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean, iterations));

        Double mean2 = 0;
        for (var i = 0; i < iterations; i++)
        {
            Assert.True(dist.TrySample(rng, out var result));
            var delta = result - mean2;
            mean2 += delta / (i + 1);
            Assert.True(0 <= result);
            Assert.True(result <= 1);
        }

        Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean2, iterations));
    }

    [Fact]
    public void DoubleNonNullable()
    {
        Assert.Throws<ArgumentNullException>(() => ClosedOpen.Double.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => OpenClosed.Double.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Closed.Double.Instance.Sample<StepRng>(null));
        Assert.Throws<ArgumentNullException>(() => Open.Double.Instance.Sample<StepRng>(null));
    }
}
