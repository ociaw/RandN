using System;
using Xunit;
using RandN.Rngs;
// ReSharper disable RedundantCast
// ReSharper disable RedundantOverflowCheckingContext
// ReSharper disable UnreachableCode

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    public sealed class UniformIntegerTests
    {
        [Theory]
        [InlineData(200, 50)]
        [InlineData(Byte.MaxValue, Byte.MinValue)]
        public void BadInclusiveRangeByte(Byte high, Byte low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(200, 50)]
        [InlineData(200, 200)]
        [InlineData(Byte.MinValue, Byte.MinValue)]
        [InlineData(Byte.MaxValue, Byte.MinValue)]
        [InlineData(Byte.MaxValue, Byte.MaxValue)]
        public void BadExclusiveRangeByte(Byte high, Byte low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(50, 200)]
        [InlineData(200, 200)]
        [InlineData(Byte.MinValue, Byte.MinValue)]
        [InlineData(Byte.MinValue, Byte.MaxValue)]
        [InlineData(Byte.MaxValue, Byte.MaxValue)]
        [InlineData(Byte.MinValue, 1)]
        [InlineData(0, Byte.MaxValue)]
        public void SampleInclusiveByte(Byte low, Byte high)
        {
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
        [InlineData(50, 200)]
        [InlineData(Byte.MinValue, Byte.MaxValue)]
        [InlineData(Byte.MinValue, 1)]
        [InlineData(0, Byte.MaxValue)]
        public void SampleExclusiveByte(Byte low, Byte high)
        {
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
        public void RejectionsByte()
        {
            const Byte midpoint = Byte.MaxValue / 2 + Byte.MinValue / 2;
            const Byte low = Byte.MinValue;
            const Byte high = midpoint + 1;
            const UInt64 maxRand = sizeof(Byte) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out Byte result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(Byte.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeByte()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(Byte.MinValue, Byte.MaxValue);
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
            Assert.Equal(unchecked((Byte)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out Byte result)); // RNG wraps around to 0
            Assert.Equal((Byte)0, result);
        }
        [Theory]
        [InlineData(2000, 50)]
        [InlineData(UInt16.MaxValue, UInt16.MinValue)]
        public void BadInclusiveRangeUInt16(UInt16 high, UInt16 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(2000, 50)]
        [InlineData(2000, 2000)]
        [InlineData(UInt16.MinValue, UInt16.MinValue)]
        [InlineData(UInt16.MaxValue, UInt16.MinValue)]
        [InlineData(UInt16.MaxValue, UInt16.MaxValue)]
        public void BadExclusiveRangeUInt16(UInt16 high, UInt16 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(50, 2000)]
        [InlineData(2000, 2000)]
        [InlineData(UInt16.MinValue, UInt16.MinValue)]
        [InlineData(UInt16.MinValue, UInt16.MaxValue)]
        [InlineData(UInt16.MaxValue, UInt16.MaxValue)]
        [InlineData(UInt16.MinValue, 1)]
        [InlineData(0, UInt16.MaxValue)]
        public void SampleInclusiveUInt16(UInt16 low, UInt16 high)
        {
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
        [InlineData(50, 2000)]
        [InlineData(UInt16.MinValue, UInt16.MaxValue)]
        [InlineData(UInt16.MinValue, 1)]
        [InlineData(0, UInt16.MaxValue)]
        public void SampleExclusiveUInt16(UInt16 low, UInt16 high)
        {
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
        public void RejectionsUInt16()
        {
            const UInt16 midpoint = UInt16.MaxValue / 2 + UInt16.MinValue / 2;
            const UInt16 low = UInt16.MinValue;
            const UInt16 high = midpoint + 1;
            const UInt64 maxRand = sizeof(UInt16) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out UInt16 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(UInt16.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeUInt16()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(UInt16.MinValue, UInt16.MaxValue);
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
            Assert.Equal(unchecked((UInt16)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out UInt16 result)); // RNG wraps around to 0
            Assert.Equal((UInt16)0, result);
        }
        [Theory]
        [InlineData(200000000, 50)]
        [InlineData(UInt32.MaxValue, UInt32.MinValue)]
        public void BadInclusiveRangeUInt32(UInt32 high, UInt32 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(200000000, 50)]
        [InlineData(200000000, 200000000)]
        [InlineData(UInt32.MinValue, UInt32.MinValue)]
        [InlineData(UInt32.MaxValue, UInt32.MinValue)]
        [InlineData(UInt32.MaxValue, UInt32.MaxValue)]
        public void BadExclusiveRangeUInt32(UInt32 high, UInt32 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(50, 200000000)]
        [InlineData(200000000, 200000000)]
        [InlineData(UInt32.MinValue, UInt32.MinValue)]
        [InlineData(UInt32.MinValue, UInt32.MaxValue)]
        [InlineData(UInt32.MaxValue, UInt32.MaxValue)]
        [InlineData(UInt32.MinValue, 1)]
        [InlineData(0, UInt32.MaxValue)]
        public void SampleInclusiveUInt32(UInt32 low, UInt32 high)
        {
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
        [InlineData(50, 200000000)]
        [InlineData(UInt32.MinValue, UInt32.MaxValue)]
        [InlineData(UInt32.MinValue, 1)]
        [InlineData(0, UInt32.MaxValue)]
        public void SampleExclusiveUInt32(UInt32 low, UInt32 high)
        {
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
        public void RejectionsUInt32()
        {
            const UInt32 midpoint = UInt32.MaxValue / 2 + UInt32.MinValue / 2;
            const UInt32 low = UInt32.MinValue;
            const UInt32 high = midpoint + 1;
            const UInt64 maxRand = sizeof(UInt32) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out UInt32 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(UInt32.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeUInt32()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(UInt32.MinValue, UInt32.MaxValue);
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
            Assert.Equal(unchecked((UInt32)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out UInt32 result)); // RNG wraps around to 0
            Assert.Equal((UInt32)0, result);
        }
        [Theory]
        [InlineData(200000000000, 50)]
        [InlineData(UInt64.MaxValue, UInt64.MinValue)]
        public void BadInclusiveRangeUInt64(UInt64 high, UInt64 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(200000000000, 50)]
        [InlineData(200000000000, 200000000000)]
        [InlineData(UInt64.MinValue, UInt64.MinValue)]
        [InlineData(UInt64.MaxValue, UInt64.MinValue)]
        [InlineData(UInt64.MaxValue, UInt64.MaxValue)]
        public void BadExclusiveRangeUInt64(UInt64 high, UInt64 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(50, 50)]
        [InlineData(50, 200000000000)]
        [InlineData(200000000000, 200000000000)]
        [InlineData(UInt64.MinValue, UInt64.MinValue)]
        [InlineData(UInt64.MinValue, UInt64.MaxValue)]
        [InlineData(UInt64.MaxValue, UInt64.MaxValue)]
        [InlineData(UInt64.MinValue, 1)]
        [InlineData(0, UInt64.MaxValue)]
        public void SampleInclusiveUInt64(UInt64 low, UInt64 high)
        {
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
        [InlineData(50, 200000000000)]
        [InlineData(UInt64.MinValue, UInt64.MaxValue)]
        [InlineData(UInt64.MinValue, 1)]
        [InlineData(0, UInt64.MaxValue)]
        public void SampleExclusiveUInt64(UInt64 low, UInt64 high)
        {
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
        public void RejectionsUInt64()
        {
            const UInt64 midpoint = UInt64.MaxValue / 2 + UInt64.MinValue / 2;
            const UInt64 low = UInt64.MinValue;
            const UInt64 high = midpoint + 1;
            const UInt64 maxRand = sizeof(UInt64) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out UInt64 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(UInt64.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeUInt64()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(UInt64.MinValue, UInt64.MaxValue);
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
            Assert.Equal(unchecked((UInt64)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out UInt64 result)); // RNG wraps around to 0
            Assert.Equal((UInt64)0, result);
        }
        [Theory]
        [InlineData(100, -50)]
        [InlineData(SByte.MaxValue, SByte.MinValue)]
        public void BadInclusiveRangeSByte(SByte high, SByte low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-50, -50)]
        [InlineData(100, -50)]
        [InlineData(100, 100)]
        [InlineData(SByte.MinValue, SByte.MinValue)]
        [InlineData(SByte.MaxValue, SByte.MinValue)]
        [InlineData(SByte.MaxValue, SByte.MaxValue)]
        public void BadExclusiveRangeSByte(SByte high, SByte low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-50, -50)]
        [InlineData(-50, 100)]
        [InlineData(100, 100)]
        [InlineData(SByte.MinValue, SByte.MinValue)]
        [InlineData(SByte.MinValue, SByte.MaxValue)]
        [InlineData(SByte.MaxValue, SByte.MaxValue)]
        [InlineData(SByte.MinValue, 1)]
        [InlineData(0, SByte.MaxValue)]
        public void SampleInclusiveSByte(SByte low, SByte high)
        {
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
        [InlineData(-50, 100)]
        [InlineData(SByte.MinValue, SByte.MaxValue)]
        [InlineData(SByte.MinValue, 1)]
        [InlineData(0, SByte.MaxValue)]
        public void SampleExclusiveSByte(SByte low, SByte high)
        {
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
        public void RejectionsSByte()
        {
            const SByte midpoint = SByte.MaxValue / 2 + SByte.MinValue / 2;
            const SByte low = SByte.MinValue;
            const SByte high = midpoint + 1;
            const UInt64 maxRand = sizeof(SByte) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out SByte result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(SByte.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeSByte()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(SByte.MinValue, SByte.MaxValue);
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
            Assert.Equal(unchecked((SByte)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out SByte result)); // RNG wraps around to 0
            Assert.Equal((SByte)0, result);
        }
        [Theory]
        [InlineData(1000, -1000)]
        [InlineData(Int16.MaxValue, Int16.MinValue)]
        public void BadInclusiveRangeInt16(Int16 high, Int16 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1000, -1000)]
        [InlineData(1000, -1000)]
        [InlineData(1000, 1000)]
        [InlineData(Int16.MinValue, Int16.MinValue)]
        [InlineData(Int16.MaxValue, Int16.MinValue)]
        [InlineData(Int16.MaxValue, Int16.MaxValue)]
        public void BadExclusiveRangeInt16(Int16 high, Int16 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1000, -1000)]
        [InlineData(-1000, 1000)]
        [InlineData(1000, 1000)]
        [InlineData(Int16.MinValue, Int16.MinValue)]
        [InlineData(Int16.MinValue, Int16.MaxValue)]
        [InlineData(Int16.MaxValue, Int16.MaxValue)]
        [InlineData(Int16.MinValue, 1)]
        [InlineData(0, Int16.MaxValue)]
        public void SampleInclusiveInt16(Int16 low, Int16 high)
        {
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
        [InlineData(-1000, 1000)]
        [InlineData(Int16.MinValue, Int16.MaxValue)]
        [InlineData(Int16.MinValue, 1)]
        [InlineData(0, Int16.MaxValue)]
        public void SampleExclusiveInt16(Int16 low, Int16 high)
        {
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
        public void RejectionsInt16()
        {
            const Int16 midpoint = Int16.MaxValue / 2 + Int16.MinValue / 2;
            const Int16 low = Int16.MinValue;
            const Int16 high = midpoint + 1;
            const UInt64 maxRand = sizeof(Int16) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out Int16 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(Int16.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeInt16()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(Int16.MinValue, Int16.MaxValue);
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
            Assert.Equal(unchecked((Int16)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out Int16 result)); // RNG wraps around to 0
            Assert.Equal((Int16)0, result);
        }
        [Theory]
        [InlineData(100000000, -100000000)]
        [InlineData(Int32.MaxValue, Int32.MinValue)]
        public void BadInclusiveRangeInt32(Int32 high, Int32 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100000000, -100000000)]
        [InlineData(100000000, -100000000)]
        [InlineData(100000000, 100000000)]
        [InlineData(Int32.MinValue, Int32.MinValue)]
        [InlineData(Int32.MaxValue, Int32.MinValue)]
        [InlineData(Int32.MaxValue, Int32.MaxValue)]
        public void BadExclusiveRangeInt32(Int32 high, Int32 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100000000, -100000000)]
        [InlineData(-100000000, 100000000)]
        [InlineData(100000000, 100000000)]
        [InlineData(Int32.MinValue, Int32.MinValue)]
        [InlineData(Int32.MinValue, Int32.MaxValue)]
        [InlineData(Int32.MaxValue, Int32.MaxValue)]
        [InlineData(Int32.MinValue, 1)]
        [InlineData(0, Int32.MaxValue)]
        public void SampleInclusiveInt32(Int32 low, Int32 high)
        {
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
        [InlineData(-100000000, 100000000)]
        [InlineData(Int32.MinValue, Int32.MaxValue)]
        [InlineData(Int32.MinValue, 1)]
        [InlineData(0, Int32.MaxValue)]
        public void SampleExclusiveInt32(Int32 low, Int32 high)
        {
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
        public void RejectionsInt32()
        {
            const Int32 midpoint = Int32.MaxValue / 2 + Int32.MinValue / 2;
            const Int32 low = Int32.MinValue;
            const Int32 high = midpoint + 1;
            const UInt64 maxRand = sizeof(Int32) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out Int32 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(Int32.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeInt32()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(Int32.MinValue, Int32.MaxValue);
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
            Assert.Equal(unchecked((Int32)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out Int32 result)); // RNG wraps around to 0
            Assert.Equal((Int32)0, result);
        }
        [Theory]
        [InlineData(100000000000, -100000000000)]
        [InlineData(Int64.MaxValue, Int64.MinValue)]
        public void BadInclusiveRangeInt64(Int64 high, Int64 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(high, low));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-100000000000, -100000000000)]
        [InlineData(100000000000, -100000000000)]
        [InlineData(100000000000, 100000000000)]
        [InlineData(Int64.MinValue, Int64.MinValue)]
        [InlineData(Int64.MaxValue, Int64.MinValue)]
        [InlineData(Int64.MaxValue, Int64.MaxValue)]
        public void BadExclusiveRangeInt64(Int64 high, Int64 low)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(high, low));
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
        public void SampleInclusiveInt64(Int64 low, Int64 high)
        {
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
        public void SampleExclusiveInt64(Int64 low, Int64 high)
        {
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
        public void RejectionsInt64()
        {
            const Int64 midpoint = Int64.MaxValue / 2 + Int64.MinValue / 2;
            const Int64 low = Int64.MinValue;
            const Int64 high = midpoint + 1;
            const UInt64 maxRand = sizeof(Int64) > 4 ? UInt64.MaxValue : UInt32.MaxValue;
            const UInt64 rangeSize = unchecked((UInt64)high - (UInt64)low + 1);
            const UInt64 rejectCount = (maxRand - rangeSize + 1) % rangeSize;
            const UInt64 lastAccepted = maxRand - rejectCount;

            var dist = Uniform.NewInclusive(low, high);
            var rng = new StepRng(lastAccepted - 1);

            Assert.True(dist.TrySample(rng, out Int64 result));
            Assert.Equal(midpoint, result);
            Assert.True(dist.TrySample(rng, out result));
            Assert.Equal(midpoint + 1, result);
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));
            Assert.False(dist.TrySample(rng, out _));

            // Now test a blocking sample
            rng.State = maxRand - Math.Min(20, rejectCount) + 1;
            Assert.Equal(Int64.MinValue, dist.Sample(rng));
        }

        [Fact]
        public void FullRangeInt64()
        {
            var rng = new StepRng(UInt64.MaxValue - 4);
            var dist = Uniform.NewInclusive(Int64.MinValue, Int64.MaxValue);
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
            Assert.Equal(unchecked((Int64)UInt64.MaxValue), dist.Sample(rng)); // 0
            Assert.True(dist.TrySample(rng, out Int64 result)); // RNG wraps around to 0
            Assert.Equal((Int64)0, result);
        }
    }
}