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
        public Byte[] ChaCha8()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha8.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] ChaCha12()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha12.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] ChaCha20()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _chaCha20.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] Mt1993764()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _mt1993764.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] Pcg32()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _pcg32.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] XorShift()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _xorShift.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] CryptoServiceProvider()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _cryptoServiceProvider.Fill(buffer);
            return buffer;
        }

        /// <summary>
        /// Provided as a point of comparison.
        /// </summary>
        [Benchmark]
        public Byte[] SystemRandom()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            for (Int32 i = 0; i < ITERATIONS; i++)
                _random.NextBytes(buffer);
            return buffer;
        }
    }
}
