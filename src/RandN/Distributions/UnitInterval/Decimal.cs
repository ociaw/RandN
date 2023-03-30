using System;

namespace RandN.Distributions.UnitInterval;

/// <summary>
/// A distribution over the closed-open interval [0, 1).
/// </summary>
public static partial class ClosedOpen
{
    /// <summary>
    /// A <see cref="System.Decimal"/> distribution over the closed-open interval [0m, 1m).
    /// </summary>
    public readonly struct Decimal : IPortableDistribution<System.Decimal>
    {
        /// <summary>
        /// Gets the instance of <see cref="Decimal" />.
        /// </summary>
        public static Decimal Instance { get; } = new();

        /// <inheritdoc />
        public System.Decimal Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            System.Decimal result;
            while (!TrySample(rng, out result))
            { }

            return result;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Decimal result) where TRng : notnull, IRng
        {
            result = DecimalUtils.GenerateCandidateDecimal(rng);
            return result < 1;
        }
    }
}

/// <summary>
/// A distribution over the open-closed interval (0, 1].
/// </summary>
public static partial class OpenClosed
{
    /// <summary>
    /// A <see cref="System.Decimal"/> distribution over the open-closed interval (0m, 1m].
    /// </summary>
    public readonly struct Decimal : IPortableDistribution<System.Decimal>
    {
        /// <summary>
        /// Gets the instance of <see cref="Decimal" />.
        /// </summary>
        public static Decimal Instance { get; } = new();

        /// <inheritdoc />
        public System.Decimal Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            System.Decimal result;
            while (!TrySample(rng, out result))
            { }

            return result;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Decimal result) where TRng : notnull, IRng
        {
            result = DecimalUtils.GenerateCandidateDecimal(rng);
            return result is > 0 and <= 1;
        }
    }
}

/// <summary>
/// A distribution over the closed interval [0, 1].
/// </summary>
public static partial class Closed
{
    /// <summary>
    /// A <see cref="System.Decimal"/> distribution over the closed interval [0m, 1m].
    /// </summary>
    public readonly struct Decimal : IPortableDistribution<System.Decimal>
    {
        /// <summary>
        /// Gets the instance of <see cref="Decimal" />.
        /// </summary>
        public static Decimal Instance { get; } = new();

        /// <inheritdoc />
        public System.Decimal Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            System.Decimal result;
            while (!TrySample(rng, out result))
            { }

            return result;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Decimal result) where TRng : notnull, IRng
        {
            result = DecimalUtils.GenerateCandidateDecimal(rng);
            return result <= 1;
        }
    }
}

/// <summary>
/// A distribution over the open interval (0, 1).
/// </summary>
public static partial class Open
{
    /// <summary>
    /// A <see cref="System.Decimal"/> distribution over the open interval (0m, 1m).
    /// </summary>
    public readonly struct Decimal : IPortableDistribution<System.Decimal>
    {
        /// <summary>
        /// Gets the instance of <see cref="Decimal" />.
        /// </summary>
        public static Decimal Instance { get; } = new();

        /// <inheritdoc />
        public System.Decimal Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            System.Decimal result;
            while (!TrySample(rng, out result))
            { }

            return result;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Decimal result) where TRng : notnull, IRng
        {
            result = DecimalUtils.GenerateCandidateDecimal(rng);
            return result is > 0 and < 1;
        }
    }
}

internal static class DecimalUtils
{
    internal static Decimal GenerateCandidateDecimal<TRng>(TRng rng) where TRng : notnull, IRng
    {
        // 93-94 bits of precision - we discard the upper two bits to reduce the average number
        // of sample attempts. The range generated is approximately [0-1.98], so just under 50%
        // of sample attempts will be rejected.
        const Int32 scale = 28;
        const Int32 bitsToDiscard = 2;
        Int32 lo = unchecked((Int32)rng.NextUInt32());
        Int32 mid = unchecked((Int32)rng.NextUInt32());
        Int32 hi = unchecked((Int32)(rng.NextUInt32() >> bitsToDiscard));
        return new Decimal(lo, mid, hi, isNegative: false, scale);
    }
}
