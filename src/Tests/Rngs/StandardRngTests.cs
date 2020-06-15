using Xunit;

namespace RandN.Rngs
{
    /// <summary>
    /// Very basic testing of <see cref="StandardRng"/>.
    /// </summary>
    public class StandardRngTests
    {
        [Fact]
        public void Construction()
        {
            var factory = StandardRng.GetFactory();
            var rng = factory.Create();
            Assert.True(Statistics.TestMonobitFrequency(rng, 100_000, Statistics.WideZScore));
        }
    }
}
