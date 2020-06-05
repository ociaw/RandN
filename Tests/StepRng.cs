using RandN.RngHelpers;
using System;

namespace RandN
{
    internal sealed class StepRng : IRng
    {
        public StepRng(UInt64 state) => State = state;

        public UInt64 State { get; set; }

        public UInt64 Increment { get; set; } = 1;

        public void Fill(Span<Byte> buffer) => Filler.FillBytesViaNext(this, buffer);

        public UInt32 NextUInt32() => (UInt32)NextUInt64();

        public UInt64 NextUInt64()
        {
            var value = State;
            State = unchecked(State + Increment);
            return value;
        }

        public sealed class Factory : IReproducibleRngFactory<StepRng, UInt64>
        {
            private readonly UInt64 _increment;

            public Factory(UInt64 increment = 1) => _increment = increment;

            public StepRng Create(UInt64 seed)
            {
                var rng = new StepRng(seed);
                rng.Increment = _increment;
                return rng; 
            }

            public UInt64 CreateSeed<TSeedingRng>(TSeedingRng seedingRng) where TSeedingRng : IRng => seedingRng.NextUInt64();
        }
    }
}
