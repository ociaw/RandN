using System;

namespace RandN.Distributions
{
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

        public static Single IntoFloatWithExponent(this UInt32 original, Int32 exponent)
        {
            const Int32 exponentBias = 127;
            const Int32 fractionBits = 23;
            UInt32 exponentBits = (UInt32)(exponentBias + exponent) << fractionBits;
            UInt32 bits = original | exponentBits;
            return bits.ToFloat();
        }

        public static Double IntoFloatWithExponent(this UInt64 original, Int32 exponent)
        {
            const Int32 exponentBias = 1023;
            const Int32 fractionBits = 52;
            UInt64 exponentBits = (UInt64)(exponentBias + exponent) << fractionBits;
            UInt64 bits = original | exponentBits;
            return bits.ToFloat();
        }

        public static UInt32 ToBits(this Single num)
        {
            unsafe
            {
                return *(UInt32*)&num;
            }
        }

        public static UInt64 ToBits(this Double num)
        {
            unsafe
            {
                return *(UInt64*)&num;
            }
        }

        public static Single ToFloat(this UInt32 bits)
        {
            unsafe
            {
                return *(Single*)&bits;
            }
        }

        public static Double ToFloat(this UInt64 bits)
        {
            unsafe
            {
                return *(Double*)&bits;
            }
        }
    }
}
