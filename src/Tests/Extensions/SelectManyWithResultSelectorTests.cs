using System;
using Xunit;

namespace RandN.Extensions;

public class SelectManyWithResultSelectorTests
{
    [Theory]
    [InlineData("", 0)]
    [InlineData("A", 1)]
    [InlineData("ABCDEF", 6)]
    public void SelectMany_returns_a_distribution_which_applies_the_selector_to_a_sample_from_the_input_and_samples_from_it(
        String inputSample, Int32 expectedOutputSample)
    {
        var inputDistribution = new MockDistribution<String>(inputSample);

        var outputDistribution = inputDistribution.SelectMany(
            x => new MockDistribution<Int32>(x.Length),
            (x, y) => new { X = x, Y = y });

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        var sampleFromOutputDistribution = outputDistribution.Sample(rng);

        Assert.Equal(inputSample, sampleFromOutputDistribution.X);
        Assert.Equal(expectedOutputSample, sampleFromOutputDistribution.Y);
    }

    [Theory]
    [InlineData(null, false, false, 0, false)]
    [InlineData("ABC", false, false, 0, false)] // If sampling the first distribution fails, the result should be ignored.
    [InlineData(null, false, true, 0, false)] // If sampling the first distribution fails, the selector should not be applied.
    [InlineData("ABC", true, false, 0, false)] // If sampling the second distribution fails, the result should be ignored.
    [InlineData("", true, true, 0, true)]
    [InlineData("ABCDEF", true, true, 6, true)]
    public void SelectManyWithResultSelector_TrySample(
        String inputResult,
        Boolean inputSuccess,
        Boolean intermediateSuccess,
        Int32 expectedOutputResult,
        Boolean expectedOutputSuccess)
    {
        var inputDistribution = new MockDistribution<String>(inputResult, inputSuccess);

        var outputDistribution = inputDistribution.SelectMany(
            x => new MockDistribution<Int32>(x.Length, intermediateSuccess),
            (x, y) => new { X = x, Y = y });

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        Assert.Equal(expectedOutputSuccess, outputDistribution.TrySample(rng, out var result));

        if (expectedOutputSuccess)
        {
            Assert.NotNull(result);
            Assert.Equal(inputResult, result.X);
            Assert.Equal(expectedOutputResult, result.Y);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("A", 1)]
    [InlineData("ABCDEF", 6)]
    public void SelectMany_can_be_used_with_LINQ_query_syntax(String inputSample, Int32 expectedOutputSample)
    {
        var inputDistribution = new MockDistribution<String>(inputSample);

        var outputDistribution =
            from x in inputDistribution
            from y in new MockDistribution<Int32>(x.Length)
            select new { X = x, Y = y };

        // Use a throwing RNG to check that sampling from the distribution
        // does not update the state of the RNG directly.
        var rng = new ThrowingRng();

        var sampleFromOutputDistribution = outputDistribution.Sample(rng);

        Assert.Equal(inputSample, sampleFromOutputDistribution.X);
        Assert.Equal(expectedOutputSample, sampleFromOutputDistribution.Y);
    }
}