




using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{

    /// <summary>
    /// A uniform distribution of type <see cref="SByte" />.
    /// </summary>
    public sealed class UniformSByte : IDistribution<SByte>
    {
        private readonly SByte _low;
        private readonly UInt32 _range;
        private readonly Byte _zone;

        private UniformSByte(SByte low, UInt32 range, Byte zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformSByte" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(SByte, SByte)" />.
        /// </summary>
        public static UniformSByte Create(SByte low, SByte high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (SByte)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformSByte" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(SByte, SByte)" />.
        /// </summary>
        public static UniformSByte CreateInclusive(SByte low, SByte high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformSByte(low, range, (Byte)intsToReject);
        }

        /// <inheritdoc />
        public SByte Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (SByte)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((SByte)((SByte)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out SByte result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (SByte)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((SByte)((SByte)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="Int16" />.
    /// </summary>
    public sealed class UniformInt16 : IDistribution<Int16>
    {
        private readonly Int16 _low;
        private readonly UInt32 _range;
        private readonly UInt16 _zone;

        private UniformInt16(Int16 low, UInt32 range, UInt16 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformInt16" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int16, Int16)" />.
        /// </summary>
        public static UniformInt16 Create(Int16 low, Int16 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int16)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformInt16" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int16, Int16)" />.
        /// </summary>
        public static UniformInt16 CreateInclusive(Int16 low, Int16 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt16(low, range, (UInt16)intsToReject);
        }

        /// <inheritdoc />
        public Int16 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (Int16)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Int16)((Int16)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Int16 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int16)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int16)((Int16)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="Int32" />.
    /// </summary>
    public sealed class UniformInt32 : IDistribution<Int32>
    {
        private readonly Int32 _low;
        private readonly UInt32 _range;
        private readonly UInt32 _zone;

        private UniformInt32(Int32 low, UInt32 range, UInt32 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformInt32" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int32, Int32)" />.
        /// </summary>
        public static UniformInt32 Create(Int32 low, Int32 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int32)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformInt32" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int32, Int32)" />.
        /// </summary>
        public static UniformInt32 CreateInclusive(Int32 low, Int32 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt32(low, range, (UInt32)intsToReject);
        }

        /// <inheritdoc />
        public Int32 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (Int32)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Int32)((Int32)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Int32 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int32)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int32)((Int32)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="Int64" />.
    /// </summary>
    public sealed class UniformInt64 : IDistribution<Int64>
    {
        private readonly Int64 _low;
        private readonly UInt64 _range;
        private readonly UInt64 _zone;

        private UniformInt64(Int64 low, UInt64 range, UInt64 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformInt64" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int64, Int64)" />.
        /// </summary>
        public static UniformInt64 Create(Int64 low, Int64 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int64)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformInt64" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int64, Int64)" />.
        /// </summary>
        public static UniformInt64 CreateInclusive(Int64 low, Int64 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt64.MaxValue;
            var range = unchecked((UInt64)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt64(low, range, (UInt64)intsToReject);
        }

        /// <inheritdoc />
        public Int64 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (Int64)unsigned;

            var zone = UInt64.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt64();
            }

            return unchecked((Int64)((Int64)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Int64 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int64)unsigned;
                return true;
            }

            var zone = UInt64.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int64)((Int64)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="Byte" />.
    /// </summary>
    public sealed class UniformByte : IDistribution<Byte>
    {
        private readonly Byte _low;
        private readonly UInt32 _range;
        private readonly Byte _zone;

        private UniformByte(Byte low, UInt32 range, Byte zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformByte" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Byte, Byte)" />.
        /// </summary>
        public static UniformByte Create(Byte low, Byte high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Byte)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformByte" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Byte, Byte)" />.
        /// </summary>
        public static UniformByte CreateInclusive(Byte low, Byte high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformByte(low, range, (Byte)intsToReject);
        }

        /// <inheritdoc />
        public Byte Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (Byte)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Byte)((Byte)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out Byte result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Byte)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Byte)((Byte)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="UInt16" />.
    /// </summary>
    public sealed class UniformUInt16 : IDistribution<UInt16>
    {
        private readonly UInt16 _low;
        private readonly UInt32 _range;
        private readonly UInt16 _zone;

        private UniformUInt16(UInt16 low, UInt32 range, UInt16 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt16" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt16, UInt16)" />.
        /// </summary>
        public static UniformUInt16 Create(UInt16 low, UInt16 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt16)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt16" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt16, UInt16)" />.
        /// </summary>
        public static UniformUInt16 CreateInclusive(UInt16 low, UInt16 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt16(low, range, (UInt16)intsToReject);
        }

        /// <inheritdoc />
        public UInt16 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (UInt16)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((UInt16)((UInt16)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out UInt16 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt16)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt16)((UInt16)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="UInt32" />.
    /// </summary>
    public sealed class UniformUInt32 : IDistribution<UInt32>
    {
        private readonly UInt32 _low;
        private readonly UInt32 _range;
        private readonly UInt32 _zone;

        private UniformUInt32(UInt32 low, UInt32 range, UInt32 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt32" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt32, UInt32)" />.
        /// </summary>
        public static UniformUInt32 Create(UInt32 low, UInt32 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt32)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt32" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt32, UInt32)" />.
        /// </summary>
        public static UniformUInt32 CreateInclusive(UInt32 low, UInt32 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt32(low, range, (UInt32)intsToReject);
        }

        /// <inheritdoc />
        public UInt32 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (UInt32)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((UInt32)((UInt32)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out UInt32 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt32();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt32)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt32)((UInt32)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

    /// <summary>
    /// A uniform distribution of type <see cref="UInt64" />.
    /// </summary>
    public sealed class UniformUInt64 : IDistribution<UInt64>
    {
        private readonly UInt64 _low;
        private readonly UInt64 _range;
        private readonly UInt64 _zone;

        private UniformUInt64(UInt64 low, UInt64 range, UInt64 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt64" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt64, UInt64)" />.
        /// </summary>
        public static UniformUInt64 Create(UInt64 low, UInt64 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt64)(high - 1));
        }

        /// <summary>
        /// Creates a <see cref="UniformUInt64" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt64, UInt64)" />.
        /// </summary>
        public static UniformUInt64 CreateInclusive(UInt64 low, UInt64 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt64.MaxValue;
            var range = unchecked((UInt64)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformUInt64(low, range, (UInt64)intsToReject);
        }

        /// <inheritdoc />
        public UInt64 Sample<TRng>(TRng rng) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            if (_range == 0) // 0 is a special case where we sample the entire range.
                return (UInt64)unsigned;

            var zone = UInt64.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt64();
            }

            return unchecked((UInt64)((UInt64)(unsigned % _range) + _low));
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out UInt64 result) where TRng : IRng
        {
            var unsigned = rng.NextUInt64();
            if (_range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt64)unsigned;
                return true;
            }

            var zone = UInt64.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt64)((UInt64)(unsigned % _range) + _low));
                return true;
            }

            result = default;
            return false;
        }
    }

}
