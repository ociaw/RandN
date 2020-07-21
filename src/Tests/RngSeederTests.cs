using System;
using Xunit;

namespace RandN
{
    public class RngSeederTests
    {
        [Fact]
        public void SequentialSeedStaticRng()
        {
            var seedSource = new StepRng(0);
            var rngFactory = new StepRng.Factory(increment: 0);
            using var rngSeeder = RngSeeder.Create(rngFactory, seedSource);

            for (UInt64 i = 0; i < 10; i++)
            {
                var rng = rngSeeder.Create();
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
                Assert.Equal(i, rng.NextUInt64());
            }
        }

        [Fact]
        public void NonNullable()
        {
            var seedSource = new StepRng(0);
            var rngFactory = new StepRng.Factory(increment: 0);

            Assert.Throws<ArgumentNullException>(() => RngSeeder.Create<StepRng, StepRng, UInt64>(rngFactory, null));
            Assert.Throws<ArgumentNullException>(() => RngSeeder.Create<StepRng, StepRng, UInt64>(null, seedSource));
            Assert.Throws<ArgumentNullException>(() => RngSeeder.Create<StepRng, StepRng, UInt64>(null, null));
            Assert.Throws<ArgumentNullException>(() => RngSeeder.Create<StepRng, StepRng>(null));
        }
    }
}
