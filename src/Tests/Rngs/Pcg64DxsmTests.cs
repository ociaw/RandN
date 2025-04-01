using System;

using Xunit;

namespace RandN.Rngs;

#if NET7_0_OR_GREATER

/// <summary>
/// Tests <see cref="Pcg64Dxsm"/> against a PCG64 DXSM reference implementation, available
/// <seealso href="https://dotat.at/@/2023-06-21-pcg64-dxsm.html">here</seealso>.
/// </summary>
public class Pcg64DxsmTests
{
    [Fact]
    public void Construction()
    {
        const UInt64 seedState = 578437695752307201ul;
        const UInt64 stream = 1157159078456920585ul >> 1;
        var seed1 = new Pcg64Dxsm.Seed(seedState, stream);
        var rng1 = Pcg64Dxsm.GetFactory().Create(seed1);
        Assert.Equal(5257266640616119569ul, rng1.NextUInt64());

        // test creating a seed from an existing rng
        var seed2 = Pcg64Dxsm.GetFactory().CreateSeed(rng1);

        // C# doesn't yet support UInt128 literals, so we'll check in 64 bit chunks.
        UInt64 mask = 0xFFFFFFFFFFFFFFFF;
        Assert.Equal(12196396763021089697ul, seed2.State & mask);
        Assert.Equal(3539283475694197520ul, (seed2.State >> 64) & mask);
        Assert.Equal(9543865764773069391ul, seed2.Stream & mask);
        Assert.Equal(4999610333945728358ul, (seed2.Stream >> 64) & mask);

        // test that created seed, but with downshifting the stream created above by one.
        seed2 = new Pcg64Dxsm.Seed(seed2.State, seed2.Stream >> 1);
        var rng2 = Pcg64Dxsm.GetFactory().Create(seed2);
        Assert.Equal(14559457676514576596ul, rng2.NextUInt64());
    }

    [Fact]
    public void Reference()
    {
        var rng = Pcg64Dxsm.Create(42, 54);
        Assert.Equal(0xF0847C9518BDDB90, rng.NextUInt64());
        Assert.Equal(0x8E7D5F5514BA8AAA, rng.NextUInt64());
        Assert.Equal(0x86FBD36F8028F6FD, rng.NextUInt64());
        Assert.Equal(0x8D14B6EDBE9F740A, rng.NextUInt64());
        Assert.Equal(0xA85B2896C7CAD55D, rng.NextUInt64());
        Assert.Equal(0x8CA3894A1D9227BB, rng.NextUInt64());
    }

    [Fact]
    public void NonNullable()
    {
        Assert.Throws<ArgumentNullException>(() => Pcg32.GetFactory().CreateSeed<StepRng>(null));
    }
}

#endif // NET7_0_OR_GREATER
