using System;

namespace RandN.Distributions
{
    public static class FloatUtils
    {
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
