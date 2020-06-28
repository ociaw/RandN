using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace RandN.Compat
{
    public sealed class RandomNumberGeneratorShimTests
    {
        /// <summary>
        /// Verifies <see cref="RandomNumberGeneratorShim{TRng}.GetNonZeroBytes(Byte[])"/> method.
        /// </summary>
        [Fact]
        public void NonZeroBytes()
        {
            var rng = new SequenceRng(new UInt32[] { 0, 1, 2, 0, 1, 2, 0, 1 });
            var shim = RandomNumberGeneratorShim.Create(rng);

            var buffer = new Byte[35];
            shim.GetNonZeroBytes(buffer);
            Assert.DoesNotContain((Byte)0, buffer);

#if !NET472
            shim.GetNonZeroBytes(buffer.AsSpan());
            Assert.DoesNotContain((Byte)0, buffer);
#endif
        }

        /// <summary>
        /// Verifies <see cref="RandomNumberGeneratorShim{TRng}.GetBytes(Byte[])"/> methods.
        /// </summary>
        [Fact]
        public void Bytes()
        {
            var rng = new StepRng(0);
            var shim = RandomNumberGeneratorShim.Create(rng);

            var buffer = new Byte[1024];
            shim.GetBytes(buffer);
            var ints = MemoryMarshal.Cast<Byte, UInt64>(buffer);
            for (Int32 i = 0; i < ints.Length; i++)
                Assert.Equal((UInt64)i, ints[i]);

#if !NET472
            shim.GetBytes(buffer.AsSpan());
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
    }
}
