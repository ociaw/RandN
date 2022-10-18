using System;
using System.Collections.Generic;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions;

public class UniformDecimalTests
{
    [Fact]
    public void BadRange()
    {
        // Equal
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0m, 0m));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Decimal.MaxValue, Decimal.MaxValue));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Decimal.MinValue, Decimal.MinValue));
        // Reversed
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10.0m, 10.0m));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(9001.0m, -666.0m));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Decimal.MaxValue, Decimal.MinValue));

        // Reversed
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10.0m, 9.0m));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(9001.0m, -666.0m));
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(Decimal.MaxValue, Decimal.MinValue));
    }

    public static IEnumerable<Object[]> RangeParams() => new[]
    {
        new Object[] { 0, 1 },
        new Object[] { -1, 0 },
        new Object[] { 0.0m, 100.0m },
        new Object[] { -1e28m, -1e25m },
        new Object[] { 1e-28m, 1e-25m },
        new Object[] { 0ul, 3ul },
        new Object[] { -10, -1 },
        new Object[] { -5, 0.0m },
        new Object[] { -7, -0.0m },
        // The next two need to decrement the scale when building the distribution.
        new Object[] { 5, Decimal.MaxValue - 5 }, // This simply produces an initial maximum larger than high.
        new Object[] { 10.0m, Decimal.MaxValue }, // This does the same, but causes an overflow in the process.
        new Object[] { -100.0m, Decimal.MaxValue },
        new Object[] { -Decimal.MaxValue / 5.0m, Decimal.MaxValue },
        new Object[] { -Decimal.MaxValue, Decimal.MaxValue / 5.0m },
        new Object[] { -Decimal.MaxValue * 0.8m, Decimal.MaxValue * 0.7m },
        new Object[] { -Decimal.MaxValue, Decimal.MaxValue },
        new Object[] { 0m, new Decimal(-1, -1, -1, false, 28) },
    };

    [Theory]
    [MemberData(nameof(RangeParams))]
    public void FloatTest(Decimal low, Decimal high)
    {
        var rng = Pcg32.Create(252, 11634580027462260723ul);
        var zeroRng = new StepRng(0) { Increment = 0 };
        var maxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = 0 };

        var exclusiveUniform = Uniform.New(low, high);
        var inclusiveUniform = Uniform.NewInclusive(low, high);
        for (var i = 0; i < 100; i++)
        {
            var exclusive = exclusiveUniform.Sample(rng);
            Assert.True(low <= exclusive && exclusive < high);
            var inclusive = exclusiveUniform.Sample(rng);
            Assert.True(low <= inclusive && inclusive <= high);
        }

        Assert.Equal(low, Uniform.NewInclusive(low, low).Sample(rng));

        Assert.Equal(low, exclusiveUniform.Sample(zeroRng));
        Assert.Equal(low, inclusiveUniform.Sample(zeroRng));
        Assert.True(exclusiveUniform.Sample(maxRng) < high);
        Assert.True(inclusiveUniform.Sample(maxRng) <= high);

        var loweringMaxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = unchecked((UInt64)(-1L << 12)) };
        Assert.True(exclusiveUniform.Sample(loweringMaxRng) < high);
        Assert.True(exclusiveUniform.Sample(loweringMaxRng) < high);

        var maxDoubleInclusive = Uniform.NewInclusive(Decimal.MaxValue, Decimal.MaxValue);
        Assert.Equal(Decimal.MaxValue, maxDoubleInclusive.Sample(rng));
        var minDoubleInclusive = Uniform.NewInclusive(-Decimal.MaxValue, -Decimal.MaxValue);
        Assert.Equal(-Decimal.MaxValue, minDoubleInclusive.Sample(rng));
    }

    public static IEnumerable<Object[]> AverageParams() => new[]
    {
        new Object[] {0m, 1000m, 0},
        new Object[] {0m, 1m, 1},
        new Object[] {-50.0m, 50.0m, 2},
        new Object[] {0m, Decimal.MaxValue, 3},
        new Object[] {Decimal.MinValue, 0m, 4},
        new Object[] {38.9m, 64.6m, 5},
        new Object[] {1e-28m, 1e-24m, 6},
    };


    [Theory]
    [MemberData(nameof(AverageParams))]
    public void AverageDecimals(Decimal low, Decimal high, UInt64 seed)
    {
        const Int32 iterations = 10_000;
        const Decimal sqr3 = 1.7320508075688772935274463415m;

        var populationMean = high / 2 + low / 2;
        var popStdDev = 1.0m / sqr3 * (high / 2 - low / 2);

        var exclusiveDist = Uniform.New(low, high);
        var inclusiveDist = Uniform.NewInclusive(low, high);
        var rng = Pcg32.Create(789 + seed, 11634580027462260723ul);

        Decimal exclusiveMean = 0;
        Decimal inclusiveMean = 0;
        for (var i = 0; i < iterations; i++)
        {
            var exclusive = exclusiveDist.Sample(rng);
            var exclusiveDelta = exclusive - exclusiveMean;
            exclusiveMean += exclusiveDelta / (i + 1);
            Assert.True(low <= exclusive);
            Assert.True(exclusive < high);

            var inclusive = inclusiveDist.Sample(rng);
            var inclusiveDelta = inclusive - inclusiveMean;
            inclusiveMean += inclusiveDelta / (i + 1);
            Assert.True(low <= inclusive);
            Assert.True(inclusive < high);
        }

        Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, exclusiveMean, iterations));
        Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, inclusiveMean, iterations));
    }

    [Fact]
    public void TrySample()
    {
        var rng = Pcg32.Create(789, 11634580027462260723ul);
        var dist = Uniform.New(0m, 100m);
        for (Int32 i = 0; i< 10_000; i++)
            Assert.True(dist.TrySample(rng, out _));
    }

    [Fact]
    public void NonNullable()
    {
        var dist = Uniform.Decimal.Create(1, 2);
        Assert.Throws<ArgumentNullException>(() => dist.Sample<StepRng>(null));
    }
}