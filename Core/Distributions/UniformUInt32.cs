using System;

namespace Rand.Distributions
{
    public sealed class UniformUInt32 : IDistribution<UInt32>
    {
        private const UInt32 MAX = UInt32.MaxValue;

        private readonly UInt32 _low;
        private readonly UInt32 _range;
        private readonly UInt32 _zone;

        private UniformUInt32(UInt32 low, UInt32 range, UInt32 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        public Boolean TrySample<TRng>(TRng rng, out UInt32 result) where TRng : IRng
        {
            result = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return true;
                
            if (result <= _zone)
            {
                unchecked
                {
                    result = result % _range + _low;
                }
                return true;
            }

            return false;
        }

        public sealed class Factory : IUniformFactory<UniformUInt32, UInt32>
        {
            public UniformUInt32 Create(UInt32 low, UInt32 high)
            {
                if (low >= high) 
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, high - 1);
            }

            public UniformUInt32 CreateInclusive(UInt32 low, UInt32 high)
            {
                if (low > high) 
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                UInt32 range;
                unchecked
                {
                    range = high - low + 1;
                }

                UInt32 intsToReject = range == 0 ? 0 : (MAX - range + 1) % range;
                UInt32 zone = MAX - intsToReject;

                return new UniformUInt32(low, range, zone);
            }
        }
    }
}
