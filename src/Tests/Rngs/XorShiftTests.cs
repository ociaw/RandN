using System;
using System.Runtime.InteropServices;
using Xunit;

namespace RandN.Rngs;

// Test vectors sourced from
// https://github.com/rust-random/rngs/blob/0a72c77f34f6ae4bd14963b9435f236ee6492fba/rand_xorshift/tests/mod.rs
public class XorShiftTests
{
    [Fact]
    public void Construction()
    {
        var byteSeed = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        var uintSeed = MemoryMarshal.Cast<Byte, UInt32>(byteSeed);
        var seed = (uintSeed[0], uintSeed[1], uintSeed[2], uintSeed[3]);

        var rng1 = XorShift.GetFactory().Create(seed);
        Assert.Equal(4325440999699518727ul, rng1.NextUInt64());

        var rng2 = XorShift.GetFactory().Create(rng1);
        Assert.Equal(15614385950550801700ul, rng1.NextUInt64());
        Assert.Equal(15614385950550801700ul, rng2.NextUInt64());
    }

    [Fact]
    public void Values()
    {
        var byteSeed = new Byte[] { 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        var uintSeed = MemoryMarshal.Cast<Byte, UInt32>(byteSeed);
        var seed = (uintSeed[0], uintSeed[1], uintSeed[2], uintSeed[3]);
        var rng = XorShift.Create(seed);

        var expected32 = new UInt32[]
        {
            2081028795, 620940381, 269070770, 16943764, 854422573, 29242889, 1550291885, 1227154591, 271695242
        };
        foreach (var expected in expected32)
            Assert.Equal(expected, rng.NextUInt32());

        var expected64 = new UInt64[]
        {
            9247529084182843387, 8321512596129439293, 14104136531997710878, 6848554330849612046,
            343577296533772213, 17828467390962600268, 9847333257685787782, 7717352744383350108, 1133407547287910111
        };
        foreach (var expected in expected64)
            Assert.Equal(expected, rng.NextUInt64());

        var results8 = new Byte[32];
        var expected8 = new Byte[]
        {
            102,  57, 212,  16, 233, 130,  49, 183, 158, 187,  44, 203, 63, 149, 45, 17,
            117, 129, 131, 160,  70, 121, 158, 155, 224, 209, 192,  53, 10,  62, 57, 72
        };
        rng.Fill(results8);
        Assert.Equal(expected8, results8);
    }

    [Fact]
    public void BadSeed()
    {
        var seed = (0u, 0u, 0u, 0u);
        var rng1 = XorShift.Create(seed);

        var num1 = rng1.NextUInt64();
        var num2 = rng1.NextUInt64();

        Assert.NotEqual(0ul, num1);
        Assert.NotEqual(num1, num2);
    }

    [Fact]
    public void PartiallyZeroSeed()
    {
        // These tests exist mostly to satisfy Stryker, but they also demonstrate how sensitive XorShift is to bad seeds
        // and zeros in ANY of its components
        var rng1 = XorShift.Create((0u, 0xFF00FF00, 0xFF00FF00, 0xFF00FF00));

        Assert.Equal(504676936644092128ul, rng1.NextUInt64());
        Assert.Equal(504658244946427872ul, rng1.NextUInt64());
        Assert.Equal(18390686710317907999ul, rng1.NextUInt64());
        Assert.Equal(504658107645806567ul, rng1.NextUInt64());

        var rng2 = XorShift.Create((0xFF00FF00, 0, 0xFF00FF00, 0xFF00FF00));
        Assert.Equal(504658240785671967ul, rng2.NextUInt64());
        Assert.Equal(504676936644092128ul, rng2.NextUInt64());
        Assert.Equal(506653961767935775ul, rng2.NextUInt64());
        Assert.Equal(18392631578883266808ul, rng2.NextUInt64());

        var rng3 = XorShift.Create((0xFF00FF00, 0xFF00FF00, 0, 0xFF00FF00));
        Assert.Equal(18374932770393745183ul, rng3.NextUInt64());
        Assert.Equal(504658244946427872ul, rng3.NextUInt64());
        Assert.Equal(18428764962772533279ul, rng3.NextUInt64());
        Assert.Equal(574244029618143231ul, rng3.NextUInt64());

        var rng4 = XorShift.Create((0xFF00FF00, 0xFF00FF00, 0xFF00FF00, 0));
        Assert.Equal(34089021212671ul, rng4.NextUInt64());
        Assert.Equal(17870292117364939007ul, rng4.NextUInt64());
        Assert.Equal(17803257930307839ul, rng4.NextUInt64());
        Assert.Equal(56233289412778208ul, rng4.NextUInt64());
    }

    [Fact]
    public void NonNullable()
    {
        Assert.Throws<ArgumentNullException>(() => XorShift.GetFactory().CreateSeed<StepRng>(null));
    }
}
