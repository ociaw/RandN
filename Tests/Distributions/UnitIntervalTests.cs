using System;
using Xunit;
using RandN.Rngs;

/*** This file is auto generated - any changes made here will be lost. Modify UnitIntervalTests.tt instead. ***/
namespace RandN.Distributions
{
    public class UnitIntervalTests
    {
        [Fact]
        public void SingleRanges()
        {
            var zeroRng = new StepRng(0) { Increment = 0 };
            var maxRng = new StepRng(UInt64.MaxValue) { Increment = 0 };
            Double low, high;

            var closedOpen = UnitInterval.ClosedOpenSingle.Instance;
            low = closedOpen.Sample(zeroRng);
            high = closedOpen.Sample(maxRng);

            Assert.Equal(0, low);
            Assert.True(0 < high && high < 1);

            var openClosed = UnitInterval.OpenClosedSingle.Instance;
            low = openClosed.Sample(zeroRng);
            high = openClosed.Sample(maxRng);

            Assert.True(0 < low && low < 1);
            Assert.Equal(1, high);

            var closed = UnitInterval.ClosedSingle.Instance;
            low = closed.Sample(zeroRng);
            high = closed.Sample(maxRng);

            Assert.Equal(0, low);
            Assert.Equal(1, high);

            var open = UnitInterval.OpenSingle.Instance;
            low = open.Sample(zeroRng);
            high = open.Sample(maxRng);

            Assert.True(0 < low && low < 1);
            Assert.True(0 < high && high < 1);
        }

        [Fact]
        public void SingleAverage()
        {
            Average(UnitInterval.OpenClosedSingle.Instance, 900);
            Average(UnitInterval.ClosedOpenSingle.Instance, 901);
            Average(UnitInterval.ClosedSingle.Instance, 902);
            Average(UnitInterval.OpenSingle.Instance, 903);
        }

        private void Average(IDistribution<Single> dist, UInt64 seed)
        {
            const Int32 iterations = 10_000;
            var rng = Pcg32.Create(seed, 11634580027462260723ul);

            Double mean = 0;
            for (var i = 0; i < iterations; i++)
            {
                var result = dist.Sample(rng);
                var delta = result - mean;
                mean += delta / (i + 1);
                Assert.True(0 <= result);
                Assert.True(result <= 1);
            }

            Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean, iterations));
        }
        [Fact]
        public void DoubleRanges()
        {
            var zeroRng = new StepRng(0) { Increment = 0 };
            var maxRng = new StepRng(UInt64.MaxValue) { Increment = 0 };
            Double low, high;

            var closedOpen = UnitInterval.ClosedOpenDouble.Instance;
            low = closedOpen.Sample(zeroRng);
            high = closedOpen.Sample(maxRng);

            Assert.Equal(0, low);
            Assert.True(0 < high && high < 1);

            var openClosed = UnitInterval.OpenClosedDouble.Instance;
            low = openClosed.Sample(zeroRng);
            high = openClosed.Sample(maxRng);

            Assert.True(0 < low && low < 1);
            Assert.Equal(1, high);

            var closed = UnitInterval.ClosedDouble.Instance;
            low = closed.Sample(zeroRng);
            high = closed.Sample(maxRng);

            Assert.Equal(0, low);
            Assert.Equal(1, high);

            var open = UnitInterval.OpenDouble.Instance;
            low = open.Sample(zeroRng);
            high = open.Sample(maxRng);

            Assert.True(0 < low && low < 1);
            Assert.True(0 < high && high < 1);
        }

        [Fact]
        public void DoubleAverage()
        {
            Average(UnitInterval.OpenClosedDouble.Instance, 900);
            Average(UnitInterval.ClosedOpenDouble.Instance, 901);
            Average(UnitInterval.ClosedDouble.Instance, 902);
            Average(UnitInterval.OpenDouble.Instance, 903);
        }

        private void Average(IDistribution<Double> dist, UInt64 seed)
        {
            const Int32 iterations = 10_000;
            var rng = Pcg32.Create(seed, 11634580027462260723ul);

            Double mean = 0;
            for (var i = 0; i < iterations; i++)
            {
                var result = dist.Sample(rng);
                var delta = result - mean;
                mean += delta / (i + 1);
                Assert.True(0 <= result);
                Assert.True(result <= 1);
            }

            Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean, iterations));
        }
    }
}
