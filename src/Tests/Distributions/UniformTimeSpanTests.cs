using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions;

public sealed class UniformTimeSpanTests
{
    [Theory]
    [InlineData(100000000000, -100000000000)]
    [InlineData(Int64.MaxValue, Int64.MinValue)]
    public void BadInclusiveRange(Int64 highInt, Int64 lowInt)
    {
        var low = TimeSpan.FromTicks(lowInt);
        var high = TimeSpan.FromTicks(highInt);
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(high, low));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-100000000000, -100000000000)]
    [InlineData(100000000000, -100000000000)]
    [InlineData(100000000000, 100000000000)]
    [InlineData(Int64.MinValue, Int64.MinValue)]
    [InlineData(Int64.MaxValue, Int64.MinValue)]
    [InlineData(Int64.MaxValue, Int64.MaxValue)]
    public void BadExclusiveRange(Int64 highInt, Int64 lowInt)
    {
        var low = TimeSpan.FromTicks(lowInt);
        var high = TimeSpan.FromTicks(highInt);
        Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(high, low));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-100000000000, -100000000000)]
    [InlineData(-100000000000, 100000000000)]
    [InlineData(100000000000, 100000000000)]
    [InlineData(Int64.MinValue, Int64.MinValue)]
    [InlineData(Int64.MinValue, Int64.MaxValue)]
    [InlineData(Int64.MaxValue, Int64.MaxValue)]
    [InlineData(Int64.MinValue, 1)]
    [InlineData(0, Int64.MaxValue)]
    public void SampleInclusive(Int64 lowInt, Int64 highInt)
    {
        var low = TimeSpan.FromTicks(lowInt);
        var high = TimeSpan.FromTicks(highInt);
        var dist = Uniform.NewInclusive(low, high);
        var rng = Pcg32.Create(252, 11634580027462260723ul);

        for (var i = 0; i < 10000; i++)
        {
            var result = dist.Sample(rng);
            Assert.True(low <= result);
            Assert.True(result <= high);
        }
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-100000000000, 100000000000)]
    [InlineData(Int64.MinValue, Int64.MaxValue)]
    [InlineData(Int64.MinValue, 1)]
    [InlineData(0, Int64.MaxValue)]
    public void SampleExclusive(Int64 lowInt, Int64 highInt)
    {
        var low = TimeSpan.FromTicks(lowInt);
        var high = TimeSpan.FromTicks(highInt);
        var dist = Uniform.New(low, high);
        var rng = Pcg32.Create(252, 11634580027462260723ul);

        for (var i = 0; i < 10000; i++)
        {
            var result = dist.Sample(rng);
            Assert.True(low <= result);
            Assert.True(result < high);
        }
    }

    [Fact]
    public void Rejections()
    {
        const Int64 midpoint = Int64.MaxValue / 2 + Int64.MinValue / 2;
        const Int64 lowInt = Int64.MinValue;
        const Int64 highInt = midpoint + 1;
        const UInt64 maxRand = UInt64.MaxValue;
        const UInt64 rangeSize = unchecked(highInt - (UInt64)lowInt + 1);
        const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
        const UInt64 lastAccepted = maxRand - rejectCount;

        var low = TimeSpan.FromTicks(lowInt);
        var high = TimeSpan.FromTicks(highInt);
        var dist = Uniform.NewInclusive(low, high);
        var rng = new StepRng(lastAccepted - 1);

        Assert.True(dist.TrySample(rng, out TimeSpan result));
        Assert.Equal(midpoint, result.Ticks);
        Assert.True(dist.TrySample(rng, out result));
        Assert.Equal(midpoint + 1, result.Ticks);
        Assert.False(dist.TrySample(rng, out _));
        Assert.False(dist.TrySample(rng, out _));
        Assert.False(dist.TrySample(rng, out _));

        // Now test a blocking sample
        rng.State = maxRand - Math.Min(20, rejectCount) + 1;
        Assert.Equal(TimeSpan.MinValue, dist.Sample(rng));
    }

    [Fact]
    public void ZoneEqualToGeneratedInt64()
    {
        TimeSpan low = TimeSpan.FromTicks(Int64.MinValue);
        TimeSpan high = TimeSpan.FromTicks(Int64.MaxValue - 1);
        const UInt64 maxRand = UInt64.MaxValue;

        var rng = new StepRng(maxRand - 1) { Increment = 0 };
        var dist = Uniform.NewInclusive(low, high);

        Assert.Equal(UInt32.MaxValue - 1, rng.NextUInt32());
        Assert.Equal(high, dist.Sample(rng));
    }

    [Fact]
    public void FullRange()
    {
        var rng = new StepRng(UInt64.MaxValue - 4);
        var dist = Uniform.NewInclusive(TimeSpan.MinValue, TimeSpan.MaxValue);
        _ = dist.Sample(rng); // Sample shouldn't need to retry
        // Mix up Sample and TrySample for the fun of it
        Assert.Equal(UInt64.MaxValue - 3, rng.State);
        Assert.True(dist.TrySample(rng, out _));
        _ = dist.Sample(rng);
        Assert.True(dist.TrySample(rng, out _));
        Assert.Equal(UInt64.MaxValue, rng.State);

        // The full range is a special case, where the distribution doesn't need to add _low,
        // so it simply casts directly to the result. The upshot of which is that signed and
        // unsigned distributions will behave differently, so we have to do bitwise comparisons
        // instead of using type.MaxValue and type.MinValue.
        Assert.Equal(TimeSpan.FromTicks(-1), dist.Sample(rng)); // 0
        Assert.True(dist.TrySample(rng, out TimeSpan result)); // RNG wraps around to 0
        Assert.Equal(TimeSpan.Zero, result);
    }

    [Fact]
    public void ValueStability()
    {
        var rng = Pcg32.Create(897, 11634580027462260723ul);
        var dist = Uniform.New(TimeSpan.FromTicks(50), TimeSpan.FromTicks(200_000_000_000));
        var expectedValues = new[] { TimeSpan.FromTicks(113474105527), TimeSpan.FromTicks(151896112770), TimeSpan.FromTicks(88763480610) };
        foreach (var expected in expectedValues)
            Assert.Equal(expected, dist.Sample(rng));
    }

    [Fact]
    public void ValueInclusiveStability()
    {
        var rng = Pcg32.Create(897, 11634580027462260723ul);
        var dist = Uniform.NewInclusive(TimeSpan.FromTicks(50), TimeSpan.FromTicks(200_000_000_000));
        var expectedValues = new[] { TimeSpan.FromTicks(113449542990), TimeSpan.FromTicks(151820535227), TimeSpan.FromTicks(88694964445) };
        foreach (var expected in expectedValues)
            Assert.Equal(expected, dist.Sample(rng));
    }
}
