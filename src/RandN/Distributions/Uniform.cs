



using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    /// <summary>
    /// A uniform distribution where each possible value has an equal probability of occuring.
    /// </summary>
    public static class Uniform
    {

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformSByte New(SByte low, SByte high) => UniformSByte.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformSByte NewInclusive(SByte low, SByte high) => UniformSByte.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt16 New(Int16 low, Int16 high) => UniformInt16.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt16 NewInclusive(Int16 low, Int16 high) => UniformInt16.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt32 New(Int32 low, Int32 high) => UniformInt32.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt32 NewInclusive(Int32 low, Int32 high) => UniformInt32.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt64 New(Int64 low, Int64 high) => UniformInt64.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt64 NewInclusive(Int64 low, Int64 high) => UniformInt64.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformByte New(Byte low, Byte high) => UniformByte.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformByte NewInclusive(Byte low, Byte high) => UniformByte.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformUInt16 New(UInt16 low, UInt16 high) => UniformUInt16.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformUInt16 NewInclusive(UInt16 low, UInt16 high) => UniformUInt16.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformUInt32 New(UInt32 low, UInt32 high) => UniformUInt32.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformUInt32 NewInclusive(UInt32 low, UInt32 high) => UniformUInt32.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformUInt64 New(UInt64 low, UInt64 high) => UniformUInt64.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformUInt64 NewInclusive(UInt64 low, UInt64 high) => UniformUInt64.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformTimeSpan New(TimeSpan low, TimeSpan high) => UniformTimeSpan.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformTimeSpan NewInclusive(TimeSpan low, TimeSpan high) => UniformTimeSpan.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformDecimal New(Decimal low, Decimal high) => UniformDecimal.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformDecimal NewInclusive(Decimal low, Decimal high) => UniformDecimal.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Single> New(Single low, Single high) => UniformFloat.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Single> NewInclusive(Single low, Single high) => UniformFloat.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Double> New(Double low, Double high) => UniformFloat.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformFloat<Double> NewInclusive(Double low, Double high) => UniformFloat.CreateInclusive(low, high);
    }
}
