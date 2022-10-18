using System;
using BenchmarkDotNet.Attributes;
using RandN.Rngs;

namespace RandN.Benchmarks;

[RngConfig(Iterations * sizeof(UInt32))]
public class RngUInt32
{
    public const Int32 Iterations = 4096;

    private readonly ChaCha _chaCha8;
    private readonly ChaCha _chaCha12;
    private readonly ChaCha _chaCha20;
    private readonly Pcg32 _pcg32;
    private readonly Mt1993764 _mt1993764;
    private readonly XorShift _xorShift;
    private readonly SystemCryptoRng _systemCryptoRng;
#pragma warning disable CS0618
    private readonly CryptoServiceProvider _cryptoServiceProvider;
#pragma warning restore CS0618
    private readonly Random _random;

    public RngUInt32()
    {
        _chaCha8 = ChaCha.GetChaCha8Factory().Create(new ChaCha.Seed());
        _chaCha12 = ChaCha.GetChaCha12Factory().Create(new ChaCha.Seed());
        _chaCha20 = ChaCha.GetChaCha20Factory().Create(new ChaCha.Seed());
        _pcg32 = Rngs.Pcg32.Create(0, 0);
        _mt1993764 = Rngs.Mt1993764.Create(0);
        _xorShift = Rngs.XorShift.Create(1, 1, 1, 1);
        _systemCryptoRng = Rngs.SystemCryptoRng.Create();
#pragma warning disable CS0618
        _cryptoServiceProvider = Rngs.CryptoServiceProvider.Create();
#pragma warning restore CS0618
        _random = new Random(42);
    }

    [Benchmark]
    public UInt32 ChaCha8()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _chaCha8.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 ChaCha12()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _chaCha12.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 ChaCha20()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _chaCha20.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 Mt1993764()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _mt1993764.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 Pcg32()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _pcg32.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 XorShift()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _xorShift.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 SystemCryptoRng()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _systemCryptoRng.NextUInt32());
        return sum;
    }

    [Benchmark]
    public UInt32 CryptoServiceProvider()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _cryptoServiceProvider.NextUInt32());
        return sum;
    }

    /// <summary>
    /// Provided as a point of comparison.
    /// </summary>
    [Benchmark]
    public UInt32 SystemRandom()
    {
        // Not actually equivalent to NextUInt32, since it doesn't cover the full 32-bit range.
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations - 1; i++)
            sum = unchecked(sum + (UInt32)_random.Next(Int32.MinValue, Int32.MaxValue));
        return sum;
    }
}