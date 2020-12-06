using System;
using System.Runtime.InteropServices;
using Xunit;

namespace RandN.Implementation
{
    // ReSharper disable once InconsistentNaming
    public sealed class BlockBuffer32_1Tests
    {
        [Fact]
        public void UInt64SpansBlock()
        {
            var rngCore = new StepBlockRng();
            BlockBuffer32<StepBlockRng> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 7; i++)
            {
                Assert.Equal(1u, blockBuffer.NextUInt32());
            }
            var spanned = blockBuffer.NextUInt64();
            Assert.Equal(1u, spanned & 0xFFFFFFFF);
            Assert.Equal(2u, spanned >> 32);
        }

        [Fact]
        public void UInt32WrapAround()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(UInt32.MaxValue, blockBuffer.NextUInt32());
            }
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(0u, blockBuffer.NextUInt32());
            }
        }

        [Fact]
        public void UInt64WrapAround()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 4; i++)
            {
                Assert.Equal(UInt64.MaxValue, blockBuffer.NextUInt64());
            }
            for (Int32 i = 0; i < 4; i++)
            {
                Assert.Equal(0u, blockBuffer.NextUInt64());
            }
        }

        [Fact]
        public void BadIndexes()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng> blockBuffer = new(rngCore);
            Assert.Throws<ArgumentOutOfRangeException>(() => blockBuffer.GenerateAndSet(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => blockBuffer.GenerateAndSet(20));
        }

        [Fact]
        public void Fill()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng> blockBuffer = new(rngCore);
            var dest = new UInt32[17];
            blockBuffer.Fill(MemoryMarshal.Cast<UInt32, Byte>(dest));

            for (UInt32 i = 0; i < rngCore.BlockLength; i++)
                Assert.Equal(UInt32.MaxValue, dest[i]);
            for (UInt32 i = (UInt32)rngCore.BlockLength; i < rngCore.BlockLength * 2; i++)
                Assert.Equal(0u, dest[i]);
            Assert.Equal(1u, dest[16]);
        }
    }

    // ReSharper disable once InconsistentNaming
    public sealed class BlockBuffer32_2Tests
    {
        [Fact]
        public void UInt64SpansBlock()
        {
            var rngCore = new StepBlockRng();
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 7; i++)
            {
                Assert.Equal(1u, blockBuffer.NextUInt32());
            }
            var spanned = blockBuffer.NextUInt64();
            Assert.Equal(1u, spanned & 0xFFFFFFFF);
            Assert.Equal(2u, spanned >> 32);
        }

        [Fact]
        public void UInt32WrapAround()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(UInt32.MaxValue, blockBuffer.NextUInt32());
            }
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(0u, blockBuffer.NextUInt32());
            }
            Assert.Equal(0u, blockBuffer.BlockCounter);
        }

        [Fact]
        public void UInt64WrapAround()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore);
            for (Int32 i = 0; i < 4; i++)
            {
                Assert.Equal(UInt64.MaxValue, blockBuffer.NextUInt64());
            }
            for (Int32 i = 0; i < 4; i++)
            {
                Assert.Equal(0u, blockBuffer.NextUInt64());
            }
            Assert.Equal(0u, blockBuffer.BlockCounter);
        }

        [Fact]
        public void SetCounter()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore) { BlockCounter = 9u, Index = 0 };
            blockBuffer.BlockCounter = 9u;
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(9u, blockBuffer.NextUInt32());
            }
            for (Int32 i = 0; i < 8; i++)
            {
                Assert.Equal(10u, blockBuffer.NextUInt32());
            }
            Assert.Equal(10u, blockBuffer.BlockCounter);
        }

        [Fact]
        public void BadIndexes()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore);
            Assert.Throws<ArgumentOutOfRangeException>(() => blockBuffer.GenerateAndSet(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => blockBuffer.GenerateAndSet(20));
        }

        [Fact]
        public void Fill()
        {
            var rngCore = new StepBlockRng { BlockCounter = UInt32.MaxValue - 1 };
            BlockBuffer32<StepBlockRng, UInt32> blockBuffer = new(rngCore);
            var dest = new UInt32[17];
            blockBuffer.Fill(MemoryMarshal.Cast<UInt32, Byte>(dest));

            for (UInt32 i = 0; i < rngCore.BlockLength; i++)
                Assert.Equal(UInt32.MaxValue, dest[i]);
            for (UInt32 i = (UInt32)rngCore.BlockLength; i < rngCore.BlockLength * 2; i++)
                Assert.Equal(0u, dest[i]);
            Assert.Equal(1u, dest[16]);
        }
    }
}
