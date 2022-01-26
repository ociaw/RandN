using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace RandN.Implementation
{
    /// <summary>
    /// Utilities to facilitate bitwise manipulation of floating point numbers.
    /// </summary>
    public static class FloatUtils
    {
        /// <summary>
        /// The machine epsilon value for <see cref="Single"/>.
        /// </summary>
        /// <remarks>
        /// The value provided by <see cref="Single.Epsilon"/> is *not* the typical machine
        /// epsilon value, despite its name.
        /// See <see href="https://en.wikipedia.org/wiki/Machine_epsilon">Machine epsilon</see> on
        /// Wikipedia for more details.
        /// </remarks>
        public const Single MachineEpsilonSingle = 1.19209290e-07f;

        /// <summary>
        /// The machine epsilon value for <see cref="Double"/>.
        /// </summary>
        /// <remarks>
        /// The value provided by <see cref="Double.Epsilon"/> is *not* the typical machine
        /// epsilon value, despite its name.
        /// See <see href="https://en.wikipedia.org/wiki/Machine_epsilon">Machine epsilon</see> on
        /// Wikipedia for more details.
        /// </remarks>
        public const Double MachineEpsilonDouble = 2.2204460492503131e-16d;

        /// <summary>
        /// Converts the given <see cref="UInt32"/> into a <see cref="Single"/> with the given exponent.
        /// </summary>
        /// <param name="original">The bits used for the mantissa of the <see cref="Single"/>.</param>
        /// <param name="exponent">The exponent of the <see cref="Single"/>.</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single IntoFloatWithExponent(this UInt32 original, Int32 exponent)
        {
            const Int32 exponentBias = 127;
            const Int32 fractionBits = 23;
            UInt32 exponentBits = (UInt32)(exponentBias + exponent) << fractionBits;
            UInt32 bits = original | exponentBits;
            return bits.ToFloat();
        }

        /// <summary>
        /// Converts the given <see cref="UInt64"/> into a <see cref="Double"/> with the given exponent.
        /// </summary>
        /// <param name="original">The bits used for the mantissa of the <see cref="Double"/>.</param>
        /// <param name="exponent">The exponent of the <see cref="Double"/>.</param>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double IntoFloatWithExponent(this UInt64 original, Int32 exponent)
        {
            const Int32 exponentBias = 1023;
            const Int32 fractionBits = 52;
            UInt64 exponentBits = (UInt64)(exponentBias + exponent) << fractionBits;
            UInt64 bits = original | exponentBits;
            return bits.ToFloat();
        }

        /// <summary>
        /// Reinterprets the bits of <paramref name="num"/> as a <see cref="UInt32"/>.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToBits(this Single num)
        {
#if BITWISE_FLOAT_CONVERSION
            return BitConverter.SingleToUInt32Bits(num);
#else
            unsafe
            {
                return *(UInt32*)&num;
            }
#endif
        }

        /// <summary>
        /// Reinterprets the bits of <paramref name="num"/> as a <see cref="UInt64"/>.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToBits(this Double num)
        {
#if BITWISE_FLOAT_CONVERSION
            return BitConverter.DoubleToUInt64Bits(num);
#else
            unsafe
            {
                return *(UInt64*)&num;
            }
#endif
        }

        /// <summary>
        /// Reinterprets <paramref name="bits"/> as a <see cref="Single"/>.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToFloat(this UInt32 bits)
        {
#if BITWISE_FLOAT_CONVERSION
            return BitConverter.UInt32BitsToSingle(bits);
#else
            unsafe
            {
                return *(Single*)&bits;
            }
#endif
        }

        /// <summary>
        /// Reinterprets <paramref name="bits"/> as a <see cref="Double"/>.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToFloat(this UInt64 bits)
        {
#if BITWISE_FLOAT_CONVERSION
            return BitConverter.UInt64BitsToDouble(bits);
#else
            unsafe
            {
                return *(Double*)&bits;
            }
#endif
        }

        /// <summary>
        /// Reduces the precision of the given number if it's extended precision.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ForceStandardPrecision(this Single num)
        {
            // .NET Core always uses SSE2 for floating point, so floating point numbers are always
            // standard precision.
#if NETCOREAPP
            return num;
#else
            // Stores the single as a way to force conversion to standard precision
            Span<Single> temp = stackalloc Single[1];
            temp[0] = num;
            return temp[0];
#endif
        }

        /// <summary>
        /// Reduces the precision of the given number if it's extended precision.
        /// </summary>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ForceStandardPrecision(this Double num)
        {
            // .NET Core always uses SSE2 for floating point, so floating point numbers are always
            // standard precision.
#if NETCOREAPP
            return num;
#else
            // Stores the double as a way to force conversion to standard precision
            Span<Double> temp = stackalloc Double[1];
            temp[0] = num;
            return temp[0];
#endif
        }

        /// <summary>
        /// Decrements the mantissa of a decimal by one, wrapping by increasing the scale if necessary.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="original"/> is equal to zero.</exception>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal DecrementMantissa(this Decimal original)
        {
            // If the mantissa == 0, throw exception
            // If the mantissa == 1 and scale < 28, increment scale
            // If the mantissa == 1 and scale == 28, decrement mantissa
            // Otherwise, decrement mantissa

            if (original == 0)
            {
                // The original is equal to zero, so we can't make it smaller without
                // going into negative numbers.
                throw new ArgumentOutOfRangeException(nameof(original), 0, "Cannot decrement mantissa of 0.");
            }

#if NET5_0
            Span<Int32> bits = stackalloc Int32[4];
            _ = Decimal.GetBits(original, bits);
#else
            var bits = Decimal.GetBits(original);
#endif
            var lo = unchecked((UInt32)bits[0]);
            var mid = unchecked((UInt32)bits[1]);
            var hi = unchecked((UInt32)bits[2]);
            var flags = bits[3];
            var isNegative = flags < 0;
            var scale = unchecked((Byte)((flags & 0xFF0000) >> 16));

            if (lo == 1 && mid == 0 && hi == 0)
            {
                // Decrementing a mantissa of 1 makes the entire number 0,
                // so first we'll try to increase the scale
                if (scale == 28)
                {
                    // We're at the smallest positive number, so
                    // the only smaller number is zero.
                    lo = 0;
                }
                else
                {
                    // We bump up the scale by one, then set the lowest bit to
                    // 9. For reference, if we set lo = 10, the result would
                    // equal a mantissa of 1 with the original scale. So this
                    // is the same as multiplying the original by .9.
                    scale += 1;
                    lo = 9;
                }
            }
            else if (lo != 0)
            {
                lo -= 1;
            }
            else if (mid != 0)
            {
                mid -= 1;
                lo = UInt32.MaxValue;
            }
            else if (hi != 0)
            {
                hi -= 1;
                mid = UInt32.MaxValue;
                lo = UInt32.MaxValue;
            }

            return unchecked(new Decimal((Int32)lo, (Int32)mid, (Int32)hi, isNegative, scale));
        }
    }
}
