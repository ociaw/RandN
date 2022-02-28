using System;
using Xunit;

namespace RandN.Extensions
{
    public class SelectManyTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("A", 1)]
        [InlineData("ABCDEF", 6)]
        public void SelectMany_Sample(String inputSample, Int32 expectedOutputSample)
        {
            var inputDistribution = new MockDistribution<String>(inputSample);

            var outputDistribution = inputDistribution.SelectMany(x => new MockDistribution<Int32>(x.Length));

            // Use a throwing RNG to check that sampling from the distribution
            // does not update the state of the RNG directly.
            var rng = new ThrowingRng();

            var sampleFromOutputDistribution = outputDistribution.Sample(rng);

            Assert.Equal(expectedOutputSample, sampleFromOutputDistribution);
        }

        [Theory]
        [InlineData(null, false, false, 0, false)]
        [InlineData("ABC", false, false, 0, false)] // If sampling the first distribution fails, the result should be ignored.
        [InlineData(null, false, true, 0, false)] // If sampling the first distribution fails, the selector should not be applied.
        [InlineData("ABC", true, false, 0, false)] // If sampling the second distribution fails, the result should be ignored.
        [InlineData("", true, true, 0, true)]
        [InlineData("ABCDEF", true, true, 6, true)]
        public void SelectMany_TrySample(
            String inputResult,
            Boolean inputSuccess,
            Boolean intermediateSuccess,
            Int32 expectedOutputResult,
            Boolean expectedOutputSuccess)
        {
            var inputDistribution = new MockDistribution<String>(inputResult, inputSuccess);

            var outputDistribution = inputDistribution.SelectMany(x => new MockDistribution<Int32>(x.Length, intermediateSuccess));

            // Use a throwing RNG to check that sampling from the distribution
            // does not update the state of the RNG directly.
            var rng = new ThrowingRng();

            Assert.Equal(expectedOutputSuccess, outputDistribution.TrySample(rng, out var result));
            Assert.Equal(expectedOutputResult, result);
        }
    }
}
