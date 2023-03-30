using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions;

public class BernoulliTests
{
    [Fact]
    public void TrivialRatios()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Bernoulli.FromRatio(11, 10));
        var alwaysTrue = Bernoulli.FromRatio(10, 10);
        var alwaysFalse = Bernoulli.FromRatio(0, 10);
        var rng = Mt1993764.Create(5489);

        for (Int32 i = 0; i < 10; i++)
        {
            Assert.True(alwaysTrue.TrySample(rng, out Boolean definitelyTrue));
            Assert.True(alwaysFalse.TrySample(rng, out Boolean definitelyFalse));
            Assert.True(definitelyTrue);
            Assert.False(definitelyFalse);
        }
    }

    [Fact]
    public void TrivialFloat()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Bernoulli.FromP(1.1));
        Assert.Throws<ArgumentOutOfRangeException>(() => Bernoulli.FromP(-0.5));
        var alwaysTrue = Bernoulli.FromP(1.0);
        var alwaysFalse = Bernoulli.FromP(0.0);
        var rng = Mt1993764.Create(5489);

        for (Int32 i = 0; i < 10; i++)
        {
            Assert.True(alwaysTrue.TrySample(rng, out Boolean definitelyTrue));
            Assert.True(alwaysFalse.TrySample(rng, out Boolean definitelyFalse));
            Assert.True(definitelyTrue);
            Assert.False(definitelyFalse);
        }
    }

    [Fact]
    public void Average()
    {
        const UInt32 numerator = 3;
        const UInt32 denominator = 10;
        const Double p = (Double)numerator / denominator;
        const UInt64 sampleCount = 100_000;

        var rng1 = Mt1993764.Create(5489);
        var rng2 = Mt1993764.Create(5489);
        var ratioDist = Bernoulli.FromRatio(numerator, denominator);
        var pDist = Bernoulli.FromP(p);

        UInt64 sum = 0;
        for (UInt64 i = 0; i < sampleCount; i++)
        {
            Assert.True(ratioDist.TrySample(rng1, out Boolean result1));
            Assert.True(pDist.TrySample(rng2, out Boolean result2));
            Assert.Equal(result1, result2);

            if (result1)
                sum++;
        }

        Assert.True(Statistics.WithinConfidenceBernoulli(sum, p, sampleCount));
    }

    [Fact]
    public void NonNullable()
    {
        var dist = Bernoulli.FromRatio(1, 2);
        Assert.Throws<ArgumentNullException>(() => dist.Sample<StepRng>(null));
    }

    [Fact]
    public void UnlikelyFalse()
    {
        // This distribution normally has a probably of success of 2^64 - 1 / 2^64,
        // so this is very unlikely to return false.
        var dist = Bernoulli.FromInverse(UInt64.MaxValue);
        // But we'll force it to anyway.
        var rng = new StepRng(UInt64.MaxValue);
        Assert.False(dist.Sample(rng));
        // The next 2^64 - 1 samples should return true.
        Assert.True(dist.Sample(rng));
        Assert.True(dist.Sample(rng));
        Assert.True(dist.Sample(rng));
        Assert.True(dist.Sample(rng));
    }
    
    [Theory]
    [InlineData(0x7fff_ffff_ffff_fffful, 1u, 0x002u)]
    [InlineData(0x3fff_ffff_ffff_fffful, 1u, 0x004u)]
    [InlineData(0x1fff_ffff_ffff_fffful, 1u, 0x008u)]
    [InlineData(0x0fff_ffff_ffff_fffful, 1u, 0x010u)]
    [InlineData(0x07ff_ffff_ffff_fffful, 1u, 0x020u)]
    [InlineData(0x03ff_ffff_ffff_fffful, 1u, 0x040u)]
    [InlineData(0x01ff_ffff_ffff_fffful, 1u, 0x080u)]
    [InlineData(0x00ff_ffff_ffff_fffful, 1u, 0x100u)]
    [InlineData(0x1_0000_0000ul, 1d, UInt32.MaxValue)]
    [InlineData(0x2_0000_0001ul, 2d, UInt32.MaxValue)]
    [InlineData(0x3_0000_0002ul, 3d, UInt32.MaxValue)]
    [InlineData(0x4_0000_0003ul, 4d, UInt32.MaxValue)]
    public void RatioThreshold(UInt64 rngSeed, UInt32 nominator, UInt32 denominator)
    {
        var rng = new StepRng(rngSeed);
        var dist = Bernoulli.FromRatio(nominator, denominator);

        // We ensure that threshold from true to false lies at the provided RNG seed.
        Assert.True(dist.Sample(rng));
        Assert.False(dist.Sample(rng));
    }
    
    [Theory]
    [InlineData(0x7fff_ffff_ffff_fffful)]
    [InlineData(0x3fff_ffff_ffff_fffful)]
    [InlineData(0x1fff_ffff_ffff_fffful)]
    [InlineData(0x0fff_ffff_ffff_fffful)]
    [InlineData(0x07ff_ffff_ffff_fffful)]
    [InlineData(0x03ff_ffff_ffff_fffful)]
    [InlineData(0x01ff_ffff_ffff_fffful)]
    [InlineData(0x00ff_ffff_ffff_fffful)]
    [InlineData(0x0000ul)]
    [InlineData(0x0001ul)]
    [InlineData(0x0002ul)]
    [InlineData(0x0003ul)]
    [InlineData(0x0004ul)]
    public void InverseThreshold(UInt64 rngSeed)
    {
        var rng = new StepRng(rngSeed);
        var dist = Bernoulli.FromInverse(rngSeed + 1);

        // We ensure that threshold from true to false lies at the provided RNG seed.
        Assert.True(dist.Sample(rng));
        Assert.False(dist.Sample(rng));
    }
    
    [Theory]
    [InlineData(0x7fff_ffff_ffff_fffful, .5d)]
    [InlineData(0x3fff_ffff_ffff_fffful, .25d)]
    [InlineData(0x1fff_ffff_ffff_fffful, .125d)]
    [InlineData(0x0fff_ffff_ffff_fffful, .0625d)]
    [InlineData(0x07ff_ffff_ffff_fffful, .03125d)]
    [InlineData(0x03ff_ffff_ffff_fffful, .015625d)]
    [InlineData(0x01ff_ffff_ffff_fffful, .0078125d)]
    [InlineData(0x00ff_ffff_ffff_fffful, .00390625d)]
    [InlineData(0x0000ul, 1d / UInt64.MaxValue)]
    [InlineData(0x0001ul, 2d / UInt64.MaxValue)]
    [InlineData(0x0002ul, 3d / UInt64.MaxValue)]
    [InlineData(0x0003ul, 4d / UInt64.MaxValue)]
    public void PThreshold(UInt64 rngSeed, Double p)
    {
        var rng = new StepRng(rngSeed);
        var dist = Bernoulli.FromP(p);

        // We ensure that threshold from true to false lies at the provided RNG seed.
        Assert.True(dist.Sample(rng));
        Assert.False(dist.Sample(rng));
    }

    [Fact]
    public void DoesNotCallRngWhenAlwaysTrue()
    {
        var rng = new ThrowingRng();
        var dist = Bernoulli.FromP(1);
        dist.Sample(rng);
        dist.Sample(rng);
    }

    [Fact]
    public void ValueStability()
    {
        // The values used in this test are pulled from the Rand rust crate:
        // https://github.com/rust-random/rand/blob/c6cca9cb81c72441c33b78fc74e3a5b33358334b/src/distributions/bernoulli.rs#L204
        var rng = Pcg32.Create(3, 11634580027462260723ul);
        var dist = Bernoulli.FromP(0.4532);
        var expected = new[] { true, false, false, true, false, false, true, true, true, true };
        foreach(var val in expected)
            Assert.Equal(val, dist.Sample(rng));
    }
}
