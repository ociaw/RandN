



using System;

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    /// <summary>
    /// A uniform distribution where each possible value has an equal probability of occuring.
    /// </summary>
    public static partial class Uniform
    {

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.TimeSpan New(System.TimeSpan low, System.TimeSpan high) => Uniform.TimeSpan.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.TimeSpan NewInclusive(System.TimeSpan low, System.TimeSpan high) => Uniform.TimeSpan.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Decimal New(System.Decimal low, System.Decimal high) => Uniform.Decimal.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Decimal NewInclusive(System.Decimal low, System.Decimal high) => Uniform.Decimal.CreateInclusive(low, high);



        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<SByte> New(SByte low, SByte high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<SByte> NewInclusive(SByte low, SByte high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int16> New(Int16 low, Int16 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int16> NewInclusive(Int16 low, Int16 high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int32> New(Int32 low, Int32 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int32> NewInclusive(Int32 low, Int32 high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int64> New(Int64 low, Int64 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Int64> NewInclusive(Int64 low, Int64 high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Byte> New(Byte low, Byte high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<Byte> NewInclusive(Byte low, Byte high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt16> New(UInt16 low, UInt16 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt16> NewInclusive(UInt16 low, UInt16 high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt32> New(UInt32 low, UInt32 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt32> NewInclusive(UInt32 low, UInt32 high) => UniformInt.CreateInclusive(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt64> New(UInt64 low, UInt64 high) => UniformInt.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static UniformInt<UInt64> NewInclusive(UInt64 low, UInt64 high) => UniformInt.CreateInclusive(low, high);

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
