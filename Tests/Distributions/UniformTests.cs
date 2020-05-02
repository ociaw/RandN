using System;
using Xunit;
using Rand.Tests;

namespace Rand.Distributions.Tests
{
    public class UniformTests
    {
        [Fact]
        public void BadRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.New(10, 10));
            Assert.Throws<ArgumentOutOfRangeException>("high", () => Uniform.NewInclusive(10, 9));
        }

        [Fact]
        public void Test1()
        {
            var distribution = Uniform.New(0ul, 10ul);
            var rng = new SequentialRng(0);

            for (UInt64 i = 0; i < 2000; i++)
            {
                Assert.True(distribution.TrySample(rng, out var result));
                Assert.Equal(i % 10, result);
            }
        }
    }
}
