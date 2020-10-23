using System;
using BenchmarkDotNet.Attributes;
using RandN.Distributions;

namespace RandN.Benchmarks
{
    public class UniformDists
    {
        public const Int32 Iterations = 16384;

        public const Int32 UpperBound = 88;
        public const Int32 LowerBound = 21;

        private readonly StepRng _rng;

        private readonly UniformSByte _uniformSByte;
        private readonly UniformInt16 _uniformInt16;
        private readonly UniformInt32 _uniformInt32;
        private readonly UniformInt64 _uniformInt64;
        private readonly UniformByte _uniformByte;
        private readonly UniformUInt16 _uniformUInt16;
        private readonly UniformUInt32 _uniformUInt32;
        private readonly UniformUInt64 _uniformUInt64;

        private readonly UniformFloat<Single> _uniformSingle;
        private readonly UniformFloat<Double> _uniformDouble;

        private readonly UniformTimeSpan _uniformTimeSpan;

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
    }
}
