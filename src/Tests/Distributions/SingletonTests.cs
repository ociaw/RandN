using Xunit;

namespace RandN.Distributions;

public class SingletonTests
{
    [Fact]
    public void Sample()
    {
        var value = "abc123";

        var singleton = Singleton.New(value);

        // Use a ThrowingRng to check that the state of the RNG is left untouched when sampling.
        var rng = new ThrowingRng();

        var sample = singleton.Sample(rng);

        Assert.Equal(value, sample);
    }

    [Fact]
    public void TrySample()
    {
        var value = 123.456;

        var singleton = Singleton.New(value);

        // Use a ThrowingRng to check that the state of the RNG is left untouched when sampling.
        var rng = new ThrowingRng();

        var success = singleton.TrySample(rng, out var sample);

        Assert.True(success);
        Assert.Equal(value, sample);
    }
}