using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace RandN.Compat
{
    public sealed class RandomShimTests
    {
        /// <summary>
        /// Verifies <see cref="RandomShim{TRng}.NextBytes(Byte[])"/> methods.
        /// </summary>
        [Fact]
        public void Bytes()
        {
            var rng = new StepRng(0);
            Random random = RandomShim.Create(rng);

            var buffer = new Byte[1024];
            random.NextBytes(buffer);
            var ints = MemoryMarshal.Cast<Byte, UInt64>(buffer);
            for (Int32 i = 0; i < ints.Length; i++)
                Assert.Equal((UInt64)i, ints[i]);

#if !NET472
            random.NextBytes(buffer.AsSpan());
            for (Int32 i = 0; i < ints.Length; i++)
                Assert.Equal((UInt64)(i + ints.Length), ints[i]);
#endif

            var maxRng = new StepRng(UInt64.MaxValue) { Increment = 0 };
            var maxShim = RandomNumberGeneratorShim.Create(maxRng);

            buffer = new Byte[1024];
            maxShim.GetBytes(buffer, 200, 500);
            Assert.All(buffer.Take(200), b => Assert.Equal(0, b));
            Assert.All(buffer.Skip(200).Take(500), b => Assert.Equal(Byte.MaxValue, b));
            Assert.All(buffer.Skip(700), b => Assert.Equal(0, b));
        }

        /// <summary>
        /// Verifies <see cref="RandomShim{TRng}.Next()"/> methods.
        /// </summary>
        [Fact]
        public void Next()
        {
            var rng = new StepRng(0);
            Random random = RandomShim.Create(rng);

            for (Int32 i = 0; i < 100; i++)
                Assert.Equal(i, random.Next());

            for (Int32 i = 0; i < 100; i++)
                Assert.Equal(i, random.Next(100));

            for (Int32 i = 0; i < 100; i++)
                Assert.Equal(i, random.Next(0, 100));

            for (Int32 i = 0; i < 10_000; i++)
            {
                var value = random.NextDouble();
                Assert.True(0 <= value && value < 1);
            }
        }

        [Fact]
        public void NonNullable()
        {
            Assert.Throws<ArgumentNullException>(() => RandomShim.Create<StepRng>(null));

            var rng = RandomShim.Create(new StepRng(0));
            Assert.Throws<ArgumentNullException>(() => rng.NextBytes(null));
        }
    }
}
