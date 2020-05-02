using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace Rand.Distributions
{
    public sealed class UniformSByte : IDistribution<SByte>
    {
        private readonly SByte _low;
        private readonly SByte _range;
        private readonly SByte _z;

        private UniformSByte(SByte low, SByte range, SByte z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformSByte Create(SByte low, SByte high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (SByte)(high - 1));
        }

        public static UniformSByte CreateInclusive(SByte low, SByte high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (Byte)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformSByte(low, (SByte)range, (SByte)(Byte)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out SByte result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(Byte)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (SByte)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(Byte)_z;
                
            if (unsigned <= zone)
            {
                result = (SByte)unchecked((SByte)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformInt16 : IDistribution<Int16>
    {
        private readonly Int16 _low;
        private readonly Int16 _range;
        private readonly Int16 _z;

        private UniformInt16(Int16 low, Int16 range, Int16 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformInt16 Create(Int16 low, Int16 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int16)(high - 1));
        }

        public static UniformInt16 CreateInclusive(Int16 low, Int16 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (UInt16)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt16(low, (Int16)range, (Int16)(UInt16)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out Int16 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(UInt16)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int16)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(UInt16)_z;
                
            if (unsigned <= zone)
            {
                result = (Int16)unchecked((Int16)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformInt32 : IDistribution<Int32>
    {
        private readonly Int32 _low;
        private readonly Int32 _range;
        private readonly Int32 _z;

        private UniformInt32(Int32 low, Int32 range, Int32 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformInt32 Create(Int32 low, Int32 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int32)(high - 1));
        }

        public static UniformInt32 CreateInclusive(Int32 low, Int32 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (UInt32)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt32(low, (Int32)range, (Int32)(UInt32)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out Int32 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(UInt32)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int32)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(UInt32)_z;
                
            if (unsigned <= zone)
            {
                result = (Int32)unchecked((Int32)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformInt64 : IDistribution<Int64>
    {
        private readonly Int64 _low;
        private readonly Int64 _range;
        private readonly Int64 _z;

        private UniformInt64(Int64 low, Int64 range, Int64 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformInt64 Create(Int64 low, Int64 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int64)(high - 1));
        }

        public static UniformInt64 CreateInclusive(Int64 low, Int64 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt64.MaxValue;
            var range = (UInt64)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt64(low, (Int64)range, (Int64)(UInt64)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out Int64 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            var range = (UInt64)(UInt64)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int64)unsigned;
                return true;
            }

            var unsignedMax = UInt64.MaxValue;
            var zone = unsignedMax - (UInt64)(UInt64)_z;
                
            if (unsigned <= zone)
            {
                result = (Int64)unchecked((Int64)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformByte : IDistribution<Byte>
    {
        private readonly Byte _low;
        private readonly Byte _range;
        private readonly Byte _z;

        private UniformByte(Byte low, Byte range, Byte z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformByte Create(Byte low, Byte high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Byte)(high - 1));
        }

        public static UniformByte CreateInclusive(Byte low, Byte high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (Byte)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformByte(low, (Byte)range, (Byte)(Byte)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out Byte result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(Byte)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Byte)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(Byte)_z;
                
            if (unsigned <= zone)
            {
                result = (Byte)unchecked((Byte)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformUInt16 : IDistribution<UInt16>
    {
        private readonly UInt16 _low;
        private readonly UInt16 _range;
        private readonly UInt16 _z;

        private UniformUInt16(UInt16 low, UInt16 range, UInt16 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformUInt16 Create(UInt16 low, UInt16 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt16)(high - 1));
        }

        public static UniformUInt16 CreateInclusive(UInt16 low, UInt16 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (UInt16)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt16(low, (UInt16)range, (UInt16)(UInt16)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out UInt16 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(UInt16)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt16)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(UInt16)_z;
                
            if (unsigned <= zone)
            {
                result = (UInt16)unchecked((UInt16)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformUInt32 : IDistribution<UInt32>
    {
        private readonly UInt32 _low;
        private readonly UInt32 _range;
        private readonly UInt32 _z;

        private UniformUInt32(UInt32 low, UInt32 range, UInt32 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformUInt32 Create(UInt32 low, UInt32 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt32)(high - 1));
        }

        public static UniformUInt32 CreateInclusive(UInt32 low, UInt32 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt32.MaxValue;
            var range = (UInt32)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt32(low, (UInt32)range, (UInt32)(UInt32)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out UInt32 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            var range = (UInt32)(UInt32)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt32)unsigned;
                return true;
            }

            var unsignedMax = UInt32.MaxValue;
            var zone = unsignedMax - (UInt32)(UInt32)_z;
                
            if (unsigned <= zone)
            {
                result = (UInt32)unchecked((UInt32)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
    public sealed class UniformUInt64 : IDistribution<UInt64>
    {
        private readonly UInt64 _low;
        private readonly UInt64 _range;
        private readonly UInt64 _z;

        private UniformUInt64(UInt64 low, UInt64 range, UInt64 z)
        {
            _low = low;
            _range = range;
            _z = z;
        }

        public static UniformUInt64 Create(UInt64 low, UInt64 high)
        {
            if (low >= high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt64)(high - 1));
        }

        public static UniformUInt64 CreateInclusive(UInt64 low, UInt64 high)
        {
            if (low > high) 
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");
                    
            var unsignedMax = UInt64.MaxValue;
            var range = (UInt64)unchecked(high - low + 1);
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt64(low, (UInt64)range, (UInt64)(UInt64)intsToReject);
        }

        public Boolean TrySample<TRng>(TRng rng, out UInt64 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            var range = (UInt64)(UInt64)_range;
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt64)unsigned;
                return true;
            }

            var unsignedMax = UInt64.MaxValue;
            var zone = unsignedMax - (UInt64)(UInt64)_z;
                
            if (unsigned <= zone)
            {
                result = (UInt64)unchecked((UInt64)(unsigned % range) + _low);
                return true;
            }

            result = default;
            return false;
        }
    }
}
