using System;
using Xunit;
using RandN.Rngs;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    public class UniformFloatTests
    {
        [Fact]
        public void BadRange()
        {
            // Equal
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0f, 0f));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Single.MaxValue, Single.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Single.MinValue, Single.MinValue));
            // Reversed
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10.0f, 10.0f));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(9001.0f, -666.0f));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Single.MaxValue, Single.MinValue));
            // Infinities
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Single.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.PositiveInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(0, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.PositiveInfinity, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NegativeInfinity, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NegativeInfinity, Single.PositiveInfinity));
            // NaNs
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Single.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Single.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NaN, Single.NaN));
            // Mixed NaN / Infinity
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NaN, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.PositiveInfinity, Single.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NaN, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Single.NegativeInfinity, Single.NaN));

            // Reversed
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10.0f, 9.0f));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(9001.0f, -666.0f));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(Single.MaxValue, Single.MinValue));
            // Infinities
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Single.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.PositiveInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(0, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.PositiveInfinity, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NegativeInfinity, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NegativeInfinity, Single.PositiveInfinity));
            // NaNs
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Single.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Single.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NaN, Single.NaN));
            // Mixed NaN / Infinity
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NaN, Single.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.PositiveInfinity, Single.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NaN, Single.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Single.NegativeInfinity, Single.NaN));

            // Equal
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0d, 0d));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Double.MaxValue, Double.MaxValue));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Double.MinValue, Double.MinValue));
            // Reversed
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10.0d, 10.0d));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(9001.0d, -666.0d));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(Double.MaxValue, Double.MinValue));
            // Infinities
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Double.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.PositiveInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(0, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.PositiveInfinity, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NegativeInfinity, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NegativeInfinity, Double.PositiveInfinity));
            // NaNs
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.New(Double.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(0, Double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NaN, Double.NaN));
            // Mixed NaN / Infinity
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NaN, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.PositiveInfinity, Double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NaN, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.New(Double.NegativeInfinity, Double.NaN));

            // Reversed
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10.0d, 9.0d));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(9001.0d, -666.0d));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(Double.MaxValue, Double.MinValue));
            // Infinities
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Double.NegativeInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.PositiveInfinity, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(0, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.PositiveInfinity, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NegativeInfinity, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NegativeInfinity, Double.PositiveInfinity));
            // NaNs
            Assert.Throws<ArgumentOutOfRangeException>("low", () => Uniform.NewInclusive(Double.NaN, 0));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(0, Double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NaN, Double.NaN));
            // Mixed NaN / Infinity
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NaN, Double.PositiveInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.PositiveInfinity, Double.NaN));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NaN, Double.NegativeInfinity));
            Assert.Throws<ArgumentOutOfRangeException>(() => Uniform.NewInclusive(Double.NegativeInfinity, Double.NaN));

        }

        [Fact]
        public void FloatTest()
        {
            var rng = Pcg32.Create(252, 11634580027462260723ul);
            var zeroRng = new StepRng(0) { Increment = 0 };
            var maxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = 0 };

            var vectorsSingle = new (Single, Single)[]
            {
                (0.0f, 100.0f),
                (-1e35f, -1e25f),
                (1e-35f, 1e-25f),
                (-1e35f, 1e35f),
                (0u.ToFloat(), 3u.ToFloat()),
                (-10u.ToFloat(), -1u.ToFloat()),
                (-5u.ToFloat(), 0.0f),
                (-7u.ToFloat(), -0.0f),
                (10.0f, Single.MaxValue),
                (-100.0f, Single.MaxValue),
                (-Single.MaxValue / 5.0f, Single.MaxValue),
                (-Single.MaxValue, Single.MaxValue / 5.0f),
                (-Single.MaxValue * 0.8f, Single.MaxValue * 0.7f),
                (-Single.MaxValue, Single.MaxValue),
            };

            foreach ((var low, var high) in vectorsSingle)
            {
                var uniform = Uniform.New(low, high);
                var inclusiveUniform = Uniform.NewInclusive(low, high);
                for (var i = 0; i < 100; i++)
                {
                    var exclusive = uniform.Sample(rng);
                    Assert.True(low <= exclusive && exclusive < high);
                    var inclusive = uniform.Sample(rng);
                    Assert.True(low <= inclusive && inclusive <= high);
                }

                Assert.Equal(low, Uniform.NewInclusive(low, low).Sample(rng));

                Assert.Equal(low, uniform.Sample(zeroRng));
                Assert.Equal(low, inclusiveUniform.Sample(zeroRng));
                Assert.True(uniform.Sample(maxRng) < high);
                Assert.True(inclusiveUniform.Sample(maxRng) <= high);

                // Don't run this test for really tiny differences between high and low
                // since for those rounding might result in selecting high for a very
                // long time.
                if (high - low > 0.0001)
                {
                    var loweringMaxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = unchecked((UInt64)(-1L << 9)) };
                    Assert.True(uniform.Sample(loweringMaxRng) < high);
                    Assert.True(uniform.Sample(loweringMaxRng) < high);
                }
            }

            var maxSingleInclusive = Uniform.NewInclusive(Single.MaxValue, Single.MaxValue);
            Assert.Equal(Single.MaxValue, maxSingleInclusive.Sample(rng));
            var minSingleInclusive = Uniform.NewInclusive(-Single.MaxValue, -Single.MaxValue);
            Assert.Equal(-Single.MaxValue, minSingleInclusive.Sample(rng));
            var vectorsDouble = new (Double, Double)[]
            {
                (0.0d, 100.0d),
                (-1e35d, -1e25d),
                (1e-35d, 1e-25d),
                (-1e35d, 1e35d),
                (0ul.ToFloat(), 3ul.ToFloat()),
                (-10ul.ToFloat(), -1ul.ToFloat()),
                (-5ul.ToFloat(), 0.0d),
                (-7ul.ToFloat(), -0.0d),
                (10.0d, Double.MaxValue),
                (-100.0d, Double.MaxValue),
                (-Double.MaxValue / 5.0d, Double.MaxValue),
                (-Double.MaxValue, Double.MaxValue / 5.0d),
                (-Double.MaxValue * 0.8d, Double.MaxValue * 0.7d),
                (-Double.MaxValue, Double.MaxValue),
            };

            foreach ((var low, var high) in vectorsDouble)
            {
                var uniform = Uniform.New(low, high);
                var inclusiveUniform = Uniform.NewInclusive(low, high);
                for (var i = 0; i < 100; i++)
                {
                    var exclusive = uniform.Sample(rng);
                    Assert.True(low <= exclusive && exclusive < high);
                    var inclusive = uniform.Sample(rng);
                    Assert.True(low <= inclusive && inclusive <= high);
                }

                Assert.Equal(low, Uniform.NewInclusive(low, low).Sample(rng));

                Assert.Equal(low, uniform.Sample(zeroRng));
                Assert.Equal(low, inclusiveUniform.Sample(zeroRng));
                Assert.True(uniform.Sample(maxRng) < high);
                Assert.True(inclusiveUniform.Sample(maxRng) <= high);

                // Don't run this test for really tiny differences between high and low
                // since for those rounding might result in selecting high for a very
                // long time.
                if (high - low > 0.0001)
                {
                    var loweringMaxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = unchecked((UInt64)(-1L << 12)) };
                    Assert.True(uniform.Sample(loweringMaxRng) < high);
                    Assert.True(uniform.Sample(loweringMaxRng) < high);
                }
            }

            var maxDoubleInclusive = Uniform.NewInclusive(Double.MaxValue, Double.MaxValue);
            Assert.Equal(Double.MaxValue, maxDoubleInclusive.Sample(rng));
            var minDoubleInclusive = Uniform.NewInclusive(-Double.MaxValue, -Double.MaxValue);
            Assert.Equal(-Double.MaxValue, minDoubleInclusive.Sample(rng));
        }

        [Fact]
        public void AverageSingles()
        {
            const Int32 iterations = 10_000;

            static void Average(Single low, Single high, UInt64 seed)
            {
                var populationMean = high / 2 + low / 2;
                var popStdDev = 1.0 / Math.Sqrt(3) * (high / 2 - low / 2);

                var exclusiveDist = Uniform.New(low, high);
                var inclusiveDist = Uniform.NewInclusive(low, high);
                var rng = Pcg32.Create(789 + seed, 11634580027462260723ul);

                Double exclusiveMean = 0;
                Double inclusiveMean = 0;
                for (var i = 0; i < iterations; i++)
                {
                    var exclusive = rng.Sample(exclusiveDist);
                    var exclusiveDelta = exclusive - exclusiveMean;
                    exclusiveMean += exclusiveDelta / (i + 1);
                    Assert.True(low <= exclusive);
                    Assert.True(exclusive < high);

                    var inclusive = rng.Sample(inclusiveDist);
                    var inclusiveDelta = inclusive - inclusiveMean;
                    inclusiveMean += inclusiveDelta / (i + 1);
                    Assert.True(low <= inclusive);
                    Assert.True(inclusive < high);
                }

                // 99% confidence interval
                const Double z = 2.576;
                var margin = popStdDev / Math.Sqrt(iterations) * z;

                var exclusiveSampleMeanDiff = Math.Abs(populationMean - exclusiveMean);
                Assert.True(exclusiveSampleMeanDiff < margin);
                var inclusiveSampleMeanDiff = Math.Abs(populationMean - inclusiveMean);
                Assert.True(inclusiveSampleMeanDiff < margin);
            }

            Average(0f, 1000f, 0);
            Average(0f, 1f, 1);
            Average(-50.0f, 50.0f, 2);
            Average(0f, Single.MaxValue, 3);
            Average(Single.MinValue, 0f, 4);
            Average(38.9f, 64.6f, 5);
            Average(1e-35f, 1e-30f, 6);
        }

        [Fact]
        public void AverageDoubles()
        {
            const Int32 iterations = 10_000;

            static void Average(Double low, Double high, UInt64 seed)
            {
                var populationMean = high / 2 + low / 2;
                var popStdDev = 1.0 / Math.Sqrt(3) * (high / 2 - low / 2);

                var exclusiveDist = Uniform.New(low, high);
                var inclusiveDist = Uniform.NewInclusive(low, high);
                var rng = Pcg32.Create(789 + seed, 11634580027462260723ul);

                Double exclusiveMean = 0;
                Double inclusiveMean = 0;
                for (var i = 0; i < iterations; i++)
                {
                    var exclusive = rng.Sample(exclusiveDist);
                    var exclusiveDelta = exclusive - exclusiveMean;
                    exclusiveMean += exclusiveDelta / (i + 1);
                    Assert.True(low <= exclusive);
                    Assert.True(exclusive < high);

                    var inclusive = rng.Sample(inclusiveDist);
                    var inclusiveDelta = inclusive - inclusiveMean;
                    inclusiveMean += inclusiveDelta / (i + 1);
                    Assert.True(low <= inclusive);
                    Assert.True(inclusive < high);
                }

                // 99% confidence interval
                const Double z = 2.576;
                var margin = popStdDev / Math.Sqrt(iterations) * z;

                var exclusiveSampleMeanDiff = Math.Abs(populationMean - exclusiveMean);
                Assert.True(exclusiveSampleMeanDiff < margin);
                var inclusiveSampleMeanDiff = Math.Abs(populationMean - inclusiveMean);
                Assert.True(inclusiveSampleMeanDiff < margin);
            }

            Average(0d, 1000d, 0);
            Average(0d, 1d, 1);
            Average(-50.0d, 50.0d, 2);
            Average(0d, Double.MaxValue, 3);
            Average(Double.MinValue, 0d, 4);
            Average(38.9d, 64.6d, 5);
            Average(1e-35d, 1e-30d, 6);
        }

    }
}
