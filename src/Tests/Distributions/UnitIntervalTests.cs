




using System;
using System.Collections.Generic;
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

        public static IEnumerable<Object[]> SingleParams(Int32 seedStart)
        {
            yield return new Object [] { UnitInterval.OpenClosedSingle.Instance, seedStart };
            yield return new Object[] { UnitInterval.ClosedOpenSingle.Instance, seedStart + 1 };
            yield return new Object[] { UnitInterval.ClosedSingle.Instance, seedStart + 2};
            yield return new Object[] { UnitInterval.OpenSingle.Instance, seedStart + 3};
        }

        [Theory]
        [MemberData(nameof(SingleParams), 900)]
        public void SingleAverage(IDistribution<Single> dist, UInt64 seed)
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

            Double mean2 = 0;
            for (var i = 0; i < iterations; i++)
            {
                Assert.True(dist.TrySample(rng, out var result));
                var delta = result - mean2;
                mean2 += delta / (i + 1);
                Assert.True(0 <= result);
                Assert.True(result <= 1);
            }

            Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean2, iterations));
        }

        [Fact]
        public void SingleNonNullable()
        {
            Assert.Throws<ArgumentNullException>(() => UnitInterval.ClosedOpenSingle.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.OpenClosedSingle.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.ClosedSingle.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.OpenSingle.Instance.Sample<StepRng>(null));
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

        public static IEnumerable<Object[]> DoubleParams(Int32 seedStart)
        {
            yield return new Object [] { UnitInterval.OpenClosedDouble.Instance, seedStart };
            yield return new Object[] { UnitInterval.ClosedOpenDouble.Instance, seedStart + 1 };
            yield return new Object[] { UnitInterval.ClosedDouble.Instance, seedStart + 2};
            yield return new Object[] { UnitInterval.OpenDouble.Instance, seedStart + 3};
        }

        [Theory]
        [MemberData(nameof(DoubleParams), 900)]
        public void DoubleAverage(IDistribution<Double> dist, UInt64 seed)
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

            Double mean2 = 0;
            for (var i = 0; i < iterations; i++)
            {
                Assert.True(dist.TrySample(rng, out var result));
                var delta = result - mean2;
                mean2 += delta / (i + 1);
                Assert.True(0 <= result);
                Assert.True(result <= 1);
            }

            Assert.True(Statistics.WithinConfidence(popMean: 0.5, popStdDev: 0.5, mean2, iterations));
        }

        [Fact]
        public void DoubleNonNullable()
        {
            Assert.Throws<ArgumentNullException>(() => UnitInterval.ClosedOpenDouble.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.OpenClosedDouble.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.ClosedDouble.Instance.Sample<StepRng>(null));
            Assert.Throws<ArgumentNullException>(() => UnitInterval.OpenDouble.Instance.Sample<StepRng>(null));
        }

    }
}
