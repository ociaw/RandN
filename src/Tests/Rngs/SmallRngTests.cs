using Xunit;

namespace RandN.Rngs
{
    /// <summary>
    /// Very basic testing of <see cref="SmallRng"/>.
    /// </summary>
    public class SmallRngTests
    {
        [Fact]
        public void Construction()
        {
            var factory = SmallRng.GetFactory();
            var rng = factory.Create();
            Assert.True(Statistics.TestMonobitFrequency(rng, 100_000));
        }
    }
}
