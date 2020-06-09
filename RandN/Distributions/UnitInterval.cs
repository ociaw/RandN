using System;

/*** This file is auto generated - any changes made here will be lost. Modify UnitInterval.tt instead. ***/

// The algorithms used in the ClosedOpen, OpenClosed, and Open distributions for Single and Double
// are based off of those used in the rust rand crate:
// https://github.com/rust-random/rand/blob/2c2fbd6463bb3dba492d0387f05f953bdc297d2f/src/distributions/float.rs

namespace RandN.Distributions
{
    /// <summary>
    /// A collection of distributions over the interval 0-1.
    /// </summary>
    public sealed partial class UnitInterval
    {
        /// <summary>
        /// A distribution over the closed-open interval [0, 1).
        /// </summary>
        public sealed class ClosedOpenSingle : IDistribution<Single>
        {
            private ClosedOpenSingle() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedOpenSingle" />.
            /// </summary>
            public static ClosedOpenSingle Instance { get; } = new ClosedOpenSingle();

            /// <inheritdoc />
            public Single Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Single) * 8;
                const Int32 precision = 23 + 1;
                const Single scale = 1.0f / (1u << precision);

                var random = rng.NextUInt32();
                var value = random >> (floatSize - precision);
                return scale * value;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the open-closed interval (0, 1].
        /// </summary>
        public sealed class OpenClosedSingle : IDistribution<Single>
        {
            private OpenClosedSingle() { }

            /// <summary>
            /// Gets the instance of <see cref="OpenClosedSingle" />.
            /// </summary>
            public static OpenClosedSingle Instance { get; } = new OpenClosedSingle();

            /// <inheritdoc />
            public Single Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Single) * 8;
                const Int32 precision = 23 + 1;
                const Single scale = 1.0f / (1u << precision);

                var random = rng.NextUInt32();
                var value = random >> (floatSize - precision);
                return scale * (value + 1);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the closed interval [0, 1].
        /// </summary>
        public sealed class ClosedSingle : IDistribution<Single>
        {
            private ClosedSingle() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedSingle" />.
            /// </summary>
            public static ClosedSingle Instance { get; } = new ClosedSingle();

            /// <inheritdoc />
            public Single Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Single) * 8;
                const Int32 precision = 23 + 1;
                const Single scale = (1.0f + FloatUtils.MachineEpsilonSingle) / (1u << precision);

                var random = rng.NextUInt32();
                var value = random >> (floatSize - precision);
                return scale * (value);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the open interval (0, 1).
        /// </summary>
        public sealed class OpenSingle : IDistribution<Single>
        {
            private OpenSingle() { }

            /// <summary>
            /// Gets the instance of <see cref="OpenSingle" />.
            /// </summary>
            public static OpenSingle Instance { get; } = new OpenSingle();

            /// <inheritdoc />
            public Single Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 23 or 52 bits of precision
                const Int32 floatSize = sizeof(Single) * 8;
                const Int32 precision = 23;

                var random = rng.NextUInt32();
                var value = random >> (floatSize - precision);
                return value.IntoFloatWithExponent(0) - (1.0f - FloatUtils.MachineEpsilonSingle / 2.0f);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Single result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the closed-open interval [0, 1).
        /// </summary>
        public sealed class ClosedOpenDouble : IDistribution<Double>
        {
            private ClosedOpenDouble() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedOpenDouble" />.
            /// </summary>
            public static ClosedOpenDouble Instance { get; } = new ClosedOpenDouble();

            /// <inheritdoc />
            public Double Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Double) * 8;
                const Int32 precision = 52 + 1;
                const Double scale = 1.0d / (1ul << precision);

                var random = rng.NextUInt64();
                var value = random >> (floatSize - precision);
                return scale * value;
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Double result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the open-closed interval (0, 1].
        /// </summary>
        public sealed class OpenClosedDouble : IDistribution<Double>
        {
            private OpenClosedDouble() { }

            /// <summary>
            /// Gets the instance of <see cref="OpenClosedDouble" />.
            /// </summary>
            public static OpenClosedDouble Instance { get; } = new OpenClosedDouble();

            /// <inheritdoc />
            public Double Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Double) * 8;
                const Int32 precision = 52 + 1;
                const Double scale = 1.0d / (1ul << precision);

                var random = rng.NextUInt64();
                var value = random >> (floatSize - precision);
                return scale * (value + 1);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Double result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the closed interval [0, 1].
        /// </summary>
        public sealed class ClosedDouble : IDistribution<Double>
        {
            private ClosedDouble() { }

            /// <summary>
            /// Gets the instance of <see cref="ClosedDouble" />.
            /// </summary>
            public static ClosedDouble Instance { get; } = new ClosedDouble();

            /// <inheritdoc />
            public Double Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 24 or 53 bits of precision
                const Int32 floatSize = sizeof(Double) * 8;
                const Int32 precision = 52 + 1;
                const Double scale = (1.0d + FloatUtils.MachineEpsilonDouble) / (1ul << precision);

                var random = rng.NextUInt64();
                var value = random >> (floatSize - precision);
                return scale * (value);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Double result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

        /// <summary>
        /// A distribution over the open interval (0, 1).
        /// </summary>
        public sealed class OpenDouble : IDistribution<Double>
        {
            private OpenDouble() { }

            /// <summary>
            /// Gets the instance of <see cref="OpenDouble" />.
            /// </summary>
            public static OpenDouble Instance { get; } = new OpenDouble();

            /// <inheritdoc />
            public Double Sample<TRng>(TRng rng) where TRng : IRng
            {
                // 23 or 52 bits of precision
                const Int32 floatSize = sizeof(Double) * 8;
                const Int32 precision = 52;

                var random = rng.NextUInt64();
                var value = random >> (floatSize - precision);
                return value.IntoFloatWithExponent(0) - (1.0d - FloatUtils.MachineEpsilonDouble / 2.0d);
            }

            /// <inheritdoc />
            public Boolean TrySample<TRng>(TRng rng, out Double result) where TRng : IRng
            {
                result = Sample(rng);
                return true;
            }
        }

    }
}
