using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions
{
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

        [Fact]
        public void FloatTest()
        {
            var rng = Pcg32.Create(252, 11634580027462260723ul);
            var zeroRng = new StepRng(0) { Increment = 0 };
            var maxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = 0 };

            var vectors = new (Decimal, Decimal)[]
            {
                (0, 1),
                (-1, 0),
                (0.0m, 100.0m),
                (-1e28m, -1e25m),
                (1e-28m, 1e-25m),
                (0ul, 3ul),
                (-10, -1),
                (-5, 0.0m),
                (-7, -0.0m),
                // The next two need to decrement the scale when building the distribution.
                (5, Decimal.MaxValue - 5), // This simply produces an initial maximum larger than high.
                (10.0m, Decimal.MaxValue), // This does the same, but causes an overflow in the process.
                (-100.0m, Decimal.MaxValue),
                (-Decimal.MaxValue / 5.0m, Decimal.MaxValue),
                (-Decimal.MaxValue, Decimal.MaxValue / 5.0m),
                (-Decimal.MaxValue * 0.8m, Decimal.MaxValue * 0.7m),
                (-Decimal.MaxValue, Decimal.MaxValue),
                (0m, new Decimal(-1, -1, -1, false, 28)),
            };

            foreach ((var low, var high) in vectors)
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

                var loweringMaxRng = new StepRng(0xFFFF_FFFF_FFFF_FFFF) { Increment = unchecked((UInt64)(-1L << 12)) };
                Assert.True(uniform.Sample(loweringMaxRng) < high);
                Assert.True(uniform.Sample(loweringMaxRng) < high);
            }

            var maxDoubleInclusive = Uniform.NewInclusive(Decimal.MaxValue, Decimal.MaxValue);
            Assert.Equal(Decimal.MaxValue, maxDoubleInclusive.Sample(rng));
            var minDoubleInclusive = Uniform.NewInclusive(-Decimal.MaxValue, -Decimal.MaxValue);
            Assert.Equal(-Decimal.MaxValue, minDoubleInclusive.Sample(rng));
        }

        [Fact]
        public void AverageDecimals()
        {
            const Int32 iterations = 10_000;

            static void Average(Decimal low, Decimal high, UInt64 seed)
            {
                const Decimal Sqr3 = 1.7320508075688772935274463415m;
                var populationMean = high / 2 + low / 2;
                var popStdDev = 1.0m / Sqr3 * (high / 2 - low / 2);

                var exclusiveDist = Uniform.New(low, high);
                var inclusiveDist = Uniform.NewInclusive(low, high);
                var rng = Pcg32.Create(789 + seed, 11634580027462260723ul);

                Decimal exclusiveMean = 0;
                Decimal inclusiveMean = 0;
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

                Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, exclusiveMean, iterations));
                Assert.True(Statistics.WithinConfidence(populationMean, popStdDev, inclusiveMean, iterations));
            }

            Average(0m, 1000m, 0);
            Average(0m, 1m, 1);
            Average(-50.0m, 50.0m, 2);
            Average(0m, Decimal.MaxValue, 3);
            Average(Decimal.MinValue, 0m, 4);
            Average(38.9m, 64.6m, 5);
            Average(1e-28m, 1e-24m, 6);
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
}
