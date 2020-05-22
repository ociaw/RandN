using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions.Tests
{
    public class UniformFloatTests
    {
        [Fact]
        public void BadRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10.0, 10.0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Double.MaxValue, Double.MinValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(9001.0, -666.0));
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Double.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Double.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Double.NaN));

            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10.0, 9.0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(Double.MaxValue, Double.MinValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(9001.0, -666.0));
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Double.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Double.PositiveInfinity, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Double.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Double.NaN));
        }

        [Fact]
        public void SampleDoubles()
        {
            const Double low = 0.0;
            const Double high = 1000.0;
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
        public void AverageDoubles()
        {
            const Int32 iterations = 100_000;
            Average(0.0, 1000.0, iterations, 0);
            Average(0.0, 1.0, iterations, 1);
            Average(-50.0, 50.0, iterations, 2);
            Average(0, Double.MaxValue, iterations, 3);
            Average(Double.MinValue, 0, iterations, 4);
            Average(38.9, 64.6, iterations, 5);
        }

        private void Average(Double low, Double high, Int32 iterations, UInt64 stream)
        {
            var populationMean = high / 2 + low / 2;
            var popStdDev = 1.0 / Math.Sqrt(3) * (high / 2 - low / 2);

            var exclusiveDist = Uniform.New(low, high);
            var rng = ChaCha.Create(new ChaCha.Seed(stackalloc UInt32[8], stream));

            Double mean = 0;
            for (var i = 0; i < iterations; i++)
            {
                var result = rng.Sample(exclusiveDist);
                var delta = result - mean;
                mean += delta / (i + 1);
                Assert.True(low <= result);
                Assert.True(result < high);
            }

            // 99% confidence interval
            const Double z = 2.576;
            var margin = popStdDev / Math.Sqrt(iterations) * z;
            var sampleMeanDiff = Math.Abs(populationMean - (Double)mean);

            Assert.True(sampleMeanDiff < margin);
        }
    }
}
