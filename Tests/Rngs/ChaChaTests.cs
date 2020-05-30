using System;
using Xunit;
using Seed = RandN.Rngs.ChaCha.Seed;

namespace RandN.Rngs.Tests
{
    public sealed class ChaChaTests
    {
        /// <summary>
        /// Verifies factory creation methods with test vectors from Rust's rand_chacha crate.
        /// </summary>
        [Fact]
        public void Factory()
        {
            var factory = ChaCha.GetChaCha20Factory();

            var seed1 = new Seed(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, 0);
            var rng1 = factory.Create(seed1);
            Assert.Equal(137206642u, rng1.NextUInt32());
            var rng2 = factory.Create(rng1);
            Assert.Equal(1325750369u, rng2.NextUInt32());
        }

        /// <summary>
        /// Test Vectors from IETF RFC 7539 Section 2.3.2
        /// https://tools.ietf.org/html/rfc7539#section-2.3.2
        /// </summary>
        [Fact]
        public void Rfc7539Vectors()
        {
            var seed1 = new Seed(new UInt32[] { 0x03020100, 0x07060504, 0x0b0a0908, 0x0f0e0d0c, 0x13121110, 0x17161514, 0x1b1a1918, 0x1f1e1d1c }, 0x4a000000);
            var rng1 = ChaCha.Create(seed1);
            rng1.Position = new ChaCha.Counter(0x0900000000000001, 0);
            var expected1 = new UInt32[]
            {
                0xe4e7f110, 0x15593bd1, 0x1fdd0f50, 0xc47120a3, 0xc7f4d1c7, 0x0368c033, 0x9aaa2204, 0x4e6cd4c3,
                0x466482d2, 0x09aa9f07, 0x05d7c214, 0xa2028bd9, 0xd19c12b5, 0xb94e16de, 0xe883d0cb, 0x4e3c50a2
            };
            foreach (var expected in expected1)
                Assert.Equal(expected, rng1.NextUInt32());
        }

        /// <summary>
        /// Additional Test Vectors from IETF RFC 7539 Appendix A
        /// https://tools.ietf.org/html/rfc7539#appendix-A
        /// </summary>
        [Fact]
        public void Rfc7539AdditionalVectors()
        {
            var seed1 = new Seed();
            var rng1 = ChaCha.Create(seed1);
            var expected1 = new UInt32[]
            {
                0xade0b876, 0x903df1a0, 0xe56a5d40, 0x28bd8653, 0xb819d2bd, 0x1aed8da0, 0xccef36a8, 0xc70d778b,
                0x7c5941da, 0x8d485751, 0x3fe02477, 0x374ad8b8, 0xf4b8436a, 0x1ca11815, 0x69b687c3, 0x8665eeb2
            };
            foreach (var expected in expected1)
                Assert.Equal(expected, rng1.NextUInt32());

            var expected2 = new UInt32[]
            {
                0xbee7079f, 0x7a385155, 0x7c97ba98, 0x0d082d73, 0xa0290fcb, 0x6965e348, 0x3e53c612, 0xed7aee32,
                0x7621b729, 0x434ee69c, 0xb03371d5, 0xd539d874, 0x281fed31, 0x45fb0a51, 0x1f0ae1ac, 0x6f4d794b
            };
            foreach (var expected in expected2)
                Assert.Equal(expected, rng1.NextUInt32());

            var seed3 = new Seed(new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0x01000000 }, 0);
            var rng3 = ChaCha.Create(seed3);
            rng3.Position = new ChaCha.Counter(1, 0);
            var expected3 = new UInt32[]
            {
                0x2452eb3a, 0x9249f8ec, 0x8d829d9b, 0xddd4ceb1, 0xe8252083, 0x60818b01, 0xf38422b8, 0x5aaa49c9,
                0xbb00ca8e, 0xda3ba7b4, 0xc4b592d1, 0xfdf2732f, 0x4436274e, 0x2561b3c8, 0xebdd4aa6, 0xa0136c00
            };
            foreach (var expected in expected3)
                Assert.Equal(expected, rng3.NextUInt32());

            var seed4 = new Seed(new UInt32[] { 0x0000ff00, 0, 0, 0, 0, 0, 0, 0 }, 0);
            var rng4 = ChaCha.Create(seed4);
            rng4.Position = new ChaCha.Counter(2, 0);
            var expected4 = new UInt32[]
            {
                0xfb4dd572, 0x4bc42ef1, 0xdf922636, 0x327f1394, 0xa78dea8f, 0x5e269039, 0xa1bebbc1, 0xcaf09aae,
                0xa25ab213, 0x48a6b46c, 0x1b9d9bcb, 0x092c5be6, 0x546ca624, 0x1bec45d5, 0x87f47473, 0x96f0992e
            };
            foreach (var expected in expected4)
                Assert.Equal(expected, rng4.NextUInt32());

            var seed5 = new Seed(new UInt32[] { 0, 0, 0, 0, 0, 0, 0, 0 }, 0x0200000000000000);
            var rng5 = ChaCha.Create(seed5);
            var expected5 = new UInt32[]
            {
                0x374dc6c2, 0x3736d58c, 0xb904e24a, 0xcd3f93ef, 0x88228b1a, 0x96a4dfb3, 0x5b76ab72, 0xc727ee54,
                0x0e0e978a, 0xf3145c95, 0x1b748ea8, 0xf786c297, 0x99c28f5f, 0x628314e8, 0x398a19fa, 0x6ded1b53
            };
            foreach (var expected in expected5)
                Assert.Equal(expected, rng5.NextUInt32());
        }

        /// <summary>
        /// Verifies that wrapping around the counter works as expected.
        /// </summary>
        [Fact]
        public void WrapAround()
        {
            var seed1 = new Seed();
            var rng1 = ChaCha.Create(seed1);
            rng1.Position = new ChaCha.Counter(UInt64.MaxValue, 0);
            var expected1 = new UInt32[]
            {
                3633091031, 855116898, 3226609235, 1403322700, 855321609, 4126323687, 1743354438, 3969789514,
                3364763159, 2083879426, 3940962572, 4263604596, 3196342980, 731711759, 1451501619, 2306257159,
            };
            foreach (var expected in expected1)
                Assert.Equal(expected, rng1.NextUInt32());

            var expected2 = new UInt32[]
            {
                0xade0b876, 0x903df1a0, 0xe56a5d40, 0x28bd8653, 0xb819d2bd, 0x1aed8da0, 0xccef36a8, 0xc70d778b,
                0x7c5941da, 0x8d485751, 0x3fe02477, 0x374ad8b8, 0xf4b8436a, 0x1ca11815, 0x69b687c3, 0x8665eeb2
            };
            foreach (var expected in expected2)
                Assert.Equal(expected, rng1.NextUInt32());
        }
    }
}
