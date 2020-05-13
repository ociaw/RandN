using System;
using BenchmarkDotNet.Attributes;
using RandN.Rngs;

namespace RandN.Benchmarks
{
    public class Rngs
    {
        private const Int32 BUFFER_LENGTH = 1024;

        private readonly ChaCha _chaCha8;
        private readonly ChaCha _chaCha12;
        private readonly ChaCha _chaCha20;
        private readonly Pcg32 _pcg32;
        private readonly Mt1993764 _mt1993764;
        private readonly SystemRandom _systemRandom;
        private readonly XorShift _xorShift;
        private readonly CryptoServiceProvider _cryptoServiceProvider;

        public Rngs()
        {
            _chaCha8 = ChaCha.Create(new ChaCha.Seed(), 8);
            _chaCha12 = ChaCha.Create(new ChaCha.Seed(), 12);
            _chaCha20 = ChaCha.Create(new ChaCha.Seed(), 20);
            _pcg32 = Pcg32.Create(0, 0);
            _mt1993764 = Mt1993764.Create(0);
            _systemRandom = SystemRandom.Create(0);
            _xorShift = XorShift.Create(1, 1, 1, 1);
            _cryptoServiceProvider = CryptoServiceProvider.Create();
        }

        [Benchmark]
        public Byte[] ChaCha8Fill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _chaCha8.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] ChaCha12Fill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _chaCha12.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] ChaCha20Fill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _chaCha20.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] Mt1993764Fill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _mt1993764.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] Pcg32Fill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _pcg32.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] SystemRandomFill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _systemRandom.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] XorShiftFill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _xorShift.Fill(buffer);
            return buffer;
        }

        [Benchmark]
        public Byte[] CryptoServiceProviderFill()
        {
            Byte[] buffer = new Byte[BUFFER_LENGTH];
            _cryptoServiceProvider.Fill(buffer);
            return buffer;
        }
    }
}
