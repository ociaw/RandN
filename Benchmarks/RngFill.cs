using System;
using BenchmarkDotNet.Attributes;
using RandN.Rngs;

namespace RandN.Benchmarks
{
    [RngConfig(ITERATIONS * BUFFER_LENGTH)]
    public class RngFill
    {
        public const Int32 ITERATIONS = 1024;
        public const Int32 BUFFER_LENGTH = 1024;

        private readonly ChaCha _chaCha8;
        private readonly ChaCha _chaCha12;
        private readonly ChaCha _chaCha20;
        private readonly Pcg32 _pcg32;
        private readonly Mt1993764 _mt1993764;
        private readonly XorShift _xorShift;
        private readonly CryptoServiceProvider _cryptoServiceProvider;
        private readonly Random _random;

        public RngFill()
        {
            _chaCha8 = ChaCha.GetChaCha8Factory().Create(new ChaCha.Seed());
            _chaCha12 = ChaCha.GetChaCha12Factory().Create(new ChaCha.Seed());
            _chaCha20 = ChaCha.GetChaCha20Factory().Create(new ChaCha.Seed());
            _pcg32 = Rngs.Pcg32.Create(0, 0);
            _mt1993764 = Rngs.Mt1993764.Create(0);
            _xorShift = Rngs.XorShift.Create(1, 1, 1, 1);
            _cryptoServiceProvider = Rngs.CryptoServiceProvider.Create();
            _random = new Random(42);
        }

        [Benchmark]
        public void ChaCha8()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha8.Fill(buffer);
        }

        [Benchmark]
        public void ChaCha12()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha12.Fill(buffer);
        }

        [Benchmark]
        public void ChaCha20()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha20.Fill(buffer);
        }

        [Benchmark]
        public void Mt1993764()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _mt1993764.Fill(buffer);
        }

        [Benchmark]
        public void Pcg32()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _pcg32.Fill(buffer);
        }

        [Benchmark]
        public void XorShift()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _xorShift.Fill(buffer);
        }

        [Benchmark]
        public void CryptoServiceProvider()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _cryptoServiceProvider.Fill(buffer);
        }

        /// <summary>
        /// Provided as a point of comparison.
        /// </summary>
        [Benchmark]
        public void SystemRandom()
        {
            Span<Byte> buffer = stackalloc Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _random.NextBytes(buffer);
        }
    }
}
