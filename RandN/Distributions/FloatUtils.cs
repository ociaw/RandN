using System;

namespace RandN.Distributions
{
    public static class FloatUtils
    {
        public static Double IntoFloatWithExponent(this UInt64 original, Int32 exponent)
        {
            // ($ty:ident, $uty:ident, $f_scalar:ident, $u_scalar:ty, $fraction_bits: expr, $exponent_bias: expr)
            // {      f64,        u64,             f64,          u64,                   52,                 1023 }
            const Int32 exponentBias = 1023;
            const Int32 fractionBits = 52;
            UInt64 exponentBits = (UInt64)(exponentBias + exponent) << fractionBits;
            UInt64 bits = original | exponentBits;
            return bits.ToFloat();
        }

        public static UInt64 ToBits(this Double num)
        {
            unsafe
            {
                return *(UInt64*)&num;
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
