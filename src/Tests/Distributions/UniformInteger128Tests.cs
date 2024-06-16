#if NET8_0_OR_GREATER

using System;
using System.Linq;
using RandN.Implementation;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions;

public sealed class UniformInteger128Tests
{
    /// <summary>
    /// Test that invalid inclusive ranges are correctly rejected.
    /// </summary>
    [Theory]
    [InlineData(0, 50ul, 0, 200000000000ul)]
    [InlineData(0, 0, UInt64.MaxValue, UInt64.MaxValue)]
    public void BadInclusiveRangeUInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new UInt128(highUpper, highLower);
        var low = new UInt128(lowUpper, lowLower);

        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
    }

    /// <summary>
    /// Test that invalid exclusive ranges are correctly rejected.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 50, 0, 50)]
    [InlineData(0, 50, 0, 200000000000)]
    [InlineData(0, 200000000000, 0, 200000000000)]
    [InlineData(0, 0, UInt64.MaxValue, UInt64.MaxValue)]
    [InlineData(UInt64.MaxValue, UInt64.MaxValue, UInt64.MaxValue, UInt64.MaxValue)]
    public void BadExclusiveRangeUInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new UInt128(highUpper, highLower);
        var low = new UInt128(lowUpper, lowLower);

        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
    }

    /// <summary>
    /// Generates a bunch of numbers and ensures they all fall inside the specified inclusive range.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 50, 0, 50)]
    [InlineData(0, 50, 0, 200000000000)]
    [InlineData(0, 200000000000, 0, 200000000000)]
    [InlineData(0, 0, UInt64.MaxValue, UInt64.MaxValue)]
    [InlineData(UInt64.MaxValue, UInt64.MaxValue, UInt64.MaxValue, UInt64.MaxValue)]
    [InlineData(0, 0, 0, 1)]
    public void SampleInclusiveUInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new UInt128(highUpper, highLower);
        var low = new UInt128(lowUpper, lowLower);

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

    /// <summary>
    /// Generates a bunch of numbers and ensures they all fall inside the specified exclusive range.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 1)]
    [InlineData(0, 50, 0, 200000000000)]
    [InlineData(0, 0, UInt64.MaxValue, UInt64.MaxValue)]
    public void SampleExclusiveUInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new UInt128(highUpper, highLower);
        var low = new UInt128(lowUpper, lowLower);

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

    /// <summary>
    /// Tests that RNG generated values that fall outside the range are properly rejected.
    /// </summary>
    [Fact]
    public void RejectionsUInt128()
    {
        UInt128 midpoint = UInt128.MaxValue / 2;
        UInt128 low = 0;
        UInt128 high = midpoint + 1;

        var sequence = Enumerable.Range(0, 10).Select(x => UInt128.MaxValue / 2 + (UInt128)x);

        var dist = Uniform.NewInclusive(low, high);
        var rng = new SequenceRng(sequence.SelectMany(x => new[] { x.IsolateLow(), x.IsolateHigh() }));

        Assert.False(dist.TrySample(rng, out _));
        Assert.True(dist.TrySample(rng, out UInt128 result));
        Assert.Equal(new UInt128(0x4000000000000000, 0), result);
        Assert.True(dist.TrySample(rng, out result));
        Assert.Equal(new UInt128(0x4000000000000000, 1), result);
        Assert.False(dist.TrySample(rng, out _));
        Assert.True(dist.TrySample(rng, out result));
        Assert.Equal(new UInt128(0x4000000000000000, 2), result);
        Assert.False(dist.TrySample(rng, out _));
        Assert.True(dist.TrySample(rng, out result));
        Assert.Equal(new UInt128(0x4000000000000000, 3), result);
        Assert.False(dist.TrySample(rng, out _));

        // Now test a blocking sample
        Assert.Equal(new UInt128(0x4000000000000000, 4), dist.Sample(rng));
    }

    /// <summary>
    /// Tests a full range distribution at near the minimum and maximums.
    /// </summary>
    [Fact]
    public void FullRangeUInt128()
    {
        var rng = new StepRng(UInt64.MaxValue - 4);
        var dist = Uniform.NewInclusive(UInt128.MinValue, UInt128.MaxValue);
        _ = dist.Sample(rng); // Sample shouldn't need to retry
        // Mix up Sample and TrySample for the fun of it
        Assert.Equal(UInt64.MaxValue - 2, rng.State);
        Assert.True(dist.TrySample(rng, out _));
        Assert.Equal(UInt64.MaxValue, rng.State);
        Assert.Equal(new UInt128(0x0000000000000000, 0xFFFFFFFFFFFFFFFF), dist.Sample(rng));
        Assert.True(dist.TrySample(rng, out _));
        Assert.Equal(new UInt128(0x0000000000000004, 0x0000000000000003), dist.Sample(rng));
    }

    /// <summary>
    /// Ensures that the values generated don't accidentally change from version to version 
    /// </summary>
    [Fact]
    public void UInt128Stability()
    {
        var rng = Pcg32.Create(897, 11634580027462260723ul);
        var dist = Uniform.New(new UInt128(0, 50), new UInt128(2_000, 200_000_000_000ul));
        var expectedValues = new[]
        {
            new UInt128(0x666, 0xD3A5622DD2B654F2),
            new UInt128(0x45D, 0x5667CA7B62179B9D),
            new UInt128(0x4D4, 0x81F33768224909BA),
        };
        foreach (var expected in expectedValues)
            Assert.Equal(expected, dist.Sample(rng));
    }

    /// <summary>
    /// Test that invalid inclusive ranges are correctly rejected.
    /// </summary>
    [Theory]
    [InlineData(0, 100000000000, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800)]
    [InlineData(0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000)]
    public void BadInclusiveRangeInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new Int128(highUpper, highLower);
        var low = new Int128(lowUpper, lowLower);

        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(low, high));
    }

    /// <summary>
    /// Test that invalid exclusive ranges are correctly rejected.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800)]
    [InlineData(0, 100000000000, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800)]
    [InlineData(0, 100000000000, 0, 100000000000)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0x8000000000000000, 0x0000000000000000)]
    [InlineData(0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0x8000000000000000, 0x0000000000000000)]
    [InlineData(0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    public void BadExclusiveRangeInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new Int128(highUpper, highLower);
        var low = new Int128(lowUpper, lowLower);

        Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(low, high));
    }

    /// <summary>
    /// Generates a bunch of numbers and ensures they all fall inside the specified inclusive range.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800, 0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800)]
    [InlineData(0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800, 0, 100000000000)]
    [InlineData(0, 100000000000, 0, 100000000000)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0x8000000000000000, 0x0000000000000000)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    [InlineData(0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0, 1)]
    [InlineData(0, 0, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    public void SampleInclusiveInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new Int128(highUpper, highLower);
        var low = new Int128(lowUpper, lowLower);

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

    /// <summary>
    /// Generates a bunch of numbers and ensures they all fall inside the specified exclusive range.
    /// </summary>
    [Theory]
    [InlineData(0, 0, 0, 1)]
    [InlineData(0xFFFFFFFFFFFFFFFF, 0xFFFFFFE8B7891800, 0, 100000000000)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    [InlineData(0x8000000000000000, 0x0000000000000000, 0, 1)]
    [InlineData(0, 0, 0x7FFFFFFFFFFFFFFF, 0xFFFFFFFFFFFFFFFF)]
    public void SampleExclusiveInt128(UInt64 lowUpper, UInt64 lowLower, UInt64 highUpper, UInt64 highLower)
    {
        var high = new Int128(highUpper, highLower);
        var low = new Int128(lowUpper, lowLower);

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

    /// <summary>
    /// Tests that RNG generated values that fall outside the range are properly rejected.
    /// </summary>
    [Fact]
    public void RejectionsInt128()
    {
        Int128 midpoint = 0;
        Int128 low = Int128.MinValue;
        Int128 high = midpoint + 1;

        UInt128 maxRand = UInt128.MaxValue;
        UInt128 rangeSize = unchecked((UInt128)high - (UInt128)low + 1);
        UInt128 rejectCount = (maxRand - rangeSize + 1) % rangeSize;

        UInt128 lastAccepted = maxRand - rejectCount;
        UInt128 penultimateAccepted = lastAccepted - 1;
        UInt128 firstRejected = lastAccepted + 1;
        UInt128 secondRejected = firstRejected + 1;
        UInt128 thirdRejected = secondRejected + 1;

        var dist = Uniform.NewInclusive(low, high);
        var rng = new SequenceRng([
            penultimateAccepted.IsolateLow(),
            penultimateAccepted.IsolateHigh(),
            lastAccepted.IsolateLow(),
            lastAccepted.IsolateHigh(),
            firstRejected.IsolateLow(),
            firstRejected.IsolateHigh(),
            secondRejected.IsolateLow(),
            secondRejected.IsolateHigh(),
            thirdRejected.IsolateLow(),
            thirdRejected.IsolateHigh(),

            firstRejected.IsolateLow(),
            firstRejected.IsolateHigh(),
            secondRejected.IsolateLow(),
            secondRejected.IsolateHigh(),
            thirdRejected.IsolateLow(),
            thirdRejected.IsolateHigh(),
            0,
            0,
        ]);

        Assert.True(dist.TrySample(rng, out Int128 result));
        Assert.Equal(midpoint, result);
        Assert.True(dist.TrySample(rng, out result));
        Assert.Equal(midpoint + 1, result);
        Assert.False(dist.TrySample(rng, out _));
        Assert.False(dist.TrySample(rng, out _));
        Assert.False(dist.TrySample(rng, out _));

        // Now test a blocking sample
        Assert.Equal(Int128.MinValue, dist.Sample(rng));
    }

    [Fact]
    public void ZoneEqualToGeneratedInt128()
    {
        Int128 low = Int128.MinValue;
        Int128 high = Int128.MaxValue - 1;
        UInt128 rngState = UInt128.MaxValue - 1;

        var rng = new SequenceRng([rngState.IsolateLow(), rngState.IsolateHigh()]);
        var dist = Uniform.NewInclusive(low, high);

        Assert.Equal(UInt32.MaxValue - 1, rng.NextUInt32());
        Assert.Equal(UInt32.MaxValue, rng.NextUInt32());
        Assert.Equal(UInt32.MaxValue, rng.NextUInt32());
        Assert.Equal(UInt32.MaxValue, rng.NextUInt32());
        Assert.Equal(high, dist.Sample(rng));
    }

    /// <summary>
    /// Tests a full range distribution at near the minimum and maximums.
    /// </summary>
    [Fact]
    public void FullRangeInt128()
    {
        var rng = new StepRng(UInt64.MaxValue - 4);
        var dist = Uniform.NewInclusive(Int128.MinValue, Int128.MaxValue);
        _ = dist.Sample(rng); // Sample shouldn't need to retry
        // Mix up Sample and TrySample for the fun of it
        Assert.Equal(UInt64.MaxValue - 2, rng.State);
        Assert.True(dist.TrySample(rng, out _));
        Assert.Equal(UInt64.MaxValue, rng.State);
        Assert.Equal(new Int128(0x0000000000000000, 0xFFFFFFFFFFFFFFFF), dist.Sample(rng));
        Assert.True(dist.TrySample(rng, out _));
        Assert.Equal(new Int128(0x0000000000000004, 0x0000000000000003), dist.Sample(rng));
    }

    /// <summary>
    /// Ensures that the values generated don't accidentally change from version to version 
    /// </summary>
    [Fact]
    public void Int128Stability()
    {
        var rng = Pcg32.Create(897, 11634580027462260723ul);
        var dist = Uniform.New(new Int128(0, 50), new Int128(2_000, 200_000_000_000ul));
        var expectedValues = new[]
        {
            new Int128(0x0E9, 0x2C2DE5AC5A44EFE3),
            new Int128(0x04C, 0xB23B6C6817A5B06C),
            new Int128(0x1F6, 0xBD76F937FCFCFB8D),
        };
        foreach (var expected in expectedValues)
            Assert.Equal(expected, dist.Sample(rng));
    }
}
#endif
