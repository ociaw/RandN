using Xunit;

namespace RandN.Rngs
{
    /// <summary>
    /// Very basic testing of <see cref="CryptoServiceProvider"/>.
    /// </summary>
    public sealed class CryptoServiceProviderTests
    {
        [Fact]
        public void Construction()
        {
            var factory = CryptoServiceProvider.GetFactory();
            var rng = factory.Create();
            Assert.True(Statistics.TestMonobitFrequency32(rng, 100_000, Statistics.WideZScore));
            Assert.True(Statistics.TestMonobitFrequency64(rng, 100_000, Statistics.WideZScore));
            Assert.True(Statistics.TestMonobitFrequencyFill(rng, 100_000, Statistics.WideZScore));
        }
    }
}
