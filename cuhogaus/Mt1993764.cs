using System;
using System.Buffers.Binary;

namespace Cuhogaus
{
    /// <summary>
    /// An implementation of Mersenne Twister, variant MT19937-64.
    /// Based on the pseudocode provided from https://en.wikipedia.org/wiki/Mersenne_twister.
    /// </summary>
    public sealed class Mt1993764 : IRng
    {
        private const int WordSize = sizeof(ulong) * 8; // 64 bits (w)
        private const int RecurrenceDegree = 312; // (n)
        private const int MiddleWord = 156; // (m)
        private const int OneWordSeparationPoint = 31; // (r)

        private const ulong LowerMask = (1ul << OneWordSeparationPoint) - 1;
        private const ulong UpperMask = ~LowerMask;

        private const ulong TwistMatrixCoefficients = 0xB5026F5AA96619E9; // (a)

        // TGSFSR(R) tempering bitmasks
        private const ulong BMask = 0x71D67FFFEDA60000;
        private const ulong CMask = 0xFFF7EEE000000000;

        // TGSFSR(R) tempering bit shifts
        private const int SShift = 17;
        private const int TShift = 37;

        // Additional tempering bit shifts and masks
        private const int LShift = 43;
        private const int UShift = 29;
        private const ulong DMask = 0x5555555555555555;

        private const ulong F = 6364136223846793005;

        private ulong[] _state;
        private Int32 _index;

        private Mt1993764(ulong[] state)
        {
            _state = state;
            _index = state.Length;
        }

        public uint NextUInt32() => UInt64Filler.ToUInt32(NextUInt64());

        public ulong NextUInt64()
        {
            if (_index == _state.Length)
                Twist();

            ulong y = _state[_index];
            y ^= (y >> UShift) & DMask;
            y ^= (y << SShift) & BMask;
            y ^= (y << TShift) & CMask;
            y ^= y >> LShift;

            _index++;

            return y;
        }

        public void Fill(byte[] buffer) => UInt64Filler.Fill(buffer, NextUInt64);

        public void Fill(Span<Byte> buffer) => UInt64Filler.Fill(buffer, NextUInt64);

        private void Twist()
        {
            for (int i = 0; i < _state.Length; i++)
            {
                ulong upper = _state[i] & UpperMask;
                ulong lower = _state[(i + 1) % _state.Length] & LowerMask;
                ulong x = upper + lower;

                ulong xA = x >> 1;
                if (x % 2 == 0)
                    xA ^= TwistMatrixCoefficients;

                _state[i] = _state[(i + MiddleWord) % RecurrenceDegree] ^ xA;
            }

            _index = 0;
        }

        public sealed class Factory : IRngFactory
        {
            public Int32 SeedLength => sizeof(ulong);

            public IRng Create(ReadOnlySpan<Byte> seed)
            {
                // Init state from seed
                ulong[] state = new ulong[RecurrenceDegree];
                BinaryPrimitives.TryReadUInt64LittleEndian(seed, out state[0]);

                for (uint i = 1; i < state.Length; i++)
                {
                    state[i] = F * (state[i - 1] ^ (state[i - 1] >> (WordSize - 2))) + i;
                }

                return new Mt1993764(state);
            }
        }
    }
}
