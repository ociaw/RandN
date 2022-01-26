using System;
using BenchmarkDotNet.Attributes;
using RandN.Rngs;

namespace RandN.Benchmarks
{
    [RngConfig(Iterations * sizeof(UInt64))]
    public class RngUInt64
    {
        public const Int32 Iterations = 4096;

        private readonly ChaCha _chaCha8;
        private readonly ChaCha _chaCha12;
        private readonly ChaCha _chaCha20;
        private readonly Pcg32 _pcg32;
        private readonly Mt1993764 _mt1993764;
        private readonly XorShift _xorShift;
#pragma warning disable CS0618
        private readonly CryptoServiceProvider _cryptoServiceProvider;
#pragma warning restore CS0618
        private readonly Random _random;

        public RngUInt64()
        {
            _chaCha8 = ChaCha.GetChaCha8Factory().Create(new ChaCha.Seed());
            _chaCha12 = ChaCha.GetChaCha12Factory().Create(new ChaCha.Seed());
            _chaCha20 = ChaCha.GetChaCha20Factory().Create(new ChaCha.Seed());
            _pcg32 = Rngs.Pcg32.Create(0, 0);
            _mt1993764 = Rngs.Mt1993764.Create(0);
            _xorShift = Rngs.XorShift.Create(1, 1, 1, 1);
#pragma warning disable CS0618
            _cryptoServiceProvider = Rngs.CryptoServiceProvider.Create();
#pragma warning restore CS0618
            _random = new Random(42);
        }

        [Benchmark]
        public UInt64 ChaCha8()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _chaCha8.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 ChaCha12()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _chaCha12.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 ChaCha20()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _chaCha20.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 Mt1993764()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _mt1993764.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 Pcg32()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _pcg32.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 XorShift()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _xorShift.NextUInt64());
            return sum;
        }

        [Benchmark]
        public UInt64 CryptoServiceProvider()
        {
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
                sum = unchecked(sum + _cryptoServiceProvider.NextUInt64());
            return sum;
        }

        /// <summary>
        /// Provided as a point of comparison.
        /// </summary>
        [Benchmark]
        public UInt64 SystemRandom()
        {
            // Not actually equivalent to NextUInt64, since it doesn't cover the full 32-bit range.
            UInt64 sum = 0;
            for (Int32 i = 0; i < Iterations; i++)
            {
                UInt64 num = (UInt64)_random.Next(Int32.MinValue, Int32.MaxValue) << 32 | (UInt32)_random.Next(Int32.MinValue, Int32.MaxValue);
                sum = unchecked(sum + num);
            }
            return sum;
        }
    }
}
