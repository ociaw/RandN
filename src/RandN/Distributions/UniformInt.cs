




using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    public static partial class Uniform
    {

        /// <summary>
        /// A uniform distribution of type <see cref="System.SByte" />.
        /// </summary>
        public readonly struct SByte : IPortableDistribution<System.SByte>
        {
            private readonly System.SByte _low;
            private readonly System.UInt32 _range;
            private readonly System.Byte _zone;

            private SByte(System.SByte low, System.UInt32 range, System.Byte zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="SByte" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.SByte, System.SByte)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static SByte Create(System.SByte low, System.SByte high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.SByte)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="SByte" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.SByte, System.SByte)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static SByte CreateInclusive(System.SByte low, System.SByte high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.Byte)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new SByte(low, range, (System.Byte)intsToReject);
            }

            /// <inheritdoc />
            public System.SByte Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.SByte)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.SByte)((System.SByte)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.SByte result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.SByte)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.SByte)((System.SByte)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.Int16" />.
        /// </summary>
        public readonly struct Int16 : IPortableDistribution<System.Int16>
        {
            private readonly System.Int16 _low;
            private readonly System.UInt32 _range;
            private readonly System.UInt16 _zone;

            private Int16(System.Int16 low, System.UInt32 range, System.UInt16 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="Int16" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.Int16, System.Int16)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Int16 Create(System.Int16 low, System.Int16 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.Int16)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="Int16" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Int16, System.Int16)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Int16 CreateInclusive(System.Int16 low, System.Int16 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.UInt16)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new Int16(low, range, (System.UInt16)intsToReject);
            }

            /// <inheritdoc />
            public System.Int16 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.Int16)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.Int16)((System.Int16)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.Int16 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.Int16)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.Int16)((System.Int16)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.Int32" />.
        /// </summary>
        public readonly struct Int32 : IPortableDistribution<System.Int32>
        {
            private readonly System.Int32 _low;
            private readonly System.UInt32 _range;
            private readonly System.UInt32 _zone;

            private Int32(System.Int32 low, System.UInt32 range, System.UInt32 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="Int32" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.Int32, System.Int32)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Int32 Create(System.Int32 low, System.Int32 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.Int32)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="Int32" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Int32, System.Int32)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Int32 CreateInclusive(System.Int32 low, System.Int32 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.UInt32)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new Int32(low, range, (System.UInt32)intsToReject);
            }

            /// <inheritdoc />
            public System.Int32 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.Int32)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.Int32)((System.Int32)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.Int32 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.Int32)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.Int32)((System.Int32)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.Int64" />.
        /// </summary>
        public readonly struct Int64 : IPortableDistribution<System.Int64>
        {
            private readonly System.Int64 _low;
            private readonly System.UInt64 _range;
            private readonly System.UInt64 _zone;

            private Int64(System.Int64 low, System.UInt64 range, System.UInt64 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="Int64" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.Int64, System.Int64)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Int64 Create(System.Int64 low, System.Int64 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.Int64)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="Int64" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Int64, System.Int64)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Int64 CreateInclusive(System.Int64 low, System.Int64 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt64.MaxValue;
                var range = unchecked((System.UInt64)(System.UInt64)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new Int64(low, range, (System.UInt64)intsToReject);
            }

            /// <inheritdoc />
            public System.Int64 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt64();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.Int64)unsigned);

                var zone = System.UInt64.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt64();
                }

                return unchecked((System.Int64)((System.Int64)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.Int64 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt64();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.Int64)unsigned);
                    return true;
                }

                var zone = System.UInt64.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.Int64)((System.Int64)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.Byte" />.
        /// </summary>
        public readonly struct Byte : IPortableDistribution<System.Byte>
        {
            private readonly System.Byte _low;
            private readonly System.UInt32 _range;
            private readonly System.Byte _zone;

            private Byte(System.Byte low, System.UInt32 range, System.Byte zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="Byte" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.Byte, System.Byte)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static Byte Create(System.Byte low, System.Byte high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.Byte)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="Byte" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.Byte, System.Byte)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static Byte CreateInclusive(System.Byte low, System.Byte high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.Byte)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new Byte(low, range, (System.Byte)intsToReject);
            }

            /// <inheritdoc />
            public System.Byte Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.Byte)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.Byte)((System.Byte)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.Byte result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.Byte)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.Byte)((System.Byte)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.UInt16" />.
        /// </summary>
        public readonly struct UInt16 : IPortableDistribution<System.UInt16>
        {
            private readonly System.UInt16 _low;
            private readonly System.UInt32 _range;
            private readonly System.UInt16 _zone;

            private UInt16(System.UInt16 low, System.UInt32 range, System.UInt16 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="UInt16" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.UInt16, System.UInt16)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static UInt16 Create(System.UInt16 low, System.UInt16 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.UInt16)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="UInt16" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.UInt16, System.UInt16)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static UInt16 CreateInclusive(System.UInt16 low, System.UInt16 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.UInt16)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new UInt16(low, range, (System.UInt16)intsToReject);
            }

            /// <inheritdoc />
            public System.UInt16 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.UInt16)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.UInt16)((System.UInt16)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.UInt16 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.UInt16)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.UInt16)((System.UInt16)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.UInt32" />.
        /// </summary>
        public readonly struct UInt32 : IPortableDistribution<System.UInt32>
        {
            private readonly System.UInt32 _low;
            private readonly System.UInt32 _range;
            private readonly System.UInt32 _zone;

            private UInt32(System.UInt32 low, System.UInt32 range, System.UInt32 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="UInt32" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.UInt32, System.UInt32)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static UInt32 Create(System.UInt32 low, System.UInt32 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.UInt32)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="UInt32" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.UInt32, System.UInt32)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static UInt32 CreateInclusive(System.UInt32 low, System.UInt32 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt32.MaxValue;
                var range = unchecked((System.UInt32)(System.UInt32)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new UInt32(low, range, (System.UInt32)intsToReject);
            }

            /// <inheritdoc />
            public System.UInt32 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.UInt32)unsigned);

                var zone = System.UInt32.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt32();
                }

                return unchecked((System.UInt32)((System.UInt32)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.UInt32 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt32();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.UInt32)unsigned);
                    return true;
                }

                var zone = System.UInt32.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.UInt32)((System.UInt32)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

        /// <summary>
        /// A uniform distribution of type <see cref="System.UInt64" />.
        /// </summary>
        public readonly struct UInt64 : IPortableDistribution<System.UInt64>
        {
            private readonly System.UInt64 _low;
            private readonly System.UInt64 _range;
            private readonly System.UInt64 _zone;

            private UInt64(System.UInt64 low, System.UInt64 range, System.UInt64 zone)
            {
                _low = low;
                _range = range;
                _zone = zone;
            }

            /// <summary>
            /// Creates a <see cref="UInt64" /> with an exclusive upper bound. Should not
            /// be used directly; instead, use <see cref="Uniform.New(System.UInt64, System.UInt64)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
            /// </exception>
            public static UInt64 Create(System.UInt64 low, System.UInt64 high)
            {
                if (low >= high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than {nameof(low)} ({low}).");

                return CreateInclusive(low, (System.UInt64)(high - 1));
            }

            /// <summary>
            /// Creates a <see cref="UInt64" /> with an exclusive lower bound. Should not
            /// be used directly; instead, use <see cref="Uniform.NewInclusive(System.UInt64, System.UInt64)" />.
            /// </summary>
            /// <exception cref="ArgumentOutOfRangeException">
            /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
            /// </exception>
            public static UInt64 CreateInclusive(System.UInt64 low, System.UInt64 high)
            {
                if (low > high)
                    throw new ArgumentOutOfRangeException(nameof(high), $"{nameof(high)} ({high}) must be higher than or equal to {nameof(low)} ({low}).");

                var unsignedMax = System.UInt64.MaxValue;
                var range = unchecked((System.UInt64)(System.UInt64)(high - low + 1));
                var intsToReject = range == 0 ? 0 : (unsignedMax - range + 1) % range;

                return new UInt64(low, range, (System.UInt64)intsToReject);
            }

            /// <inheritdoc />
            public System.UInt64 Sample<TRng>(TRng rng) where TRng : IRng
            {
                var unsigned = rng.NextUInt64();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                    return unchecked((System.UInt64)unsigned);

                var zone = System.UInt64.MaxValue - _zone;

                while (unsigned > zone)
                {
                    unsigned = rng.NextUInt64();
                }

                return unchecked((System.UInt64)((System.UInt64)(unsigned % _range) + _low));
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out System.UInt64 result) where TRng : IRng
            {
                var unsigned = rng.NextUInt64();
                if (_range == 0) // 0 is a special case where we sample the entire range.
                {
                    result = unchecked((System.UInt64)unsigned);
                    return true;
                }

                var zone = System.UInt64.MaxValue - _zone;

                if (unsigned <= zone)
                {
                    result = unchecked((System.UInt64)((System.UInt64)(unsigned % _range) + _low));
                    return true;
                }

                result = default;
                return false;
            }
        }

    }
}
