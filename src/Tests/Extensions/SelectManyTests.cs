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
        public void SelectMany_returns_a_distribution_which_applies_the_selector_to_a_sample_from_the_input_and_samples_from_it(
            String inputSample, Int32 expectedOutputSample)
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
        [InlineData("", 0)]
        [InlineData("ABCDEF", 6)]
        public void If_both_input_distributions_succeed_then_the_result_of_applying_SelectMany_succeeds(
            String inputResult,
            Int32 expectedOutputResult)
        {
            TestTrySample(
                inputResult,
                inputSuccess: true,
                intermediateSuccess: true,
                expectedOutputResult,
                expectedOutputSuccess: true);
        }

        [Theory]
        [InlineData(null, false, true, 0)] // If sampling the first distribution fails, the selector should not be applied.
        [InlineData("ABC", true, false, 0)] // If sampling the second distribution fails, the result should be ignored.
        public void If_one_of_input_distributions_fail_then_the_result_of_applying_SelectMany_fails(
            String inputResult,
            Boolean inputSuccess,
            Boolean intermediateSuccess,
            Int32 expectedOutputResult)
        {
            TestTrySample(inputResult, inputSuccess, intermediateSuccess, expectedOutputResult, expectedOutputSuccess: false);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData("ABC", 0)] // If sampling the first distribution fails, the result should be ignored.
        public void If_both_input_distributions_fail_then_the_result_of_applying_SelectMany_fails(
            String inputResult,
            Int32 expectedOutputResult)
        {
            TestTrySample(
                inputResult,
                inputSuccess: false,
                intermediateSuccess: false,
                expectedOutputResult,
                expectedOutputSuccess: false);
        }

        private static void TestTrySample(
            String inputResult,
            Boolean inputSuccess,
            Boolean intermediateSuccess,
            Int32 expectedOutputResult,
            Boolean expectedOutputSuccess)
        {
            var inputDistribution = new MockDistribution<String>(inputResult, inputSuccess);

            var outputDistribution =
                inputDistribution.SelectMany(x => new MockDistribution<Int32>(x.Length, intermediateSuccess));

            // Use a throwing RNG to check that sampling from the distribution
            // does not update the state of the RNG directly.
            var rng = new ThrowingRng();

            Assert.Equal(expectedOutputSuccess, outputDistribution.TrySample(rng, out var result));
            Assert.Equal(expectedOutputResult, result);
        }
    }
}
