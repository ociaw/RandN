using System;
using System.Runtime.InteropServices;
using Xunit;

namespace RandN.Implementation;

public sealed class FillerTests
{
    [Theory]
    [InlineData(0, 0x0100000000)]
    [InlineData(0x0123456789ABCDEF, 0x89ABCDF089ABCDEF)]
    [InlineData(UInt32.MaxValue, UInt32.MaxValue)]
    [InlineData(UInt64.MaxValue, UInt32.MaxValue)]
    public void NextUInt64ViaUInt32(UInt64 initialRngState, UInt64 expected)
    {
        var rng = new StepRng(initialRngState);
        Assert.Equal(expected, Filler.NextUInt64ViaUInt32(rng));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0x0123456789ABCDEF, 0x89ABCDEF)]
    [InlineData(UInt32.MaxValue, UInt32.MaxValue)]
    [InlineData(UInt64.MaxValue, UInt32.MaxValue)]
    public void NextUInt32ViaUInt64(UInt64 initialRngState, UInt32 expected)
    {
        var rng = new StepRng(initialRngState);
        Assert.Equal(expected, Filler.NextUInt32ViaUInt64(rng));
    }

    [Fact]
    public void FillViaNextCleanly()
    {
        var rng = new StepRng(0);
        var buffer = new UInt64[8];
        Filler.FillBytesViaNext(rng, MemoryMarshal.Cast<UInt64, Byte>(buffer));
        for (UInt32 i = 0; i < buffer.Length; i++)
        {
            Assert.Equal(i, buffer[i]);
        }
    }

    [Fact]
    public void FillViaNext4BytesLeft()
    {
        var rng = new StepRng(0x0123456789ABCDEF)
        {
            Increment = 0x0001000100010001
        };
        var buffer = new UInt32[17];
        Filler.FillBytesViaNext(rng, MemoryMarshal.Cast<UInt32, Byte>(buffer));

        var expected = new UInt32[]
        {
            0x89ABCDEF, 0x01234567,
            0x89ACCDF0, 0x01244568,
            0x89ADCDF1, 0x01254569,
            0x89AECDF2, 0x0126456A,
            0x89AFCDF3, 0x0127456B,
            0x89B0CDF4, 0x0128456C,
            0x89B1CDF5, 0x0129456D,
            0x89B2CDF6, 0x012A456E,
            0x89B3CDF7,
        };

        Assert.Equal(expected, buffer);
    }

    [Fact]
    public void FillViaNext2BytesLeft()
    {
        var rng = new StepRng(0x0123456789ABCDEF)
        {
            Increment = 0x0001000100010001
        };
        var buffer = new UInt16[33];
        Filler.FillBytesViaNext(rng, MemoryMarshal.Cast<UInt16, Byte>(buffer));
        var expected = new UInt16[]
        {
            0xCDEF, 0x89AB, 0x4567, 0x0123,
            0xCDF0, 0x89AC, 0x4568, 0x0124,
            0xCDF1, 0x89AD, 0x4569, 0x0125,
            0xCDF2, 0x89AE, 0x456A, 0x0126,
            0xCDF3, 0x89AF, 0x456B, 0x0127,
            0xCDF4, 0x89B0, 0x456C, 0x0128,
            0xCDF5, 0x89B1, 0x456D, 0x0129,
            0xCDF6, 0x89B2, 0x456E, 0x012A,
            0xCDF7,
        };

        Assert.Equal(expected, buffer);
    }

    [Fact]
    public void FillViaNext6BytesLeft()
    {
        var rng = new StepRng(0x0123456789ABCDEF)
        {
            Increment = 0x0001000100010001
        };
        var buffer = new UInt16[35];
        Filler.FillBytesViaNext(rng, MemoryMarshal.Cast<UInt16, Byte>(buffer));

        var expected = new UInt16[]
        {
            0xCDEF, 0x89AB, 0x4567, 0x0123,
            0xCDF0, 0x89AC, 0x4568, 0x0124,
            0xCDF1, 0x89AD, 0x4569, 0x0125,
            0xCDF2, 0x89AE, 0x456A, 0x0126,
            0xCDF3, 0x89AF, 0x456B, 0x0127,
            0xCDF4, 0x89B0, 0x456C, 0x0128,
            0xCDF5, 0x89B1, 0x456D, 0x0129,
            0xCDF6, 0x89B2, 0x456E, 0x012A,
            0xCDF7, 0x89B3, 0x456F,
        };

        Assert.Equal(expected, buffer);
    }

    [Fact]
    public void FillViaUInt32ChunksIncomplete()
    {
        var source = new UInt32[]
        {
            0x1111_1111, 0x2222_2222, 0x3333_3333,
            0x4444_4444, 0x5555_5555, 0x6666_6666,
            0x7777_7777, 0x8888_8888, 0x9999_9999,
        };
        var dest = new UInt32[12];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt32Chunks(source, MemoryMarshal.Cast<UInt32, Byte>(dest));
        Assert.Equal(9, consumed);
        Assert.Equal(36, filled);

        for (Int32 i = 0; i < 9; i++)
            Assert.Equal(source[i], dest[i]);

        Assert.Equal(0u, dest[9]);
        Assert.Equal(0u, dest[10]);
        Assert.Equal(0u, dest[11]);
    }

    [Fact]
    public void FillViaUInt32ChunksComplete()
    {
        var source = new UInt32[]
        {
            0x1111_1111, 0x2222_2222, 0x3333_3333,
            0x4444_4444, 0x5555_5555, 0x6666_6666,
            0x7777_7777, 0x8888_8888, 0x9999_9999,
        };
        var dest = new UInt32[7];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt32Chunks(source, MemoryMarshal.Cast<UInt32, Byte>(dest));
        Assert.Equal(7, consumed);
        Assert.Equal(28, filled);

        for (Int32 i = 0; i < 7; i++)
            Assert.Equal(source[i], dest[i]);
    }

    [Fact]
    public void FillViaUInt32ChunksCompletePartial()
    {
        var source = new UInt32[]
        {
            0x1111_1111, 0x2222_2222, 0x3333_3333,
            0x4444_4444, 0x5555_5555, 0x6666_6666,
            0x7777_7777, 0x8888_8888, 0x9999_9999,
        };
        var dest = new UInt16[13];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt32Chunks(source, MemoryMarshal.Cast<UInt16, Byte>(dest));
        Assert.Equal(7, consumed);
        Assert.Equal(26, filled);

        for (Int32 i = 0; i < 6; i += 2)
        {
            Assert.Equal(source[i / 2] & 0xFFFF, dest[i]);
            Assert.Equal(source[i / 2] & 0xFFFF, dest[i + 1]);
        }
    }

    [Fact]
    public void FillViaUInt64ChunksIncomplete()
    {
        var source = new UInt64[]
        {
            0x1111_1111_1111_1111, 0x2222_2222_2222_2222, 0x3333_3333_3333_3333,
            0x4444_4444_4444_4444, 0x5555_5555_5555_5555, 0x6666_6666_6666_6666,
            0x7777_7777_7777_7777, 0x8888_8888_8888_8888, 0x9999_9999_9999_9999,
        };
        var dest = new UInt64[12];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt64(source, MemoryMarshal.Cast<UInt64, Byte>(dest));
        Assert.Equal(9, consumed);
        Assert.Equal(72, filled);

        for (Int32 i = 0; i < 9; i++)
            Assert.Equal(source[i], dest[i]);

        Assert.Equal(0u, dest[9]);
        Assert.Equal(0u, dest[10]);
        Assert.Equal(0u, dest[11]);
    }

    [Fact]
    public void FillViaUInt64ChunksComplete()
    {
        var source = new UInt64[]
        {
            0x1111_1111_1111_1111, 0x2222_2222_2222_2222, 0x3333_3333_3333_3333,
            0x4444_4444_4444_4444, 0x5555_5555_5555_5555, 0x6666_6666_6666_6666,
            0x7777_7777_7777_7777, 0x8888_8888_8888_8888, 0x9999_9999_9999_9999,
        };
        var dest = new UInt64[7];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt64(source, MemoryMarshal.Cast<UInt64, Byte>(dest));
        Assert.Equal(7, consumed);
        Assert.Equal(56, filled);

        for (Int32 i = 0; i < 7; i++)
            Assert.Equal(source[i], dest[i]);
    }

    [Fact]
    public void FillViaUInt64ChunksCompletePartial()
    {
        var source = new UInt64[]
        {
            0x1111_1111_1111_1111, 0x2222_2222_2222_2222, 0x3333_3333_3333_3333,
            0x4444_4444_4444_4444, 0x5555_5555_5555_5555, 0x6666_6666_6666_6666,
            0x7777_7777_7777_7777, 0x8888_8888_8888_8888, 0x9999_9999_9999_9999,
        };
        var dest = new UInt32[13];
        (Int32 consumed, Int32 filled) = Filler.FillViaUInt64(source, MemoryMarshal.Cast<UInt32, Byte>(dest));
        Assert.Equal(7, consumed);
        Assert.Equal(52, filled);

        for (Int32 i = 0; i < 6; i += 2)
        {
            Assert.Equal(source[i / 2] & 0xFFFF_FFFF, dest[i]);
            Assert.Equal(source[i / 2] & 0xFFFF_FFFF, dest[i + 1]);
        }
    }
}
