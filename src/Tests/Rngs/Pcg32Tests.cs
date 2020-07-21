using Xunit;

namespace RandN.Rngs
{
    public class Pcg32Tests
    {
        [Fact]
        public void Construction()
        {
            var seedState = 578437695752307201ul;
            // This seed is from rust_rand, so we right shift them for compatibility.
            var stream = 1157159078456920585ul >> 1;
            var seed1 = new Pcg32.Seed(seedState, stream);
            var rng1 = Pcg32.GetFactory().Create(seed1);
            Assert.Equal(1204678643940597513ul, rng1.NextUInt64());
            var seed2 = Pcg32.GetFactory().CreateSeed(rng1);
            seed2 = new Pcg32.Seed(seed2.State, seed2.Stream >> 1);
            var rng2 = Pcg32.GetFactory().Create(seed2);
            Assert.Equal(12384929573776311845ul, rng2.NextUInt64());
        }

        [Fact]
        public void Reference()
        {
            var rng = Pcg32.Create(42, 54);
            Assert.Equal(0xa15c02b7u, rng.NextUInt32());
            Assert.Equal(0x7b47f409u, rng.NextUInt32());
            Assert.Equal(0xba1d3330u, rng.NextUInt32());
            Assert.Equal(0x83d2f293u, rng.NextUInt32());
            Assert.Equal(0xbfa4784bu, rng.NextUInt32());
            Assert.Equal(0xcbed606eu, rng.NextUInt32());
        }

        [Fact]
        public void NonNullable()
        {
            Assert.Throws<System.ArgumentNullException>(() => Pcg32.GetFactory().CreateSeed<StepRng>(null));
        }
    }
}
