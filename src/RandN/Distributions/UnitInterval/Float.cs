using System;
using RandN.Implementation;

/*** This file is auto generated - any changes made here will be lost. Modify UnitInterval.tt instead. ***/

// The algorithms used in the ClosedOpen, OpenClosed, and Open distributions for Single and Double
// are based off of those used in the rust rand crate:
// https://github.com/rust-random/rand/blob/2c2fbd6463bb3dba492d0387f05f953bdc297d2f/src/distributions/float.rs

namespace RandN.Distributions.UnitInterval;

public static partial class ClosedOpen
{
    /// <summary>
    /// A <see cref="System.Single"/> distribution over the closed-open interval [0f, 1f).
    /// </summary>
    public readonly struct Single : IDistribution<System.Single>
    {
        /// <summary>
        /// Gets the instance of <see cref="Single" />.
        /// </summary>
        public static Single Instance { get; } = new();

        /// <inheritdoc />
        public System.Single Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Single) * 8;
            const Int32 precision = 23 + 1;
            const System.Single scale = 1.0f / (1u << precision);

            var random = rng.NextUInt32();
            var value = random >> (floatSize - precision);
            return scale * value;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Single result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class OpenClosed
{
    /// <summary>
    /// A <see cref="System.Single"/> distribution over the open-closed interval (0f, 1f].
    /// </summary>
    public readonly struct Single : IDistribution<System.Single>
    {
        /// <summary>
        /// Gets the instance of <see cref="Single" />.
        /// </summary>
        public static Single Instance { get; } = new();

        /// <inheritdoc />
        public System.Single Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Single) * 8;
            const Int32 precision = 23 + 1;
            const System.Single scale = 1.0f / (1u << precision);

            var random = rng.NextUInt32();
            var value = random >> (floatSize - precision);
            return scale * (value + 1);
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Single result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class Closed
{
    /// <summary>
    /// A <see cref="System.Single"/> distribution over the closed interval [0f, 1f].
    /// </summary>
    public readonly struct Single : IDistribution<System.Single>
    {
        /// <summary>
        /// Gets the instance of <see cref="Single" />.
        /// </summary>
        public static Single Instance { get; } = new();

        /// <inheritdoc />
        public System.Single Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Single) * 8;
            const Int32 precision = 23 + 1;
            const System.Single scale = (1.0f + FloatUtils.MachineEpsilonSingle) / (1u << precision);

            var random = rng.NextUInt32();
            var value = random >> (floatSize - precision);
            return (scale * value).ForceStandardPrecision();
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Single result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class Open
{
    /// <summary>
    /// A <see cref="System.Single"/> distribution over the open interval (0f, 1f).
    /// </summary>
    public readonly struct Single : IDistribution<System.Single>
    {
        /// <summary>
        /// Gets the instance of <see cref="Single" />.
        /// </summary>
        public static Single Instance { get; } = new();

        /// <inheritdoc />
        public System.Single Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 23 or 52 bits of precision
            const Int32 floatSize = sizeof(System.Single) * 8;
            const Int32 precision = 23;

            var random = rng.NextUInt32();
            var value = random >> (floatSize - precision);
            return value.IntoFloatWithExponent(0) - (1.0f - FloatUtils.MachineEpsilonSingle / 2.0f);
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Single result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
public static partial class ClosedOpen
{
    /// <summary>
    /// A <see cref="System.Double"/> distribution over the closed-open interval [0d, 1d).
    /// </summary>
    public readonly struct Double : IDistribution<System.Double>
    {
        /// <summary>
        /// Gets the instance of <see cref="Double" />.
        /// </summary>
        public static Double Instance { get; } = new();

        /// <inheritdoc />
        public System.Double Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Double) * 8;
            const Int32 precision = 52 + 1;
            const System.Double scale = 1.0d / (1ul << precision);

            var random = rng.NextUInt64();
            var value = random >> (floatSize - precision);
            return scale * value;
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Double result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class OpenClosed
{
    /// <summary>
    /// A <see cref="System.Double"/> distribution over the open-closed interval (0d, 1d].
    /// </summary>
    public readonly struct Double : IDistribution<System.Double>
    {
        /// <summary>
        /// Gets the instance of <see cref="Double" />.
        /// </summary>
        public static Double Instance { get; } = new();

        /// <inheritdoc />
        public System.Double Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Double) * 8;
            const Int32 precision = 52 + 1;
            const System.Double scale = 1.0d / (1ul << precision);

            var random = rng.NextUInt64();
            var value = random >> (floatSize - precision);
            return scale * (value + 1);
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Double result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class Closed
{
    /// <summary>
    /// A <see cref="System.Double"/> distribution over the closed interval [0d, 1d].
    /// </summary>
    public readonly struct Double : IDistribution<System.Double>
    {
        /// <summary>
        /// Gets the instance of <see cref="Double" />.
        /// </summary>
        public static Double Instance { get; } = new();

        /// <inheritdoc />
        public System.Double Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 24 or 53 bits of precision
            const Int32 floatSize = sizeof(System.Double) * 8;
            const Int32 precision = 52 + 1;
            const System.Double scale = (1.0d + FloatUtils.MachineEpsilonDouble) / (1ul << precision);

            var random = rng.NextUInt64();
            var value = random >> (floatSize - precision);
            return (scale * value).ForceStandardPrecision();
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Double result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}

public static partial class Open
{
    /// <summary>
    /// A <see cref="System.Double"/> distribution over the open interval (0d, 1d).
    /// </summary>
    public readonly struct Double : IDistribution<System.Double>
    {
        /// <summary>
        /// Gets the instance of <see cref="Double" />.
        /// </summary>
        public static Double Instance { get; } = new();

        /// <inheritdoc />
        public System.Double Sample<TRng>(TRng rng) where TRng : notnull, IRng
        {
            // 23 or 52 bits of precision
            const Int32 floatSize = sizeof(System.Double) * 8;
            const Int32 precision = 52;

            var random = rng.NextUInt64();
            var value = random >> (floatSize - precision);
            return value.IntoFloatWithExponent(0) - (1.0d - FloatUtils.MachineEpsilonDouble / 2.0d);
        }

        /// <inheritdoc />
        public Boolean TrySample<TRng>(TRng rng, out System.Double result) where TRng : notnull, IRng
        {
            result = Sample(rng);
            return true;
        }
    }
}
