using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions
{
    public class UniformIntTests
    {
        [Fact]
        public void BadIntRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10, 10));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Int32.MaxValue, Int32.MinValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(9001, -666));

            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10, 9));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(Int32.MaxValue, Int32.MinValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(9001, -666));
        }

        [Fact]
        public void RejectUInt()
        {
            const UInt32 halfway = UInt32.MaxValue / 2;
            var distribution = Uniform.NewInclusive(0, halfway + 1);
            var rng = new StepRng(halfway);
            Assert.True(distribution.TrySample(rng, out UInt32 result));
            Assert.Equal(halfway, result);
            Assert.True(distribution.TrySample(rng, out result));
            Assert.Equal(halfway + 1, result);
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
        }

        [Fact]
        public void RejectInts()
        {
            const Int32 halfway = -1;
            var distribution = Uniform.NewInclusive(Int32.MinValue, halfway + 1);
            var rng = new StepRng(Int32.MaxValue);
            Assert.True(distribution.TrySample(rng, out Int32 result));
            Assert.Equal(halfway, result);
            Assert.True(distribution.TrySample(rng, out result));
            Assert.Equal(halfway + 1, result);
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
            Assert.False(distribution.TrySample(rng, out _));
        }

        [Fact]
        public void RejectBytes()
        {
            const Byte low = 0;
            const Byte high = 128;
            const Byte range = high - low + 1;
            const UInt32 seed = UInt32.MaxValue - 16;
            var distribution = Uniform.NewInclusive(low, high);
            var rng = new StepRng(seed);
            Assert.True(distribution.TrySample(rng, out Byte result));
            Assert.Equal(seed % range, result);
            for (Int32 i = 0; i < 16; i++)
            {
                Assert.False(distribution.TrySample(rng, out _));
            }
            Assert.True(distribution.TrySample(rng, out result));
            Assert.Equal(0, result);
        }

        [Fact]
        public void SampleUInt64()
        {
            const UInt64 low = 0;
            const UInt64 high = 1000;
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void SampleSByte()
        {
            const SByte low = -128;
            const SByte high = 127;
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void SampleInt16()
        {
            const Int16 low = -500;
            const Int16 high = 500;
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void SampleInt32()
        {
            const Int32 low = -500;
            const Int32 high = 500;
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void SampleInt64()
        {
            const Int64 low = -500;
            const Int64 high = 500;
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void SampleTimeSpan()
        {
            TimeSpan low = TimeSpan.FromTicks(-500);
            TimeSpan high = TimeSpan.FromTicks(500);
            var exclusiveDist = Uniform.New(low, high);
            var inclusiveDist = Uniform.New(low, high);
            var rng = Mt1993764.Create(5489);

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(exclusiveDist);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            for (var i = 0; i < 10000; i++)
            {
                var result = rng.Sample(inclusiveDist);
                Assert.True(low <= result);
                Assert.True(result <= high);
            }
        }

        [Fact]
        public void NonNullable()
        {
            var dist = Uniform.New(1, 2);
            Assert.Throws<ArgumentNullException>(() => dist.Sample<StepRng>(null));

            var dist2 = Uniform.TimeSpan.Create(TimeSpan.FromTicks(1), TimeSpan.FromTicks(2));
            Assert.Throws<ArgumentNullException>(() => dist2.Sample<StepRng>(null));
        }
    }
}
