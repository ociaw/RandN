using System;
using System.Numerics;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions;

public class UniformBigIntegerTests
{
    [Theory]
    [InlineData(100000000, -100000000)]
    [InlineData(Int32.MaxValue, Int32.MinValue)]
    public void BadInclusiveRange(Int32 lowInt32, Int32 highInt32)
    {
        BigInteger low = lowInt32;
        BigInteger high = highInt32;
        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(low, high));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-100000000, -100000000)]
    [InlineData(100000000, -100000000)]
    [InlineData(100000000, 100000000)]
    [InlineData(Int32.MinValue, Int32.MinValue)]
    [InlineData(Int32.MaxValue, Int32.MinValue)]
    [InlineData(Int32.MaxValue, Int32.MaxValue)]
    public void BadExclusiveRange(Int32 lowInt32, Int32 highInt32)
    {
        BigInteger low = lowInt32;
        BigInteger high = highInt32;
        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(low, high));
    }

    /// <summary>
    /// Generates a bunch of numbers and ensures they all fall inside the specified inclusive range.
    /// </summary>
    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 2)]
    [InlineData(0, 3)]
    [InlineData(0, 4)]
    [InlineData(0, 5)]
    [InlineData(0, 6)]
    [InlineData(0, 7)]
    [InlineData(0, 8)]
    [InlineData(-100000000, -100000000)]
    [InlineData(-100000000, 100000000)]
    [InlineData(100000000, 100000000)]
    [InlineData(Int32.MinValue, Int32.MinValue)]
    [InlineData(Int32.MinValue, Int32.MaxValue)]
    [InlineData(Int32.MaxValue, Int32.MaxValue)]
    [InlineData(Int32.MinValue, 1)]
    [InlineData(0, Int32.MaxValue)]
    public void SampleInclusiveSmall(Int32 lowInt32, Int32 highInt32)
    {
        BigInteger low = lowInt32;
        BigInteger high = highInt32;

        var dist = Uniform.NewInclusive(low, high);
        var rng = Pcg32.Create(252, 11634580027462260723ul);

        for (var i = 0; i < 10000; i++)
        {
            var result = dist.Sample(rng);
            Assert.True(low <= result);
            Assert.True(result <= high);
        }

        for (var i = 0; i < 10000; i++)
        {
            if (!dist.TrySample(rng, out var result))
                continue;

            Assert.True(low <= result);
            Assert.True(result <= high);
        }
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-100000000, 100000000)]
    [InlineData(Int32.MinValue, Int32.MaxValue)]
    [InlineData(Int32.MinValue, 1)]
    [InlineData(0, Int32.MaxValue)]
    public void SampleExclusiveSmall(Int32 lowInt32, Int32 highInt32)
    {
        BigInteger low = lowInt32;
        BigInteger high = highInt32;

        var dist = Uniform.New(low, high);
        var rng = Pcg32.Create(252, 11634580027462260723ul);

        for (var i = 0; i < 10000; i++)
        {
            var result = dist.Sample(rng);
            Assert.True(low <= result);
            Assert.True(result < high);
        }

        for (var i = 0; i < 10000; i++)
        {
            if (!dist.TrySample(rng, out var result))
                continue;

            Assert.True(low <= result);
            Assert.True(result < high);
        }
    }

    [Fact]
    public void SampleInclusiveLarge()
    {
        BigInteger low = new BigInteger([234, 221, 31, 123, 12, 42, 33, 21, 12, 98, 187, 76, 154, 44, 3]);
        BigInteger high = new BigInteger([64, 211, 54, 109, 11, 22, 33, 44, 55, 66, 77, 176, 54, 144, 13]);
        var dist = Uniform.NewInclusive(low, high);
        var rng = Pcg32.Create(252, 11634580027462260723ul);

        for (var i = 0; i < 10000; i++)
        {
            var result = dist.Sample(rng);
            Assert.True(low <= result);
            Assert.True(result <= high);
        }

        for (var i = 0; i < 10000; i++)
        {
            if (!dist.TrySample(rng, out var result))
                continue;

            Assert.True(low <= result);
            Assert.True(result <= high);
        }
    }

    public static TheoryData<BigInteger, BigInteger, UInt64> AverageParams => new()
    {
        { -new BigInteger(UInt64.MaxValue), new BigInteger(UInt64.MaxValue), 0 },
        { new BigInteger(-1_000_000), new BigInteger(1_000_000), 1 },
        { new BigInteger(Decimal.MinValue), BigInteger.Zero, 2 },
        { BigInteger.Zero, new BigInteger(1_000_000), 3 },
        { BigInteger.Zero, BigInteger.One, 4 },
        { BigInteger.Zero, new BigInteger(Decimal.MaxValue), 5 },
        { new BigInteger(10), new BigInteger(110), 6 },
        { new BigInteger(Decimal.MinValue), new BigInteger(Decimal.MaxValue), 7 },
        { BigInteger.Pow(new BigInteger(Decimal.MinValue), 17), BigInteger.Pow(new BigInteger(Decimal.MaxValue), 17), 8 },
        { BigInteger.Zero, new BigInteger(2), 9 },
    };

    [Theory]
    [MemberData(nameof(AverageParams))]
    public void Average(BigInteger low, BigInteger high, UInt64 seed)
    {
        const Int32 iterations = 10_000;

        var sqr3InverseNumerator = new BigInteger(1.0m / 1.7320508075688772935274463415m * 10000000000000000000000000000m);
        var sqr3InverseDenominator = new BigInteger(10000000000000000000000000000m);

        var populationMean = (high + low) / 2;
        var popStdDev = (high - low) / 2 * sqr3InverseNumerator / sqr3InverseDenominator;

        var exclusiveDist = Uniform.New(low, high);
        var inclusiveDist = Uniform.NewInclusive(low, high);
        var rng = Pcg32.Create(789 + seed, 11634580027462260726ul);

        BigInteger exclusiveSum = 0;
        BigInteger inclusiveSum = 0;
        for (var i = 0; i < iterations; i++)
        {
            var exclusive = exclusiveDist.Sample(rng);
            exclusiveSum += exclusive;
            Assert.True(low <= exclusive);
            Assert.True(exclusive < high);

            var inclusive = inclusiveDist.Sample(rng);
            inclusiveSum += inclusive;
            Assert.True(low <= inclusive);
            Assert.True(inclusive <= high);
        }

        BigInteger exclusiveMean = exclusiveSum / iterations;
        BigInteger inclusiveMean = inclusiveSum / iterations;

        Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, exclusiveMean, iterations));
        Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, inclusiveMean, iterations));
    }
    
    [Fact]
    public void Rejections()
    {
        var rng = new SequenceRng(new[] { 0b10000000u, 0b10000001u, 0x81u, 0x81u });
        var lower = BigInteger.Zero;
        var upper = new BigInteger(0b10000000u);

        var dist = Uniform.NewInclusive(lower, upper);
        Assert.True(dist.TrySample(rng, out var result));
        Assert.Equal(0b10000000u, result);
        Assert.False(dist.TrySample(rng, out _));
    }
}
