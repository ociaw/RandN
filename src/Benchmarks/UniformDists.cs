using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using RandN.Distributions;
// ReSharper disable RedundantCast
// ReSharper disable RedundantOverflowCheckingContext

namespace RandN.Benchmarks;

public class UniformDists
{
    public const Int32 Iterations = 16384;

    public const Int32 UpperBound = 88;
    public const Int32 LowerBound = 21;

    private readonly StepRng _rng;

    private readonly Uniform.SByte _uniformSByte;
    private readonly Uniform.Int16 _uniformInt16;
    private readonly Uniform.Int32 _uniformInt32;
    private readonly Uniform.Int64 _uniformInt64;
    private readonly Uniform.Byte _uniformByte;
    private readonly Uniform.UInt16 _uniformUInt16;
    private readonly Uniform.UInt32 _uniformUInt32;
    private readonly Uniform.UInt64 _uniformUInt64;

    private readonly Uniform.Single _uniformSingle;
    private readonly Uniform.Double _uniformDouble;

    private readonly Uniform.TimeSpan _uniformTimeSpan;
    private readonly Uniform.BigInteger _uniformBigInteger;

#if NET8_0_OR_GREATER
    private readonly Uniform.Int128 _uniformInt128;
    private readonly Uniform.UInt128 _uniformUInt128;
#endif

    public UniformDists()
    {
        _rng = new StepRng(0);
        _uniformSByte = Uniform.New((SByte)LowerBound, (SByte)UpperBound);
        _uniformInt16 = Uniform.New((Int16)LowerBound, (Int16)UpperBound);
        _uniformInt32 = Uniform.New(LowerBound, UpperBound);
        _uniformInt64 = Uniform.New((Int64)LowerBound, (Int64)UpperBound);

        _uniformByte = Uniform.New((Byte)LowerBound, (Byte)UpperBound);
        _uniformUInt16 = Uniform.New((UInt16)LowerBound, (UInt16)UpperBound);
        _uniformUInt32 = Uniform.New((UInt32)LowerBound, (UInt32)UpperBound);
        _uniformUInt64 = Uniform.New((UInt64)LowerBound, (UInt64)UpperBound);

        _uniformSingle = Uniform.New((Single)LowerBound, (Single)UpperBound);
        _uniformDouble = Uniform.New((Double)LowerBound, (Double)UpperBound);

        _uniformTimeSpan = Uniform.New(TimeSpan.FromHours(LowerBound), TimeSpan.FromHours(UpperBound));
        _uniformBigInteger = Uniform.New(new BigInteger(LowerBound), new BigInteger(UpperBound));

#if NET8_0_OR_GREATER
        _uniformInt128 = Uniform.New((Int128)LowerBound, (Int128)UpperBound);
        _uniformUInt128 = Uniform.New((UInt128)LowerBound, (UInt128)UpperBound);
#endif
    }

    [Benchmark]
    public Int32 SampleSByte()
    {
        Int32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformSByte.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public Int32 SampleInt16()
    {
        Int32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformInt16.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public Int32 SampleInt32()
    {
        Int32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformInt32.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public Int64 SampleInt64()
    {
        Int64 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformInt64.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public UInt32 SampleByte()
    {
        UInt32 sum = 0;
        for (UInt32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformByte.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public UInt32 SampleUInt16()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformUInt16.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public UInt32 SampleUInt32()
    {
        UInt32 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformUInt32.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public UInt64 SampleUInt64()
    {
        UInt64 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformUInt64.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public Double SampleSingle()
    {
        Double sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformSingle.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public Double SampleDouble()
    {
        Double sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformDouble.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public TimeSpan SampleTimeSpan()
    {
        TimeSpan sum = TimeSpan.Zero;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformTimeSpan.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public BigInteger SampleBigInteger()
    {
        BigInteger sum = BigInteger.Zero;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformBigInteger.Sample(_rng));
        return sum;
    }

#if NET8_0_OR_GREATER
    [Benchmark]
    public Int128 SampleInt128()
    {
        Int128 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformInt128.Sample(_rng));
        return sum;
    }

    [Benchmark]
    public UInt128 SampleUInt128()
    {
        UInt128 sum = 0;
        for (Int32 i = 0; i < Iterations; i++)
            sum = unchecked(sum + _uniformUInt128.Sample(_rng));
        return sum;
    }
#endif
}
