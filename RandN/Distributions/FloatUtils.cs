using System;

namespace RandN.Distributions
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
        public static UInt32 ToBits(this Single num)
        {
            unsafe
            {
                return *(UInt32*)&num;
            }
        }

        /// <summary>
        /// Reinterprets the bits of <paramref name="num"/> as a <see cref="UInt64"/>.
        /// </summary>
        public static UInt64 ToBits(this Double num)
        {
            unsafe
            {
                return *(UInt64*)&num;
            }
        }

        /// <summary>
        /// Reinterprets <paramref name="bits"/> as a <see cref="Single"/>.
        /// </summary>
        public static Single ToFloat(this UInt32 bits)
        {
            unsafe
            {
                return *(Single*)&bits;
            }
        }

        /// <summary>
        /// Reinterprets <paramref name="bits"/> as a <see cref="Double"/>.
        /// </summary>
        public static Double ToFloat(this UInt64 bits)
        {
            unsafe
            {
                return *(Double*)&bits;
            }
        }
    }
}
