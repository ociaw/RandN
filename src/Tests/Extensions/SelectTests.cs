using System;
using Xunit;

namespace RandN.Extensions;

public class SelectTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("A", 1)]
    [InlineData("ABCDEF", 6)]
    public void Select_returns_a_distribution_which_applies_the_selector_to_a_sample_from_the_input(
        String inputSample, Int32 expectedOutputSample)
    {
        var inputDistribution = new MockDistribution<String>(inputSample);

        var outputDistribution = inputDistribution.Select(x => x.Length);

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        var sampleFromOutputDistribution = outputDistribution.Sample(rng);

        Assert.Equal(expectedOutputSample, sampleFromOutputDistribution);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("A", 1)]
    [InlineData("ABCDEF", 6)]
    public void If_the_input_distribution_succeeds_then_the_result_of_applying_Select_also_succeeds(
        String inputResult,
        Int32 expectedOutputResult)
    {
        TestTrySample(inputResult, inputSuccess: true, expectedOutputResult, expectedOutputSuccess: true);
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("ABC", 0)] // If sampling fails, the result from the input distribution should be ignored.
    public void If_the_input_distribution_fails_then_the_result_of_applying_Select_also_fails(
        String inputResult,
        Int32 expectedOutputResult)
    {
        TestTrySample(inputResult, inputSuccess: false, expectedOutputResult, expectedOutputSuccess: false);
    }

    [Fact]
    public void Select_can_be_used_with_LINQ_query_syntax()
    {
        var inputDistribution = new MockDistribution<String>("ABCDEF");

        var outputDistribution =
            from x in inputDistribution
            select x.Length;

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        Assert.Equal(6, outputDistribution.Sample(rng));
    }

    private static void TestTrySample(
        String inputResult,
        Boolean inputSuccess,
        Int32 expectedOutputResult,
        Boolean expectedOutputSuccess)
    {
        var inputDistribution = new MockDistribution<String>(inputResult, inputSuccess);

        var outputDistribution = inputDistribution.Select(x => x.Length);

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        Assert.Equal(expectedOutputSuccess, outputDistribution.TrySample(rng, out var result));
        Assert.Equal(expectedOutputResult, result);
    }
}