using Xunit;

namespace RandN.Rngs;

/// <summary>
/// Very basic testing of <see cref="SystemCryptoRng"/>.
/// </summary>
public sealed class SystemCryptoRngTests
{
    [Fact]
    public void Construction()
    {
        var factory = SystemCryptoRng.GetFactory();
        using var rng = factory.Create();
        Assert.True(Statistics.TestMonobitFrequency32(rng, 100_000, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequency64(rng, 100_000, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequencyFill(rng, 32, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequencyFill(rng, 64, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequencyFill(rng, 128, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequencyFill(rng, 256, Statistics.WideZScore));
        Assert.True(Statistics.TestMonobitFrequencyFill(rng, 100_000, Statistics.WideZScore));
    }
}
