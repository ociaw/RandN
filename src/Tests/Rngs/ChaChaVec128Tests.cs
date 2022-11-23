#if X86_INTRINSICS
using System;
using Xunit;

namespace RandN.Rngs
{
    public sealed class ChaChaVec128Tests
    {
        [Vec128HwAccelRequiredFact]
        public void Regenerate20()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, 0, 0, 10);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Regenerate(buffer);
            Assert.Equal(137206642u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void Generate20()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, UInt64.MaxValue, 0, 10);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Generate(buffer);
            Assert.Equal(137206642u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void Regenerate12()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, 0, 0, 6);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Regenerate(buffer);
            Assert.Equal(1488489079u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void Generate12()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, UInt64.MaxValue, 0, 6);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Generate(buffer);
            Assert.Equal(1488489079u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void Regenerate8()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, 0, 0, 4);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Regenerate(buffer);
            Assert.Equal(3680296248u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void Generate8()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, UInt64.MaxValue, 0, 4);
            Span<UInt32> buffer = stackalloc UInt32[64];
            rng.Generate(buffer);
            Assert.Equal(3680296248u, buffer[0]);
            Assert.Equal(0ul, rng.Stream);
        }

        [Vec128HwAccelRequiredFact]
        public void StreamModification()
        {
            var rng = ChaChaVec128.Create(new UInt32[] { 0, 0, 1, 0, 2, 0, 3, 0 }, 0, 0, 4);
            Span<UInt32> buffer = stackalloc UInt32[64];
            Span<UInt32> buffer2 = stackalloc UInt32[64];
            rng.Regenerate(buffer);
            rng.Stream = 1ul;
            rng.Regenerate(buffer2);

            for (Int32 i = 0; i < buffer.Length; i++)
                Assert.NotEqual(buffer[i], buffer2[i]);
        }
    }
}
#endif
