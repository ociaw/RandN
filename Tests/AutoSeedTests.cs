using System;
using Xunit;

namespace RandN.Tests
{
    public class AutoSeedTests
    {
        [Fact]
        public void SequentialSeedStaticRng()
        {
            var seedSource = new SequentialRng(0);
            var rngFactory = new StaticRng.Factory();
            var autoSeeder = AutoSeedingRngFactory.Create(rngFactory, seedSource);

            for (UInt64 i = 0; i < 10; i++)
            {
                var rng = autoSeeder.Create();
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
            }
        }
    }
}
