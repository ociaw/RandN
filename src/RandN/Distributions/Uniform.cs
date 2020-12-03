



using System;
// ReSharper disable RedundantNameQualifier

/*** This file is auto generated - any changes made here will be lost. ***/
namespace RandN.Distributions
{
    /// <summary>
    /// A uniform distribution where each possible value has an equal probability of occurring.
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
        public static Uniform.SByte New(System.SByte low, System.SByte high) => Uniform.SByte.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.SByte NewInclusive(System.SByte low, System.SByte high) => Uniform.SByte.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int16 New(System.Int16 low, System.Int16 high) => Uniform.Int16.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int16 NewInclusive(System.Int16 low, System.Int16 high) => Uniform.Int16.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int32 New(System.Int32 low, System.Int32 high) => Uniform.Int32.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int32 NewInclusive(System.Int32 low, System.Int32 high) => Uniform.Int32.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int64 New(System.Int64 low, System.Int64 high) => Uniform.Int64.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Int64 NewInclusive(System.Int64 low, System.Int64 high) => Uniform.Int64.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Byte New(System.Byte low, System.Byte high) => Uniform.Byte.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Byte NewInclusive(System.Byte low, System.Byte high) => Uniform.Byte.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt16 New(System.UInt16 low, System.UInt16 high) => Uniform.UInt16.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt16 NewInclusive(System.UInt16 low, System.UInt16 high) => Uniform.UInt16.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt32 New(System.UInt32 low, System.UInt32 high) => Uniform.UInt32.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt32 NewInclusive(System.UInt32 low, System.UInt32 high) => Uniform.UInt32.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt64 New(System.UInt64 low, System.UInt64 high) => Uniform.UInt64.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.UInt64 NewInclusive(System.UInt64 low, System.UInt64 high) => Uniform.UInt64.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Single New(System.Single low, System.Single high) => Uniform.Single.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Single NewInclusive(System.Single low, System.Single high) => Uniform.Single.CreateInclusive(low, high);


        /// <summary>
        /// Creates uniform distribution in the interval [low, high), inclusive of low and exclusive of high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The exclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than or equal to <paramref name="high"/>.
        /// </exception>
        public static Uniform.Double New(System.Double low, System.Double high) => Uniform.Double.Create(low, high);

        /// <summary>
        /// Creates uniform distribution in the interval [low, high], inclusive of low and high.
        /// </summary>
        /// <param name="low">The inclusive lower bound.</param>
        /// <param name="high">The inclusive upper bound.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="low"/> is greater than <paramref name="high"/>.
        /// </exception>
        public static Uniform.Double NewInclusive(System.Double low, System.Double high) => Uniform.Double.CreateInclusive(low, high);


    }
}
