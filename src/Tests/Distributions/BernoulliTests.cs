using System;
using Xunit;
using RandN.Rngs;

namespace RandN.Distributions
{
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
            const UInt32 NUMERATOR = 3;
            const UInt32 DENOMINATOR = 10;
            const Double P = (Double)NUMERATOR / DENOMINATOR;
            const UInt64 SAMPLE_COUNT = 100_000;

            var rng1 = Mt1993764.Create(5489);
            var rng2 = Mt1993764.Create(5489);
            var ratioDist = Bernoulli.FromRatio(NUMERATOR, DENOMINATOR);
            var pDist = Bernoulli.FromP(P);

            UInt64 sum = 0;
            for (UInt64 i = 0; i < SAMPLE_COUNT; i++)
            {
                Assert.True(ratioDist.TrySample(rng1, out Boolean result1));
                Assert.True(pDist.TrySample(rng2, out Boolean result2));
                Assert.Equal(result1, result2);

                if (result1)
                    sum++;
            }

            Assert.True(Statistics.WithinConfidenceBernoulli(sum, P, SAMPLE_COUNT));
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
            // BUt we'll force it to anyway.
            var rng = new StepRng(UInt64.MaxValue);
            Assert.False(dist.Sample(rng));
        }
    }
}
