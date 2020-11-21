




using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
        /// <summary>
        /// Contains methods to create instances of <see cref="UniformInt{T}" />.
        /// </summary>
    internal static class UniformInt
    {

        /// <summary>
        /// Creates a <see cref="UniformInt{SByte}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(SByte, SByte)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<SByte> Create(SByte low, SByte high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (SByte)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{SByte}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(SByte, SByte)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<SByte> CreateInclusive(SByte low, SByte high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<SByte>(low, range, (Byte)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{Int16}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int16, Int16)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int16> Create(Int16 low, Int16 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int16)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{Int16}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int16, Int16)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int16> CreateInclusive(Int16 low, Int16 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<Int16>(low, range, (UInt16)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{Int32}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int32, Int32)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int32> Create(Int32 low, Int32 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int32)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{Int32}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int32, Int32)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int32> CreateInclusive(Int32 low, Int32 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<Int32>(low, range, (UInt32)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{Int64}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Int64, Int64)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int64> Create(Int64 low, Int64 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Int64)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{Int64}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Int64, Int64)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int64> CreateInclusive(Int64 low, Int64 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt64.MaxValue;
            var range = unchecked((UInt64)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<Int64>(low, range, (UInt64)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{Byte}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(Byte, Byte)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Byte> Create(Byte low, Byte high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (Byte)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{Byte}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(Byte, Byte)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Byte> CreateInclusive(Byte low, Byte high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<Byte>(low, range, (Byte)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt16}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt16, UInt16)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt16> Create(UInt16 low, UInt16 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt16)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt16}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt16, UInt16)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt16> CreateInclusive(UInt16 low, UInt16 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<UInt16>(low, range, (UInt16)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt32}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt32, UInt32)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt32> Create(UInt32 low, UInt32 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt32)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt32}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt32, UInt32)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt32> CreateInclusive(UInt32 low, UInt32 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt32.MaxValue;
            var range = unchecked((UInt32)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<UInt32>(low, range, (UInt32)intsToReject);
        }

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt64}" /> with an exclusive upper bound. Should not
        /// be used directly; instead, use <see cref="Uniform.New(UInt64, UInt64)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt64> Create(UInt64 low, UInt64 high)
        {
            if (low >= high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

            return CreateInclusive(low, (UInt64)(high - 1));
        }
        

        /// <summary>
        /// Creates a <see cref="UniformInt{UInt64}" /> with an exclusive lower bound. Should not
        /// be used directly; instead, use <see cref="Uniform.NewInclusive(UInt64, UInt64)" />.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt64> CreateInclusive(UInt64 low, UInt64 high)
        {
            if (low > high)
                throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

            var unsignedMax = UInt64.MaxValue;
            var range = unchecked((UInt64)(high - low + 1));
            var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

            return new UniformInt<UInt64>(low, range, (UInt64)intsToReject);
        }

    }

    /// <summary>
    /// Implements a Uniform <see cref="IDistribution{TResult}"/> for integral types such as <see cref="Int32" /> and <see cref="UInt64" />.
    /// Use of any other type results in a runtime exception.
    /// </summary>
    public readonly struct UniformInt<T> : IPortableDistribution<T>
        // We're extremely restrictive here to discourage people from trying to use unsupported types for T
        where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
    {
        private readonly T _low;
        private readonly UInt64 _range;
        private readonly UInt64 _zone;
        
        internal UniformInt(T low, UInt64 range, UInt64 zone)
        {
            _low = low;
            _range = range;
            _zone = zone;
        }

        /// <inheritdoc />
        public T Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {

            if (typeof(T) == typeof(SByte))
                return (T)(Object)SampleSByte(rng);

            if (typeof(T) == typeof(Int16))
                return (T)(Object)SampleInt16(rng);

            if (typeof(T) == typeof(Int32))
                return (T)(Object)SampleInt32(rng);

            if (typeof(T) == typeof(Int64))
                return (T)(Object)SampleInt64(rng);

            if (typeof(T) == typeof(Byte))
                return (T)(Object)SampleByte(rng);

            if (typeof(T) == typeof(UInt16))
                return (T)(Object)SampleUInt16(rng);

            if (typeof(T) == typeof(UInt32))
                return (T)(Object)SampleUInt32(rng);

            if (typeof(T) == typeof(UInt64))
                return (T)(Object)SampleUInt64(rng);

            throw new NotSupportedException($"Type {typeof(T)} is not supported.");
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out T result) where TRng : notnull, IRng
        {

            if (typeof(T) == typeof(SByte))
            {
                Boolean success = TrySampleSByte(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(Int16))
            {
                Boolean success = TrySampleInt16(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(Int32))
            {
                Boolean success = TrySampleInt32(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(Int64))
            {
                Boolean success = TrySampleInt64(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(Byte))
            {
                Boolean success = TrySampleByte(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(UInt16))
            {
                Boolean success = TrySampleUInt16(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(UInt32))
            {
                Boolean success = TrySampleUInt32(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            if (typeof(T) == typeof(UInt64))
            {
                Boolean success = TrySampleUInt64(rng, out var temp);
                result = (T)(Object)temp;
                return success;
            }

            throw new NotSupportedException($"Type {typeof(T)} is not supported.");
        }
        

        private SByte SampleSByte<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (SByte)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (SByte)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((SByte)((SByte)(unsigned % range) + low));
        }

        private Boolean TrySampleSByte<TRng>(TRng rng, out SByte result) where TRng : notnull, IRng
        {
            var low = (SByte)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (SByte)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((SByte)((SByte)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private Int16 SampleInt16<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (Int16)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (Int16)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Int16)((Int16)(unsigned % range) + low));
        }

        private Boolean TrySampleInt16<TRng>(TRng rng, out Int16 result) where TRng : notnull, IRng
        {
            var low = (Int16)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int16)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int16)((Int16)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private Int32 SampleInt32<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (Int32)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (Int32)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Int32)((Int32)(unsigned % range) + low));
        }

        private Boolean TrySampleInt32<TRng>(TRng rng, out Int32 result) where TRng : notnull, IRng
        {
            var low = (Int32)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int32)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int32)((Int32)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private Int64 SampleInt64<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (Int64)(Object)_low;
            var range = (UInt64)_range;
            var unsigned = rng.NextUInt64();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (Int64)unsigned;

            var zone = UInt64.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt64();
            }

            return unchecked((Int64)((Int64)(unsigned % range) + low));
        }

        private Boolean TrySampleInt64<TRng>(TRng rng, out Int64 result) where TRng : notnull, IRng
        {
            var low = (Int64)(Object)_low;
            var range = (UInt64)_range;
            var unsigned = rng.NextUInt64();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Int64)unsigned;
                return true;
            }

            var zone = UInt64.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Int64)((Int64)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private Byte SampleByte<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (Byte)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (Byte)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((Byte)((Byte)(unsigned % range) + low));
        }

        private Boolean TrySampleByte<TRng>(TRng rng, out Byte result) where TRng : notnull, IRng
        {
            var low = (Byte)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (Byte)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((Byte)((Byte)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private UInt16 SampleUInt16<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (UInt16)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (UInt16)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((UInt16)((UInt16)(unsigned % range) + low));
        }

        private Boolean TrySampleUInt16<TRng>(TRng rng, out UInt16 result) where TRng : notnull, IRng
        {
            var low = (UInt16)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt16)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt16)((UInt16)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private UInt32 SampleUInt32<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (UInt32)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (UInt32)unsigned;

            var zone = UInt32.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt32();
            }

            return unchecked((UInt32)((UInt32)(unsigned % range) + low));
        }

        private Boolean TrySampleUInt32<TRng>(TRng rng, out UInt32 result) where TRng : notnull, IRng
        {
            var low = (UInt32)(Object)_low;
            var range = (UInt32)_range;
            var unsigned = rng.NextUInt32();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt32)unsigned;
                return true;
            }

            var zone = UInt32.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt32)((UInt32)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

        private UInt64 SampleUInt64<TRng>(TRng rng) where TRng : notnull, IRng
        {
            var low = (UInt64)(Object)_low;
            var range = (UInt64)_range;
            var unsigned = rng.NextUInt64();
            if (range == 0) // 0 is a special case where we sample the entire range.
                return (UInt64)unsigned;

            var zone = UInt64.MaxValue - _zone;

            while (unsigned > zone)
            {
                unsigned = rng.NextUInt64();
            }

            return unchecked((UInt64)((UInt64)(unsigned % range) + low));
        }

        private Boolean TrySampleUInt64<TRng>(TRng rng, out UInt64 result) where TRng : notnull, IRng
        {
            var low = (UInt64)(Object)_low;
            var range = (UInt64)_range;
            var unsigned = rng.NextUInt64();
            if (range == 0) // 0 is a special case where we sample the entire range.
            {
                result = (UInt64)unsigned;
                return true;
            }

            var zone = UInt64.MaxValue - _zone;

            if (unsigned <= zone)
            {
                result = unchecked((UInt64)((UInt64)(unsigned % range) + low));
                return true;
            }

            result = default;
            return false;
        }

    }
}
