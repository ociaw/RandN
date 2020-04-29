using System;

namespace Rand
{
    /// <summary>
    /// An implementation of Mersenne Twister, variant MT19937-64.
    /// Based on the pseudocode provided from https://en.wikipedia.org/wiki/Mersenne_twister
    /// and the original C code at http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/VERSIONS/C-LANG/mt19937-64.c
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

        private readonly ulong[] _state;
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

        public void Fill(Span<Byte> buffer) => UInt64Filler.Fill(buffer, NextUInt64);

        private void Twist()
        {
            for (int i = 0; i < _state.Length; i++)
            {
                ulong upper = _state[i] & UpperMask;
                ulong lower = _state[(i + 1) % RecurrenceDegree] & LowerMask;
                ulong x = upper | lower;

                ulong xA = x >> 1;
                if (x % 2 != 0)
                    xA ^= TwistMatrixCoefficients;

                _state[i] = _state[(i + MiddleWord) % RecurrenceDegree] ^ xA;
            }

            _index = 0;
        }

        public sealed class Factory : IReproducibleRngFactory<Mt1993764, UInt64>, IReproducibleRngFactory<Mt1993764, ulong[]>
        {
            public Mt1993764 Create(UInt64 seed)
            {
                // Init state from seed
                ulong[] state = new ulong[RecurrenceDegree];
                state[0] = seed;

                for (uint i = 1; i < state.Length; i++)
                {
                    state[i] = unchecked(F * (state[i - 1] ^ (state[i - 1] >> (WordSize - 2))) + i);
                }

                return new Mt1993764(state);
            }

            public Mt1993764 Create(ulong[] seed)
            {
                const UInt64 INITIAL_SEED = 19650218ul;
                const UInt64 F1 = 3935559000370003845ul;
                const UInt64 F2 = 2862933555777941757ul;

                var rng = Create(INITIAL_SEED);
                var state = rng._state;
                int stateIndex = 1;
                int seedIndex = 0;
                int iterationCount = Math.Max(RecurrenceDegree, seed.Length);
                for (int i = 0; i < iterationCount; i++)
                {
                    state[stateIndex] = unchecked((state[stateIndex] ^ ((state[stateIndex - 1] ^ (state[stateIndex - 1] >> 62)) * F1)) + seed[seedIndex] + (ulong)seedIndex);
                    stateIndex += 1;
                    seedIndex += 1;
                    if (stateIndex >= state.Length)
                    {
                        state[0] = state[^1];
                        stateIndex = 1;
                    }
                    if (seedIndex >= seed.Length)
                        seedIndex = 0;
                }

                for (int k = state.Length - 1; k > 0; k--)
                {
                    state[stateIndex] = unchecked((state[stateIndex] ^ ((state[stateIndex - 1] ^ (state[stateIndex - 1] >> 62)) * F2)) - (ulong)stateIndex);
                    stateIndex += 1;
                    if (stateIndex >= state.Length)
                    {
                        state[0] = state[^1];
                        stateIndex = 1;
                    }
                }

                state[0] = 1ul << 63;
                return rng;
            }
        }
    }
}
